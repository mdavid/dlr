require File.dirname(__FILE__) + '/../../spec_helper'

@methods_string = <<-EOL
  #region private methods
  private string Private1Generic0Arg<T>() {
    return "private generic no args";
  }
  
  private string Private1Generic1Arg<T>(T arg0) {
    return Public1Generic1Arg<T>(arg0);
  }

  private string Private1Generic2Arg<T>(T arg0, string arg1) {
    return Public1Generic2Arg<T>(arg0, arg1);
  }

  private string Private2Generic2Arg<T, U>(T arg0, U arg1) {
    return Public2Generic2Arg<T, U>(arg0, arg1);
  }

  private string Private2Generic3Arg<T, U>(T arg0, U arg1, string arg2) {
    return Public2Generic3Arg<T, U>(arg0, arg1, arg2);
  }

  private string Private3Generic3Arg<T, U, V>(T arg0, U arg1, V arg2) {
    return Public3Generic3Arg<T, U, V>(arg0, arg1, arg2);
  }

  private string Private3Generic4Arg<T, U, V>(T arg0, U arg1, V arg2, string arg3) {
    return Public3Generic4Arg<T, U, V>(arg0, arg1, arg2, arg3);
  }
  #endregion
  
  #region protected methods
  protected string Protected1Generic0Arg<T>() {
    return "protected generic no args";
  }
  
  protected string Protected1Generic1Arg<T>(T arg0) {
    return Public1Generic1Arg<T>(arg0);
  }

  protected string Protected1Generic2Arg<T>(T arg0, string arg1) {
    return Public1Generic2Arg<T>(arg0, arg1);
  }

  protected string Protected2Generic2Arg<T, U>(T arg0, U arg1) {
    return Public2Generic2Arg<T, U>(arg0, arg1);
  }

  protected string Protected2Generic3Arg<T, U>(T arg0, U arg1, string arg2) {
    return Public2Generic3Arg<T, U>(arg0, arg1, arg2);
  }

  protected string Protected3Generic3Arg<T, U, V>(T arg0, U arg1, V arg2) {
    return Public3Generic3Arg<T, U, V>(arg0, arg1, arg2);
  }

  protected string Protected3Generic4Arg<T, U, V>(T arg0, U arg1, V arg2, string arg3) {
    return Public3Generic4Arg<T, U, V>(arg0, arg1, arg2, arg3);
  }
  #endregion
 
  #region public methods
  public string Public1Generic0Arg<T>() {
    return "public generic no args";
  }

  public string Public1Generic1Arg<T>(T arg0) {
    return arg0.ToString();
  }

  public string Public1Generic2Arg<T>(T arg0, string arg1) {
    return System.String.Format("{0} {1}", arg0, arg1);
  }

  public string Public2Generic2Arg<T, U>(T arg0, U arg1) {
    return Public1Generic2Arg<T>(arg0, arg1.ToString());
  }

  public string Public2Generic3Arg<T, U>(T arg0, U arg1, string arg2) {
    return System.String.Format("{0} {1} {2}", arg0, arg1, arg2);
  }

  public string Public3Generic3Arg<T, U, V>(T arg0, U arg1, V arg2) {
    return Public2Generic3Arg<T, U>(arg0, arg1, arg2.ToString());
  }

  public string Public3Generic4Arg<T, U, V>(T arg0, U arg1, V arg2, string arg3) {
    return System.String.Format("{0} {1} {2} {3}", arg0, arg1, arg2, arg3);
  }
  #endregion
  
  #region Constrained methods
  public T StructConstraintMethod<T>(T arg0)
  where T : struct {
    return arg0;
  }

  public T ClassConstraintMethod<T>(T arg0)
  where T : class {
    return arg0;
  }

  public T ConstructorConstraintMethod<T>()
  where T : new() {
    return new T();
  }

  public T TypeConstraintMethod<T, TBase>(T arg0)
  where T : TBase {
    return arg0;
  }
  #endregion
