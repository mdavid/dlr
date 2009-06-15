#if CODEPLEX_40
using System;
#else
using System; using Microsoft;
#endif
using System.Collections.Generic;
using System.Text;
using Microsoft.Scripting;
using Microsoft.Scripting.Utils;
using System.IO;

namespace IronRuby.Runtime {
    internal sealed class BinaryContentProvider : StreamContentProvider {
        private readonly byte[]/*!*/ _bytes;

        public BinaryContentProvider(byte[]/*!*/ bytes) {
            Assert.NotNull(bytes);
            _bytes = bytes;
        }

        public override Stream/*!*/ GetStream() {
            return new MemoryStream(_bytes);
        }
    }
}
