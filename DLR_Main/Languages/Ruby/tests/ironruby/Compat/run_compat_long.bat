REM ****************************************************************************
REM
REM Copyright (c) Microsoft Corporation. 
REM
REM This source code is subject to terms and conditions of the Microsoft Public License. A 
REM copy of the license can be found in the License.html file at the root of this distribution. If 
REM you cannot locate the  Microsoft Public License, please send an email to 
REM ironruby@microsoft.com. By using this source code in any fashion, you are agreeing to be bound 
REM by the terms of the Microsoft Public License.
REM
REM You must not remove this notice, or any other, from this software.
REM
REM
REM ****************************************************************************

if "%1"=="" (
    set FILELIST=gen_assignment_long,gen_exception1_long,gen_exception2_long,gen_block_ctrl_flow_long,gen_rescue_clause_long
) ELSE (
    set FILELIST=%1
)

for %%f in (%FILELIST%) do (
    %MERLIN_ROOT%\..\external\Languages\IronPython\25\python.exe %%f.py > %%f.lst
    %MERLIN_ROOT%\..\external\languages\ruby\ruby-1.8.6\bin\ruby.exe run_compat.rb @%%f.lst
    if NOT "%ERRORLEVEL%" == "0" exit /b 1
)

exit /b 0