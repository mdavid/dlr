class Object
  def metaclass
    class << self; self; end
  end

  def metaclass_eval(&block)
    metaclass.class_eval(&block)
  end

  def metaclass_alias(new,old)
    metaclass_eval { alias_method new, old}
  end

  def metaclass_temp_alias(new, old)
    metaclass_alias new, old
    res = nil
    if block_given?
      res = yield
      metaclass_alias old, new
    end
    res
  end

  def metaclass_def(meth_name)
    metaclass_eval do 
      define_method(meth_name) { yield }
    end
  end
end

class Array
  def to_clr_array(type = Object, convert = nil)
    result = convert ? map {|el| el.send(convert)} : self
    System::Array.of(type).new(result)
  end
end

class String
  def to_snake_case
    gsub(/(.)([A-Z])/) {|el| "#{$1 == "_" ? "" : $1}_#{$2}"}.downcase
  end
end

