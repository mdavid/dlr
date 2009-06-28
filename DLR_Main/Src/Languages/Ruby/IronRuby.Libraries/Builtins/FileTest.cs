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
using IronRuby.Runtime;
using Microsoft.Scripting.Runtime;

namespace IronRuby.Builtins {
    [RubyModule("FileTest")]
    public static class FileTest {
        [RubyMethod("blockdev?", RubyMethodAttributes.PublicSingleton)]
        public static bool IsBlockDevice(RubyModule/*!*/ self, [DefaultProtocol, NotNull]MutableString/*!*/ path) {
            return RubyFileOps.RubyStatOps.IsBlockDevice(RubyFileOps.RubyStatOps.Create(self.Context, path));
        }

        [RubyMethod("chardev?", RubyMethodAttributes.PublicSingleton)]
        public static bool IsCharDevice(RubyModule/*!*/ self, [DefaultProtocol, NotNull]MutableString/*!*/ path) {
            return RubyFileOps.RubyStatOps.IsCharDevice(RubyFileOps.RubyStatOps.Create(self.Context, path));
        }

        [RubyMethod("directory?", RubyMethodAttributes.PublicSingleton)]
        public static bool IsDirectory(RubyModule/*!*/ self, [DefaultProtocol, NotNull]MutableString/*!*/ path) {
            return RubyFileOps.DirectoryExists(self.Context, path.ConvertToString());
        }

        [RubyMethod("executable?", RubyMethodAttributes.PublicSingleton)]
        [RubyMethod("executable_real?", RubyMethodAttributes.PublicSingleton)]
        public static bool IsExecutable(RubyModule/*!*/ self, [DefaultProtocol, NotNull]MutableString/*!*/ path) {
            return RubyFileOps.RubyStatOps.IsExecutable(RubyFileOps.RubyStatOps.Create(self.Context, path));
        }

        [RubyMethod("exist?", RubyMethodAttributes.PublicSingleton)]
        [RubyMethod("exists?", RubyMethodAttributes.PublicSingleton)]
        public static bool Exists(RubyModule/*!*/ self, [DefaultProtocol, NotNull]MutableString/*!*/ path) {
            string strPath = path.ConvertToString();
            return RubyFileOps.FileExists(self.Context, strPath) || RubyFileOps.DirectoryExists(self.Context, strPath);
        }

        [RubyMethod("file?", RubyMethodAttributes.PublicSingleton)]
        public static bool IsAFile(RubyModule/*!*/ self, [DefaultProtocol, NotNull]MutableString/*!*/ path) {
            return RubyFileOps.FileExists(self.Context, path.ConvertToString());
        }

        [RubyMethod("grpowned?", RubyMethodAttributes.PublicSingleton)]
        public static bool IsGroupOwned(RubyModule/*!*/ self, [DefaultProtocol, NotNull]MutableString/*!*/ path) {
            return RubyFileOps.RubyStatOps.IsGroupOwned(RubyFileOps.RubyStatOps.Create(self.Context, path));
        }

        //identical?

        [RubyMethod("owned?", RubyMethodAttributes.PublicSingleton)]
        public static bool IsUserOwned(RubyModule/*!*/ self, [DefaultProtocol, NotNull]MutableString/*!*/ path) {
            return RubyFileOps.RubyStatOps.IsUserOwned(RubyFileOps.RubyStatOps.Create(self.Context, path));
        }

        [RubyMethod("pipe?", RubyMethodAttributes.PublicSingleton)]
        public static bool IsPipe(RubyModule/*!*/ self, [DefaultProtocol, NotNull]MutableString/*!*/ path) {
            return RubyFileOps.RubyStatOps.IsPipe(RubyFileOps.RubyStatOps.Create(self.Context, path));
        }

        [RubyMethod("readable?", RubyMethodAttributes.PublicSingleton)]
        [RubyMethod("readable_real?", RubyMethodAttributes.PublicSingleton)]
        public static bool IsReadable(RubyModule/*!*/ self, [DefaultProtocol, NotNull]MutableString/*!*/ path) {
            return IsReadableImpl(self.Context, path.ConvertToString());
        }

