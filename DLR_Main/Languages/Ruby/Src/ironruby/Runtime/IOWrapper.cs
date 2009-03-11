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
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Runtime.CompilerServices;

using IronRuby.Builtins;
using Microsoft.Scripting.Runtime;
using Microsoft.Scripting.Generation;
using Microsoft.Scripting.Utils;
using IronRuby.Runtime.Calls;

namespace IronRuby.Runtime {

    public class IOWrapper : Stream {
        private static readonly CallSite<Func<CallSite, RubyContext, object, MutableString, object>> _writeSite =
            CallSite<Func<CallSite, RubyContext, object, MutableString, object>>.Create(
                RubyCallAction.Make("write", RubyCallSignature.WithImplicitSelf(1))
            );

        private static readonly CallSite<Func<CallSite, RubyContext, object, int, MutableString>> _readSite =
            CallSite<Func<CallSite, RubyContext, object, int, MutableString>>.Create(
                RubyCallAction.Make("read", RubyCallSignature.WithImplicitSelf(1))
            );

        private static readonly CallSite<Func<CallSite, RubyContext, object, long, int, object>> _seekSite =
            CallSite<Func<CallSite, RubyContext, object, long, int, object>>.Create(
                RubyCallAction.Make("seek", RubyCallSignature.WithImplicitSelf(2))
            );

        private static readonly CallSite<Func<CallSite, RubyContext, object, long>> _tellSite =
            CallSite<Func<CallSite, RubyContext, object, long>>.Create(
                RubyCallAction.Make("tell", RubyCallSignature.WithImplicitSelf(0))
            );

        private readonly RubyContext/*!*/ _context;
        private readonly object _obj;
        private readonly bool _canRead;
        private readonly bool _canWrite;
        private readonly bool _canSeek;
        private readonly byte[]/*!*/ _buffer;
        private int _writePos;
        private int _readPos;
        private int _readLen;

        private const int _bufferSize = 0x1000;

        public IOWrapper(RubyContext/*!*/ context, object io, bool canRead, bool canWrite, bool canSeek) {
            Assert.NotNull(context);

            _context = context;
            _obj = io;

            _canRead = canRead;
            _canWrite = canWrite;
            _canSeek = canSeek;
            _buffer = new byte[_bufferSize];
            _writePos = 0;
            _readPos = 0;
            _readLen = 0;
        }

        public override bool CanRead {
            get { return _canRead; }
        }

        public override bool CanSeek {
            get { return _canSeek; }
        }

        public override bool CanWrite {
            get { return _canWrite; }
        }

        public override long Length {
            get {
                long currentPos = Position;
                Seek(0, SeekOrigin.End);
                long result = Position;
                Position = currentPos;
                return result;
            }
        }

        public override long Position {
            get {
                if (!_canSeek) {
                    throw new NotSupportedException();
                }
                return _tellSite.Target(_tellSite, _context, _obj);
            }
            set {
                if (!_canSeek) {
                    throw new NotSupportedException();
                }
                Seek(value, SeekOrigin.Begin);
            }
        }

        public override void Flush() {
            FlushWrite();
            FlushRead();
        }

        private void FlushWrite() {
            if (_writePos > 0) {
                WriteToObject();
            }
        }

        private void FlushRead() {
            if (_canSeek && (_readPos < _readLen)) {
                Seek(this._readPos - this._readLen, SeekOrigin.Current);
            }
            _readPos = 0;
            _readLen = 0;
        }

        public override int Read(byte[]/*!*/ buffer, int offset, int count) {
            if (!_canRead) {
                throw new NotSupportedException();
            }
            int size = _readLen - _readPos;
            if (size == 0) {
                FlushWrite();
                if (count > _bufferSize) {
                    size = ReadFromObject(buffer, offset, count);
                    _readPos = 0;
                    _readLen = 0;
                    return size;
                }
                size = ReadFromObject(_buffer, 0, _bufferSize);
                if (size == 0) {
                    return 0;
                }
                _readPos = 0;
                _readLen = size;
            }
            if (size > count) {
                size = count;
            }
            Buffer.BlockCopy(_buffer, _readPos, buffer, offset, size);
            _readPos += size;
            if (size < count) {
                int additionalSize = ReadFromObject(buffer, offset + size, count - size);
                size += additionalSize;
                _readPos = 0;
                _readLen = 0;
            }
            return size;
        }

        public override int ReadByte() {
            if (!_canRead) {
                throw new NotSupportedException();
            }
            if (_readPos == _readLen) {
                FlushWrite();

                _readLen = ReadFromObject(_buffer, 0, _bufferSize);
                _readPos = 0;
                if (_readLen == 0) {
                    return -1;
                }

            }
            return _buffer[_readPos++];
        }

        private int ReadFromObject(byte[]/*!*/ buffer, int offset, int count) {
            MutableString result = _readSite.Target(_readSite, _context, _obj, count);
            if (result == null) {
                return 0;
            } else {
                byte[] readdata = result.ConvertToBytes();
                Buffer.BlockCopy(readdata, 0, buffer, offset, readdata.Length);
                return readdata.Length;
            }
        }

        public override long Seek(long offset, SeekOrigin origin) {
            if (!_canSeek) {
                throw new NotSupportedException();
            }

            int rubyOrigin = 0;
            switch (origin) {
                case SeekOrigin.Begin:
                    rubyOrigin = RubyIO.SEEK_SET;
                    break;
                case SeekOrigin.Current:
                    rubyOrigin = RubyIO.SEEK_CUR;
                    break;
                case SeekOrigin.End:
                    rubyOrigin = RubyIO.SEEK_END;
                    break;
            }

            _seekSite.Target(_seekSite, _context, _obj, offset, rubyOrigin);
            return Position;
        }

        public override void SetLength(long value) {
            throw new NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count) {
            if (!_canWrite) {
                throw new NotSupportedException();
            }

            if (_writePos == 0) {
                FlushRead();
            } else {
                int size = _bufferSize - _writePos;
                if (size > 0) {
                    if (size > count) {
                        size = count;
                    }
                    Buffer.BlockCopy(buffer, offset, _buffer, _writePos, size);
                    _writePos += size;
                    if (size == count) {
                        return;
                    }
                    offset += size;
                    count -= size;
                }
                WriteToObject();
            }
            if (count >= _bufferSize) {
                WriteToObject(buffer, offset, count);
            } else if (count > 0) {
                Buffer.BlockCopy(buffer, offset, _buffer, 0, count);
                _writePos = count;
            }
        }

        public override void WriteByte(byte value) {
            if (!_canWrite) {
                throw new NotSupportedException();
            }

            if (_writePos == 0) {
                FlushRead();
            }
            if (_writePos == _bufferSize) {
                WriteToObject();
            }
            _buffer[_writePos++] = value;
        }

        private void WriteToObject() {
            WriteToObject(_buffer, 0, _writePos);
            _writePos = 0;
        }

        private void WriteToObject(byte[] buffer, int offset, int count) {
            if (offset != 0 || count != buffer.Length) {
                byte[] newBuffer = new byte[count];
                Buffer.BlockCopy(buffer, offset, newBuffer, 0, count);
                buffer = newBuffer;
            }
            MutableString argument = MutableString.CreateBinary(buffer, count);
            _writeSite.Target(_writeSite, _context, _obj, argument);
        }
    }
}
