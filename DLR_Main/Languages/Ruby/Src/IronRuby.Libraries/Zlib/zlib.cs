/* ****************************************************************************
 *
 * Copyright (c) Microsoft Corporation. 
 *
 * This source code is subject to terms and conditions of the Microsoft Public License. A 
 * copy of the license can be found in the License.html file at the root of this distribution. If 
 * you cannot locate the  Microsoft Public License, please send an email to 
 * ironruby@microsoft.com. By using this source code in any fashion, you are agreeing to be bound 
 * by the terms of the Microsoft Public License.
 *
 * You must not remove this notice, or any other, from this software.
 *
 *
 * ***************************************************************************/

using System; using Microsoft;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Runtime.CompilerServices;

using IronRuby.Builtins;
using IronRuby.Runtime;
using Microsoft.Scripting.Runtime;
using Microsoft.Scripting.Generation;

namespace IronRuby.StandardLibrary.Zlib {

    [RubyModule("Zlib")]
    public static class Zlib {

        #region Constants

        [RubyConstant("ZLIB_VERSION")]
        public static string ZLIB_VERSION = "1.2.3";

        [RubyConstant("VERSION")]
        public static string VERSION = "0.6.0";

        [RubyConstant("MAXBITS")]
        public const int MAXBITS = 15;

        [RubyConstant("MAXLCODES")]
        public const int MAXLCODES = 286;

        [RubyConstant("MAXDCODES")]
        public const int MAXDCODES = 30;

        [RubyConstant("MAXCODES")]
        public const int MAXCODES = (MAXLCODES + MAXDCODES);

        [RubyConstant("FIXLCODES")]
        public const int FIXLCODES = 288;

        [RubyConstant("MAX_WBITS")]
        public const int MAX_WBITS = 15;

        [RubyConstant("Z_DEFLATED")]
        public const int Z_DEFLATED = 8;

        #endregion

        #region ZStream class

        [RubyClass("ZStream")]
        public class ZStream {
            protected readonly List<byte>/*!*/ _inputBuffer;
            protected readonly List<byte>/*!*/ _outputBuffer;
            protected int _outPos = -1;
            protected int _inPos = -1;
            protected byte _bitBucket = 0;
            protected byte _bitCount = 0;
            protected bool _closed = false;

            public ZStream() {
                _outPos = -1;
                _inPos = -1;
                _bitBucket = 0;
                _bitCount = 0;
                _inputBuffer = new List<byte>();
                _outputBuffer = new List<byte>();
            }

            #region instance methods
            public bool Close() {
                _closed = true;
                return _closed;
            }
            #endregion

            [RubyMethod("adler")]
            public static int Adler(ZStream/*!*/ self) {
                throw new NotImplementedError();
            }

            [RubyMethod("avail_in")]
            public static int AvailIn(ZStream/*!*/ self) {
                return self._inputBuffer.Count - self._inPos;
            }

            [RubyMethod("avail_out")]
            public static int GetAvailOut(ZStream/*!*/ self) {
                return self._outputBuffer.Count - self._outPos;
            }

            [RubyMethod("avail_out=")]
            public static int SetAvailOut(ZStream/*!*/ self, int size) {
                self._outputBuffer.Capacity = size;
                return self._outputBuffer.Count;
            }

            [RubyMethod("finish")]
            [RubyMethod("close")]
            public static bool Close(ZStream/*!*/ self) {
                return self.Close();
            }

            [RubyMethod("stream_end?")]
            [RubyMethod("finished?")]
            [RubyMethod("closed?")]
            public static bool IsClosed(ZStream/*!*/ self) {
                return self._closed;
            }

            [RubyMethod("data_type")]
            public static void DataType(ZStream/*!*/ self) {
                throw new NotImplementedException();
            }

            [RubyMethod("flush_next_in")]
            public static List<byte> FlushNextIn(ZStream/*!*/ self) {
                self._inPos = self._inputBuffer.Count;
                return self._inputBuffer;
            }

            [RubyMethod("flush_next_out")]
            public static List<byte> FlushNextOut(ZStream/*!*/ self) {
                self._outPos = self._outputBuffer.Count;
                return self._outputBuffer;
            }