        private static bool IsReadableImpl(RubyContext/*!*/ context, string/*!*/ path) {
            FileSystemInfo fsi;
            if (RubyFileOps.RubyStatOps.TryCreate(context, path, out fsi)) {
                return RubyFileOps.RubyStatOps.IsReadable(fsi);
            } else {
                return false;
            }
        }

        [RubyMethod("setgid?", RubyMethodAttributes.PublicSingleton)]
        public static bool IsSetGid(RubyModule/*!*/ self, [DefaultProtocol, NotNull]MutableString/*!*/ path) {
            return RubyFileOps.RubyStatOps.IsSetGid(RubyFileOps.RubyStatOps.Create(self.Context, path));
        }

        [RubyMethod("setuid?", RubyMethodAttributes.PublicSingleton)]
        public static bool IsSetUid(RubyModule/*!*/ self, [DefaultProtocol, NotNull]MutableString/*!*/ path) {
            return RubyFileOps.RubyStatOps.IsSetUid(RubyFileOps.RubyStatOps.Create(self.Context, path));
        }

        [RubyMethod("size", RubyMethodAttributes.PublicSingleton)]
        public static int Size(RubyModule/*!*/ self, [DefaultProtocol, NotNull]MutableString/*!*/ path) {
            return RubyFileOps.RubyStatOps.Size(RubyFileOps.RubyStatOps.Create(self.Context, path));
        }

        [RubyMethod("size?", RubyMethodAttributes.PublicSingleton)]
        public static object NullableSize(RubyModule/*!*/ self, [DefaultProtocol, NotNull]MutableString/*!*/ path) {
            return RubyFileOps.RubyStatOps.NullableSize(RubyFileOps.RubyStatOps.Create(self.Context, path));
        }

        [RubyMethod("socket?", RubyMethodAttributes.PublicSingleton)]
        public static bool IsSocket(RubyModule/*!*/ self, [DefaultProtocol, NotNull]MutableString/*!*/ path) {
            return RubyFileOps.RubyStatOps.IsSocket(RubyFileOps.RubyStatOps.Create(self.Context, path));
        }

        [RubyMethod("sticky?", RubyMethodAttributes.PublicSingleton)]
        public static bool IsSticky(RubyModule/*!*/ self, [DefaultProtocol, NotNull]MutableString/*!*/ path) {
            return RubyFileOps.RubyStatOps.IsSticky(RubyFileOps.RubyStatOps.Create(self.Context, path));
        }

#if !SILVERLIGHT
        [RubyMethod("symlink?", RubyMethodAttributes.PublicSingleton, BuildConfig = "!SILVERLIGHT")]
        public static bool IsSymLink(RubyModule/*!*/ self, [DefaultProtocol, NotNull]MutableString/*!*/ path) {
            return RubyFileOps.RubyStatOps.IsSymLink(RubyFileOps.RubyStatOps.Create(self.Context, path));
        }
#endif

        [RubyMethod("writable?", RubyMethodAttributes.PublicSingleton)]
        [RubyMethod("writable_real?", RubyMethodAttributes.PublicSingleton)]
        public static bool IsWritable(RubyModule/*!*/ self, [DefaultProtocol, NotNull]MutableString/*!*/ path) {
            return IsWritableImpl(self.Context, path.ConvertToString());
        }

        private static bool IsWritableImpl(RubyContext/*!*/ context, string/*!*/ path) {
            FileSystemInfo fsi;
            if (RubyFileOps.RubyStatOps.TryCreate(context, path, out fsi)) {
                return RubyFileOps.RubyStatOps.IsWritable(fsi);
            } else {
                return false;
            }
        }

        [RubyMethod("zero?", RubyMethodAttributes.PublicSingleton)]
        public static bool IsZeroLength(RubyModule/*!*/ self, [DefaultProtocol, NotNull]MutableString/*!*/ path) {
            string strPath = path.ConvertToString();

            // NUL/nul is a special-cased filename on Windows
            if (strPath.ToLower() == "nul") {
                return RubyFileOps.RubyStatOps.IsZeroLength(RubyFileOps.RubyStatOps.Create(self.Context, strPath));
            }

            if (RubyFileOps.DirectoryExists(self.Context, strPath) || !RubyFileOps.FileExists(self.Context, strPath)) {
                return false;
            }

            return RubyFileOps.RubyStatOps.IsZeroLength(RubyFileOps.RubyStatOps.Create(self.Context, strPath));
        }
    }
}
