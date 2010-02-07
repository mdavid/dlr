class Helper
  def self.non_mangled_methods
    [ "Class", "Clone", "Display", "Dup", "Extend", "Freeze", "Hash", "Initialize", "Inspect", "InstanceEval", "InstanceExec", "InstanceVariableGet", "InstanceVariableSet", "InstanceVariables", "Method", "Methods", "ObjectId", "PrivateMethods", "ProtectedMethods", "PublicMethods", "Send", "SingletonMethods", "Taint", "Untaint", ]
  end

  def self.define_clr_method(name)
    "public string #{name}(){return \"#{name}\";}\n    "
  end

  def self.define_static_clr_method(name)
    "public static string #{name}(){return \"#{name}\";}\n    "
  end
end
csc <<-EOL
  namespace CLRNew {
    public class Ctor {
      public int Tracker {get; set;}

      public Ctor() {
        Tracker = 1; 
      }
    }
  }
  public class PublicNameHolder {
    #{
      %w{a A Unique snake_case CamelCase Mixed_Snake_case CAPITAL PartialCapitalID PartialCapitalId __LeadingCamelCase __leading_snake_case foNBar fNNBar NNNBar MyUIApp MyIdYA NaN NaNa}.inject("") do |res, name|
        res << Helper.define_clr_method(name)
      end
    }
  }

  public class StaticNameHolder {
    #{
      %w{a A Unique snake_case CamelCase Mixed_Snake_case CAPITAL PartialCapitalID PartialCapitalId __LeadingCamelCase __leading_snake_case foNBar fNNBar NNNBar MyUIApp MyIdYA NaN NaNa}.inject("") do |res, name|
        res << Helper.define_static_clr_method(name)
      end
    }
  }

  public class SubPublicNameHolder : PublicNameHolder {}

  public class SubStaticNameHolder : StaticNameHolder {}
    
  public class MangledBase {}
  public class NotMangled : MangledBase {
  #{ 
        Helper.non_mangled_methods.inject("") do |result, name|
          result << Helper.define_clr_method(name)
        end
  }
  }

  public class SubNotMangled : NotMangled {}
  
  public class StaticNotMangled : MangledBase {
  #{ 
        Helper.non_mangled_methods.inject("") do |result, name|
          result << Helper.define_static_clr_method(name)
        end
  }
  }

  public class SubStaticNotMangled : StaticNotMangled {}

  public static class ObjectExtensions {
    public static bool IsNotNull(this object value){
      return value != null;
    }

    public static bool IsNull(this object value){
      return value == null;
    }
  }
EOL
no_csc do
  class CLRNew::Ctor
    def initialize
      tracker = 2
    end
  end

  class MangledBase
    Helper.non_mangled_methods.each do |method|
      define_method(method.to_snake_case) { "base #{method}"}
    end
  end
end