            [RubyMethod("reset")]
            public static void Reset(ZStream/*!*/ self) {
                self._outPos = -1;
                self._inPos = -1;
                self._inputBuffer.Clear();
                self._outputBuffer.Clear();
            }

            [RubyMethod("total_in")]
            public static int TotalIn(ZStream/*!*/ self) {
                return self._inputBuffer.Count;
            }

            [RubyMethod("total_out")]
            public static int TotalOut(ZStream/*!*/ self) {
                return self._outputBuffer.Count;
            }

            protected int GetBits(int need) {
                int val = _bitBucket;
                while (_bitCount < need) {
                    val |= (int)(_inputBuffer[++_inPos] << _bitCount);
                    _bitCount += 8;
                }

                _bitBucket = (byte)(val >> need);
                _bitCount -= (byte)need;
                return (val & ((1 << need) - 1));
            }
        }

        #endregion

        #region Inflate class

        [RubyClass("Inflate")]
        public class Inflate : ZStream {
            private int _wBits;
            private bool _rawDeflate;
            private HuffmanTree _fixedLengthCodes;
            private HuffmanTree _fixedDistanceCodes;
            private HuffmanTree _dynamicLengthCodes;
            private HuffmanTree _dynamicDistanceCodes;

            [RubyConstructor]
            public static Inflate/*!*/ Create(RubyClass/*!*/ self) {
                return new Inflate(MAX_WBITS);
            }

            [RubyConstructor]
            public static Inflate/*!*/ Create(RubyClass/*!*/ self, int windowBits) {
                return new Inflate(windowBits);
            }

            public Inflate(int windowBits) {
                _wBits = windowBits;
                if (_wBits < 0) {
                    _rawDeflate = true;
                    _wBits *= -1;
                }
            }

            [RubyMethod("inflate")]
            public static MutableString/*!*/ InflateStream(Inflate/*!*/ self, MutableString/*!*/ zstring) {
                self._inputBuffer.AddRange(zstring.ConvertToBytes());

                if (self._rawDeflate == false) {
                    byte compression_method_and_flags = self._inputBuffer[++(self._inPos)];
                    byte flags = self._inputBuffer[++(self._inPos)];
                    if (((compression_method_and_flags << (byte)0x08) + flags) % (byte)31 != 0) {
                        throw new DataError("incorrect header check");
                    }

                    byte compression_method = (byte)(compression_method_and_flags & (byte)0x0F);
                    if (compression_method != Z_DEFLATED) {
                        throw new DataError("unknown compression method");
                    }

                    byte compression_info = (byte)(compression_method_and_flags >> (byte)0x04);
                    if ((compression_info + 8) > self._wBits) {
                        throw new DataError("invalid window size");
                    }

                    bool preset_dictionary_flag = ((flags & 0x20) >> 0x05 == 1);
                    byte compression_level = (byte)((flags & 0xC0) >> (byte)0x06);

                    //TODO: Add Preset Dictionary Support
                    if (preset_dictionary_flag) {
                        self._inPos += 4;
                    }
                }

                bool last_block = false;

                while (!last_block) {
                    last_block = (self.GetBits(1) == 1);
                    byte block_type = (byte)self.GetBits(2);
                    switch (block_type) {
                        case 0:
                            self.NoCompression();
                            break;
                        case 1:
                            self.FixedCodes();
                            break;
                        case 2:
                            self.DynamicCodes();
                            break;
                        case 3:
                            throw new DataError("invalid block type");
                    }
                }

                return Inflate.Close(self);
            }

