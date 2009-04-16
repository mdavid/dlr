# ****************************************************************************
#
# Copyright (c) Microsoft Corporation. 
#
# This source code is subject to terms and conditions of the Microsoft Public License. A 
# copy of the license can be found in the License.html file at the root of this distribution. If 
# you cannot locate the  Microsoft Public License, please send an email to 
# ironruby@microsoft.com. By using this source code in any fashion, you are agreeing to be bound 
# by the terms of the Microsoft Public License.
#
# You must not remove this notice, or any other, from this software.
#
#
# ****************************************************************************

require 'Find'

ARGV.each do |type|
  Find.find "Builtin/#{type}" do |path|
    if ( /_svn/ =~ path )
      Find.prune
    end
    if ( /_spec\.rb/ =~ path ) then
      print path, '(ruby): '
      system("ruby #{path}")
      puts
      print path, '(ir): '
      system("..\\..\\..\\Test\\Scripts\\ir.cmd #{path}")
      puts
    end
  end
end
