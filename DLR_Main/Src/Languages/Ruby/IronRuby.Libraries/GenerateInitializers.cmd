@echo off
if "%1" == "4" (
  set DIR=%MERLIN_ROOT%\Bin\Debug
) else (
  set DIR=%MERLIN_ROOT%\Bin\V4 Debug
)

"%DIR%\ClassInitGenerator" "%DIR%\IronRuby.Libraries.dll" /libraries:IronRuby.Builtins;IronRuby.StandardLibrary.Threading;IronRuby.StandardLibrary.Sockets;IronRuby.StandardLibrary.OpenSsl;IronRuby.StandardLibrary.Digest;IronRuby.StandardLibrary.Zlib;IronRuby.StandardLibrary.StringIO;IronRuby.StandardLibrary.StringScanner;IronRuby.StandardLibrary.Enumerator;IronRuby.StandardLibrary.FunctionControl;IronRuby.StandardLibrary.FileControl;IronRuby.StandardLibrary.BigDecimal;IronRuby.StandardLibrary.Iconv;IronRuby.StandardLibrary.ParseTree;IronRuby.StandardLibrary.Open3 /out:%~dp0\Initializers.Generated.cs