            private void DynamicCodes() {
                byte[] order = { 16, 17, 18, 0, 8, 7, 9, 6, 10, 5, 11, 4, 12, 3, 13, 2, 14, 1, 15 };
                int nlen = (int)GetBits(5) + 257;
                int ndist = (int)GetBits(5) + 1;
                int ncode = (int)GetBits(4) + 4;

                List<int> lengths = new List<int>();

                _dynamicLengthCodes = new HuffmanTree();
                _dynamicDistanceCodes = new HuffmanTree();

                if (nlen > MAXLCODES || ndist > MAXDCODES) {
                    throw new DataError("too many length or distance codes");
                }

                int idx = 0;

                while (idx < ncode) {
                    SetOrExpand(lengths, order[idx], GetBits(3));
                    idx++;
                }

                while (idx < 19) {
                    SetOrExpand(lengths, order[idx], 0);
                    idx++;
                }

                int err = ConstructTree(_dynamicLengthCodes, lengths, 18);
                if (err != 0) {
                    throw new DataError("code lengths codes incomplete");
                }

                idx = 0;

                while (idx < (nlen + ndist)) {
                    int symbol = Decode(_dynamicLengthCodes);
                    if (symbol < 16) {
                        SetOrExpand(lengths, idx, symbol);
                        idx++;
                    } else {
                        int len = 0;
                        if (symbol == 16) {
                            if (idx == 0) {
                                throw new DataError("repeat lengths with no first length");
                            }
                            len = lengths[idx - 1];
                            symbol = 3 + (int)GetBits(2);
                        } else if (symbol == 17) {
                            symbol = 3 + (int)GetBits(3);
                        } else if (symbol == 18) {
                            symbol = 11 + (int)GetBits(7);
                        } else {
                            throw new DataError("invalid repeat length code");
                        }

                        if ((idx + symbol) > (nlen + ndist)) {
                            throw new DataError("repeat more than specified lengths");
                        }

                        while (symbol != 0) {
                            SetOrExpand(lengths, idx, len);
                            idx++;
                            symbol--;
                        }
                    }
                }

                err = ConstructTree(_dynamicLengthCodes, lengths, nlen - 1);
                if (err < 0 || (err > 0 && (nlen - _dynamicLengthCodes.Count[0] != 1))) {
                    throw new DataError("invalid literal/length code lengths");
                }

                lengths.RemoveRange(0, nlen);

                err = ConstructTree(_dynamicDistanceCodes, lengths, ndist - 1);
                if (err < 0 || (err > 0 && (ndist - _dynamicDistanceCodes.Count[0] != 1))) {
                    throw new DataError("invalid distance code lengths");
                }

                Codes(_dynamicLengthCodes, _dynamicDistanceCodes);
            }

            [RubyMethod("close")]
            public static MutableString/*!*/ Close(Inflate/*!*/ self) {
                return MutableString.CreateBinary(self._outputBuffer);
            }

            private void NoCompression() {
                _bitBucket = 0;
                _bitCount = 0;

                if (_inPos + 4 > _inputBuffer.Count) {
                    throw new DataError("not enough input to read length code");
                }

                int length = (int)(_inputBuffer[++_inPos] | (_inputBuffer[++_inPos] << 8));
                int lengthComplement = (int)(_inputBuffer[++_inPos] | (_inputBuffer[++_inPos] << 8));
                if (unchecked((ushort)length) != unchecked((ushort)(~lengthComplement))) {
                    throw new DataError("invalid stored block lengths");
                }

                if (_inPos + length > _inputBuffer.Count) {
                    throw new DataError("ran out of input");
                }

                _outputBuffer.AddRange(_inputBuffer.GetRange(_inPos + 1, length));
                _inPos += length;
                _outPos += length;
            }

            private void FixedCodes() {
                if (_fixedLengthCodes == null && _fixedDistanceCodes == null) {
                    GenerateHuffmans();
                }
                Codes(_fixedLengthCodes, _fixedDistanceCodes);
            }

            private void GenerateHuffmans() {
                List<int> lengths = new List<int>(300);
                int x = 0;
                for (; x < 144; x++) {
                    lengths.Add(8);
                }
                for (; x < 256; x++) {
                    lengths.Add(9);
                }
                for (; x < 280; x++) {
                    lengths.Add(7);
                }
                for (; x < 288; x++) {
                    lengths.Add(8);
                }
                _fixedLengthCodes = new HuffmanTree();
                ConstructTree(_fixedLengthCodes, lengths, 287);

                lengths.Clear();

                for (int y = 0; y < 30; y++) {
                    lengths.Add(5);
                }

                _fixedDistanceCodes = new HuffmanTree();
                ConstructTree(_fixedDistanceCodes, lengths, 29);
            }