EOL

@conflicting_method_string = <<-EOL
  public string Public1Generic2Arg<T>(T arg0, K arg1) {
    return Public2Generic2Arg<T, K>(arg0, arg1);
  }
  
  public string ConflictingGenericMethod<K>(K arg0) {
    return arg0.ToString();
  }
EOL
describe :generic_methods, :shared => true do
  it "are callable via call and [] when pubic or protected" do
    @klass.method(:public_1_generic_0_arg).of(Fixnum).call.to_s.should == "public generic no args"
    (@public_method_list + @protected_method_list).each do |m|
      generic_count, arity = m.match(/_(\d)_generic_(\d)_/)[1..2].map {|e| e.to_i}
      generics = Array.new(generic_count, Fixnum)
      args = Array.new(arity, 1)
      args << args.pop.to_s.to_clr_string if arity > generic_count

      @klass.method(m).of(*generics).call(*args).to_s.should == args.join(" ")
      @klass.method(m).of(*generics)[*args].to_s.should == args.join(" ")
    end
  end

  it "binds struct constraints correctly" do 
    @klass.method(:struct_constraint_method).of(Fixnum).call(1).should == 1
  end

  it "binds class constraints correctly" do
    @klass.method(:class_constraint_method).of(String).call("a").should == "a"
  end

  it "binds constructor constraints correctly" do
    @klass.method(:constructor_constraint_method).of(Klass).call.foo.should == 10
  end

  it "binds secondary type constraints correctly" do
    @klass.method(:type_constraint_method).of(SubKlass, Klass).call(SubKlass.new).foo.should == 10
  end

  if IronRuby.dlr_config.private_binding
    it "are callable via call and [] when private" do
      @private_method_list.each do |m|
        generic_count, arity = m.match(/_(\d)_generic_(\d)_/)[1..2].map {|e| e.to_i}
        generics = Array.new(generic_count, Fixnum)
        args = Array.new(arity, 1)
        args << args.pop.to_s.to_clr_string if arity > generic_count

        @klass.method(m).of(*generics).call(*args).to_s.should == args.join(" ")
        @klass.method(m).of(*generics)[*args].to_s.should == args.join(" ")
      end
    end
  end

  it "cannot be called directly" do
    (@public_method_list + @protected_method_list).each do |m|
      generic_count, arity = m.match(/_(\d)_generic_(\d)_/)[1..2].map {|e| e.to_i}
      args = Array.new(arity, 1)
      args << args.pop.to_s.to_clr_string if arity > generic_count
      lambda {@klass.send(m, *args)}.should raise_error(ArgumentError)
    end
  end

  it "has proper errors for constrained generics" do
    lambda { @klass.method(:struct_constraint_method).of(String).call("a")}.should raise_error(ArgumentError)
    lambda { @klass.method(:class_constraint_method).of(Fixnum).call(1)}.should raise_error(ArgumentError)
    lambda { @klass.method(:constructor_constraint_method).of(ClrString).call}.should raise_error(ArgumentError)
    lambda { @klass.method(:type_constraint_method).of(String, Klass).call("a")}.should raise_error(ArgumentError)
  end

  it "can use Ruby types for constrained generics" do
    class Foo
      attr_reader :foo
      def initialize
        @foo = 10
      end
    end

    class SubFoo < Foo
    end
    @klass.method(:constructor_constraint_method).of(Foo).call.foo.should == 10
    @klass.method(:type_constraint_method).of(SubFoo, Foo).call(SubFoo.new).foo.should == 10
  end

  it "has proper error messages for skipping generic" do
    lambda {@klass.method(:public_1_generic_1_arg).call("a")}.should raise_error(ArgumentError, /generic/i)
  end

  it "has proper error messages for incorrect number of arguments" do
    lambda {@klass.method(:public_1_generic_2_arg).of(Fixnum).call(1)}.should raise_error(ArgumentError, /1 for 2/)
  end
