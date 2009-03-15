module Kernel
  alias _require require

  def require(*args)
    debug("require",args)
    nil
  end
end

class Module
  def const_missing(sym)
    debug("module constant missing", sym)
    self
  end
end

class Object
  def method_missing(sym, *args, &block)
    debug("method missing sym", sym)
    debug("method missing args", args)
    debug("method missing block", block)
    debug("method missing caller", caller)
    nil
  end

  def const_missing(sym)
    debug("cmsym", sym)
    Module.new
  end
  
  def debug(header, string)
    puts header << ":" << string.to_s if $DEBUG
  end

  def describe(string)
    debug("describe", string)
    yield
  end

  def assembly(assembly_name, options = {})
    Compiler.switch_to(assembly_name, options)
    yield
    Compiler.compile
    Compiler.switch_back!
  end

  def csc(code)
    debug("csc", code)
    Compiler.code << "//" + caller[0]
    Compiler.code << code.strip
  end
end

class Compiler
  def initialize(target_dir)
    @assemblies = {}
    @target_dir = target_dir
    @current = nil
  end

  def self.create(target_dir)
    @compiler = Compiler.new(target_dir)
  end
  
  def self.method_missing(sym, *args, &block)
    @compiler.send(sym, *args, &block)
  end

  def compile
    @assemblies[@current].compile(@current)
  end

  def code
    @assemblies[@current].code 
  end

  def switch_to(name, options)
    unless @assemblies[name]
      @assemblies[name] = Assembly.new(@target_dir, options)
    end
    @old, @current = @current, name
    @old = @current unless @old
  end

  def switch_back!
    @old, @current = @current, @old
  end
end

class Assembly
  attr_accessor :code
  def initialize(target_dir,options)
    @target_dir = target_dir
    @options = options
    @code = []
  end

  def compile(name)
    Dir.chdir(@target_dir) do
      File.open(name, "w") {|f| f.write @code.join("\n")}
      opts = ""
      if @options[:references]
        opts << " /r:#{@options[:references]}"
      end
      if @options[:target]
        opts << " /t:#{@options[:target]}"
      end
      if @options[:out]
        opts << " /out:#{@options[:out]}"
      end
      cmd = "csc /t:library#{opts} #{name}"
      debug("compile", cmd)
      system cmd
    end
  end
end

Compiler.create(ARGV[0])
assembly("fixtures.generated.cs") do
  Dir.chdir(ARGV[0]) do
    Dir["**/*_spec.rb"].each {|e| debug("file",e) }.each do |file|
      load file
    end
  end
end