            private int ConstructTree(HuffmanTree/*!*/ tree, List<int>/*!*/ lengths, int symbols) {
                List<int> offs = new List<int>();

                for (int x = 0; x <= MAXBITS; x++) {
                    SetOrExpand(tree.Count, x, 0);
                }

                for (int y = 0; y <= symbols; y++) {
                    (tree.Count[lengths[y]])++;
                }

                if (tree.Count[0] == symbols) {
                    return 0;
                }

                int left = 1;
                for (int y = 1; y <= MAXBITS; y++) {
                    left <<= 1;
                    left -= tree.Count[y];
                    if (left < 0) {
                        return left;
                    }
                }

                offs.Add(0);
                offs.Add(0);

                for (int len = 1; len <= MAXBITS - 1; len++) {
                    offs.Add(0);
                    offs[len + 1] = offs[len] + tree.Count[len];
                }

                for (int symbol = 0; symbol <= symbols; symbol++) {
                    if (lengths[symbol] != 0) {
                        SetOrExpand(tree.Symbol, offs[lengths[symbol]], symbol);
                        offs[lengths[symbol]]++;
                    }
                }

                return left;
            }

            private void SetOrExpand<T>(List<T>/*!*/ list, int index, T item) {
                int minCount = index + 1;
                int expand = minCount - list.Count;
                while (expand > 0) {
                    list.Add(default(T));
                    expand--;
                }
                list[index] = item;
            }

            private int Codes(HuffmanTree/*!*/ lengthCodes, HuffmanTree/*!*/ distanceCodes) {
                int[] lens = { 3, 4, 5, 6, 7, 8, 9, 10, 11, 13, 15, 17, 19, 23, 27, 31, 35, 43, 51, 59, 67, 83, 99, 115, 131, 163, 195, 227, 258 };
                int[] lext = { 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 0 };
                int[] dists = { 1, 2, 3, 4, 5, 7, 9, 13, 17, 25, 33, 49, 65, 97, 129, 193, 257, 385, 513, 769, 1025, 1537, 2049, 3073, 4097, 6145, 8193, 12289, 16385, 24577 };
                int[] dext = { 0, 0, 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12, 12, 13, 13 };

                int symbol = 0;
                while (symbol != 256) {
                    symbol = Decode(lengthCodes);
                    if (symbol < 0) {
                        return symbol;
                    }
                    if (symbol < 256) {
                        SetOrExpand(_outputBuffer, ++_outPos, (byte)symbol);
                    }
                    if (symbol > 256) {
                        symbol -= 257;
                        if (symbol >= 29) {
                            throw new DataError("invalid literal/length or distance code in fixed or dynamic block");
                        }
                        int len = lens[symbol] + GetBits((byte)lext[symbol]);
                        symbol = Decode(distanceCodes);
                        if (symbol < 0) {
                            return symbol;
                        }
                        int dist = dists[symbol] + GetBits((byte)dext[symbol]);
                        if (dist > _outputBuffer.Count) {
                            throw new DataError("distance is too far back in fixed or dynamic block");
                        }
                        while (len > 0) {
                            SetOrExpand(_outputBuffer, ++_outPos, _outputBuffer[_outPos - dist]);
                            len--;
                        }
                    }
                }

                return 0;
            }

            private int Decode(HuffmanTree/*!*/ tree) {
                int code = 0;
                int first = 0;
                int index = 0;
                for (int len = 1; len <= 15; len++) {
                    code |= GetBits(1);
                    int count = tree.Count[len];
                    if (code < (first + count)) {
                        return tree.Symbol[index + (code - first)];
                    }
                    index += count;
                    first += count;
                    first <<= 1;
                    code <<= 1;
                }

                return -9;
            }