end

describe :generic_conflicting_methods, :shared => true do
  it "binds class type parameter correctly" do
    @klass.method(:public_1_generic_2_arg).of(String).call("hello", 1).to_s.should == "hello 1"
  end

  it "binds conflicting type parameter correctly" do
    @klass.method(:conflicting_generic_method).of(String).call("hello").to_s.should == "hello"
  end
end
describe "Generic methods" do
  describe "on regular classes" do
    csc <<-EOL
    public partial class ClassWithMethods {
      #{@methods_string}
    }

    public partial class Klass {
      private int _foo;
      
      public int Foo {
        get { return _foo; }
      }

      public Klass() {
        _foo = 10;
      }
    }

    public partial class SubKlass : Klass {}
    EOL
    before :each do
      @klass = ClassWithMethods.new
      @public_method_list = %w{public_1_generic_1_arg public_1_generic_2_arg
                        public_2_generic_2_arg public_2_generic_3_arg
                        public_3_generic_3_arg public_3_generic_3_arg}
      @private_method_list = %w{private_1_generic_1_arg private_1_generic_2_arg
                        private_2_generic_2_arg private_2_generic_3_arg
                        private_3_generic_3_arg private_3_generic_3_arg}
      @protected_method_list = %w{protected_1_generic_1_arg protected_1_generic_2_arg
                        protected_2_generic_2_arg protected_2_generic_3_arg
                        protected_3_generic_3_arg protected_3_generic_3_arg}
    end
    it_behaves_like :generic_methods, Object.new

  end

  describe "on generic classes with one parameter" do
    csc <<-EOL
    #pragma warning disable 693
    public partial class GenericClassWithMethods<K> {
    #{@methods_string}
    #{@conflicting_method_string}
    }
    #pragma warning restore 693
    EOL
    before :each do
      @klass = GenericClassWithMethods.of(Fixnum).new
      @public_method_list = %w{public_1_generic_1_arg public_1_generic_2_arg
                        public_2_generic_2_arg public_2_generic_3_arg
                        public_3_generic_3_arg public_3_generic_3_arg}
      @private_method_list = %w{private_1_generic_1_arg private_1_generic_2_arg
                        private_2_generic_2_arg private_2_generic_3_arg
                        private_3_generic_3_arg private_3_generic_3_arg}
      @protected_method_list = %w{protected_1_generic_1_arg protected_1_generic_2_arg
                        protected_2_generic_2_arg protected_2_generic_3_arg
                        protected_3_generic_3_arg protected_3_generic_3_arg}
    end
    it_behaves_like :generic_methods, Object.new
    it_behaves_like :generic_conflicting_methods, Object.new
  end

  describe "on generic classes with 2 parameters" do
    csc <<-EOL
    #pragma warning disable 693
    public partial class GenericClass2Params<K, J> {
    #{@methods_string}
    #{@conflicting_method_string}
    }
    #pragma warning restore 693
    EOL
    before :each do
      @klass = GenericClass2Params.of(Fixnum, String).new
      @public_method_list = %w{public_1_generic_1_arg public_1_generic_2_arg
                        public_2_generic_2_arg public_2_generic_3_arg
                        public_3_generic_3_arg public_3_generic_3_arg}
      @private_method_list = %w{private_1_generic_1_arg private_1_generic_2_arg
                        private_2_generic_2_arg private_2_generic_3_arg
                        private_3_generic_3_arg private_3_generic_3_arg}
      @protected_method_list = %w{protected_1_generic_1_arg protected_1_generic_2_arg
                        protected_2_generic_2_arg protected_2_generic_3_arg
                        protected_3_generic_3_arg protected_3_generic_3_arg}
    end
    it_behaves_like :generic_methods, Object.new
    it_behaves_like :generic_conflicting_methods, Object.new
  end
end