            [RubyMethod("inflate", RubyMethodAttributes.PublicSingleton)]
            public static MutableString InflateStream(
                CallSiteStorage<Func<CallSite, RubyContext, RubyClass, object>>/*!*/ allocateStorage,
                CallSiteStorage<Func<CallSite, RubyContext, object, MutableString, MutableString>>/*!*/ inflateStorage,
                RubyClass/*!*/ self, MutableString zstring) {

                var allocateSite = allocateStorage.GetCallSite("allocate", 0);
                object obj = allocateSite.Target(allocateSite, self.Context, self);

                var inflateSite = inflateStorage.GetCallSite("inflate", 1);
                return inflateSite.Target(inflateSite, self.Context, obj, zstring);
            }

            internal class HuffmanTree {
                internal List<int>/*!*/ Count;
                internal List<int>/*!*/ Symbol;

                internal HuffmanTree() {
                    Count = new List<int>();
                    Symbol = new List<int>();
                }
            }
        }

        #endregion

        #region GzipFile class

        [RubyClass("GzipFile")]
        public class GZipFile {
            protected List<byte>/*!*/ _inputBuffer;
            protected List<byte>/*!*/ _outputBuffer;
            protected int _outPos;
            protected int _inPos;

            public GZipFile() {
                _inputBuffer = new List<byte>();
                _outputBuffer = new List<byte>();
                _outPos = -1;
                _inPos = -1;
            }

            [RubyClass("Error")]
            public class Error : RuntimeError {
                public Error(string message)
                    : base(message) {
                }
            }

            // TODO: missing NoFooter, LengthError, CRCError constants
        }

        #endregion

        #region GzipReader class

        /* [RubyClass("GzipReader"), Includes(typeof(Enumerable))] */
        [RubyClass("GzipReader")]
        public class GZipReader : GZipFile {

            protected MutableString _xtraField;
            protected MutableString/*!*/ _contents;
            protected MutableString/*!*/ _originalName;
            protected MutableString/*!*/ _comment;
            protected ushort _headerCrc;

            [RubyMethod("xtra_field")]
            public static MutableString ExtraField(GZipReader/*!*/ self) {
                return self._xtraField;
            }

            [RubyMethod("original_name")]
            public static MutableString/*!*/ OriginalName(GZipReader/*!*/ self) {
                return self._originalName;
            }

            [RubyMethod("comment")]
            public static MutableString/*!*/ Comment(GZipReader/*!*/ self) {
                return self._comment;
            }

            [RubyConstant("OSES")]
            public static string[] OSES = {
                "FAT filesystem", 
			    "Amiga", 
			    "VMS (or OpenVMS)", 
			    "Unix", 
			    "VM/CMS", 
			    "Atari TOS", 
			    "HPFS fileystem (OS/2, NT)", 
			    "Macintosh", 
			    "Z-System",
			    "CP/M",
			    "TOPS-20",
			    "NTFS filesystem (NT)",
			    "QDOS",
			    "Acorn RISCOS",
			    "unknown"
            };

            private bool IsBitSet(byte b, byte bit) {
                return ((b & (1 << bit)) == (1 << bit));
            }

            [RubyConstructor]
            public static GZipReader/*!*/ Create(RubyClass/*!*/ self, [NotNull]RubyIO/*!*/ io) {
                using (BinaryReader reader = io.GetBinaryReader()) {
                    return new GZipReader(reader);
                }
            }

            [RubyConstructor]
            public static GZipReader/*!*/ Create(RespondToStorage/*!*/ respondToStorage, RubyClass/*!*/ self, object io) {
                Stream stream = null;
                if (io != null) {
                    stream = RubyIOOps.CreateIOWrapper(respondToStorage, self.Context, io, FileAccess.Read);
                }
                if (stream == null || !stream.CanRead) {
                    throw RubyExceptions.CreateTypeError("instance of IO needed");
                }

                using (BinaryReader reader = new BinaryReader(stream)) {
                    return new GZipReader(reader);
                }
            }

            private static ushort ReadUInt16LE(BinaryReader/*!*/ reader) {
                return (ushort)(
                    (ushort)(reader.ReadByte()) |
                    (((ushort)(reader.ReadByte())) << 8)
                    );
            }

            private static uint ReadUInt32LE(BinaryReader/*!*/ reader) {
                return (uint)(
                    (uint)(reader.ReadByte()) |
                    (((uint)(reader.ReadByte())) << 8) |
                    (((uint)(reader.ReadByte())) << 16) |
                    (((uint)(reader.ReadByte())) << 24)
                    );
            }

            private static MutableString/*!*/ ReadStringZ(BinaryReader/*!*/ reader) {
                List<byte> result = new List<byte>();
                byte c;
                while ((c = reader.ReadByte()) != 0) {
                    result.Add(c);
                }
                return MutableString.CreateBinary(result);
            }

            private static MutableString/*!*/ ReadToEnd(BinaryReader/*!*/ reader) {
                List<byte> result = new List<byte>();
                try {
                    while (true) {
                        result.Add(reader.ReadByte());
                    }
                } catch (EndOfStreamException) {
                }
                return MutableString.CreateBinary(result);
            }

            public GZipReader(BinaryReader/*!*/ reader)
                : base() {

                // TODO: should all of this code be moved to open()?
                if (ReadUInt16LE(reader) != 0x8b1f) {
                    throw new Error("not in gzip format");
                }
                if (reader.ReadByte() != 0x08) {
                    throw new Error("unknown compression method");
                }

                byte flg = reader.ReadByte();
                bool ftext = IsBitSet(flg, 0);
                bool fhcrc = IsBitSet(flg, 1);
                bool fextra = IsBitSet(flg, 2);
                bool fname = IsBitSet(flg, 3);
                bool fcomment = IsBitSet(flg, 4);

                uint secondsSince1970 = ReadUInt32LE(reader);
                DateTime mtime = TimeOps.Create(null, (double)secondsSince1970);
                byte xfl = reader.ReadByte();
                string os = GZipReader.OSES[reader.ReadByte()];
                if (fextra) {
                    int xlen = ReadUInt16LE(reader);
                    _xtraField = MutableString.CreateBinary(reader.ReadBytes(xlen));
                }

                if (fname) {
                    _originalName = ReadStringZ(reader);
                } else {
                    _originalName = MutableString.CreateBinary();
                }

                if (fcomment) {
                    _comment = ReadStringZ(reader);
                } else {
                    _comment = MutableString.CreateBinary();
                }

                if (fhcrc) {
                    _headerCrc = ReadUInt16LE(reader);
                }

                _contents = ReadToEnd(reader);
            }

            [RubyMethod("read")]
            public static MutableString/*!*/ Read(GZipReader/*!*/ self) {
                Inflate z = new Inflate(-MAX_WBITS);
                return Inflate.InflateStream(z, self._contents);
            }

            [RubyMethod("open", RubyMethodAttributes.PrivateInstance)]
            public static GZipReader/*!*/ Open(GZipReader/*!*/ self) {
                // TODO: Open as an private instance method probably doesn't create a new GzipReader, right?
                // it probably returns nothing and is used internally to do all initialization
                return self;
            }

            [RubyMethod("open", RubyMethodAttributes.PublicSingleton)]
            public static GZipReader/*!*/ Open(RubyClass/*!*/ self, [DefaultProtocol, NotNull]MutableString/*!*/ path) {
                return Create(self, new RubyFile(self.Context, path.ConvertToString(), RubyFileMode.RDONLY));
            }

            [RubyMethod("open", RubyMethodAttributes.PublicSingleton)]
            public static object Open([NotNull]BlockParam/*!*/ block, RubyClass/*!*/ self, [DefaultProtocol, NotNull]MutableString/*!*/ path) {
                GZipReader reader = Open(self, path);
                object blockResult;
                block.Yield(reader, out blockResult);
                return blockResult;
            }

            [RubyMethod("close")]
            public static GZipReader/*!*/ Close(GZipReader/*!*/ self) {
                return self;
            }
        }

        #endregion

        #region DataError class

        [RubyException("DataError"), Serializable]
        public class DataError : RuntimeError {
            public DataError() : this(null, null) { }
            public DataError(string message) : this(message, null) { }
            public DataError(string message, Exception inner) : base(message ?? "SignalException", inner) { }

#if !SILVERLIGHT
            protected DataError(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
                : base(info, context) { }
#endif
        }

        #endregion
    }
}
