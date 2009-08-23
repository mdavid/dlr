require File.dirname(__FILE__) + "/spec_helper"

describe "Regression dev tests" do

end

describe "Command line options" do
  describe "-profile" do
    s = ruby_exe("puts IronRuby::Clr.profile{ require 'rubygems' }.size", :options => "-profile").chomp.to_i
    s.should > 15
    # We check for an upper bound only so that we can increase the lower bound, which is currently lower than we would like it to be
    s.should < 40
  end
end

describe "Enumerable blocks (Codeplex 1301)" do
  before :each do
    @b = [2, 5, 3, 6, 1, 4]  
  end 
  
  it "propagates break out of the block passed to Array#all?" do
    count = 0
    result = @b.all? { |obj| count = count + 1; break "break" }
    count.should == 1
    result.should == "break"
  end

  it "propagates exception out of the block passed to Array#all?" do
    count = 0
    lambda { @b.all? { |obj| count = count + 1; raise "exception" } }.should raise_error(RuntimeError)
    count.should == 1
  end

  it "propagates break out of the block passed to Array#any?" do
    count = 0
    result = @b.any? { |obj| count = count + 1; break "break" }
    count.should == 1
    result.should == "break"
  end

  it "propagates exception out of the block passed to Array#any?" do
    count = 0
    lambda { @b.any? { |obj| count = count + 1; raise "exception" } }.should raise_error(RuntimeError)
    count.should == 1
  end

  it "propagates break out of the block passed to Array#collect" do
    count = 0
    result = @b.collect { |obj| count = count + 1; break "break" }
    count.should == 1
    result.should == "break"
  end

  it "propagates exception out of the block passed to Array#collect" do
    count = 0
    lambda { @b.collect { |obj| count = count + 1; raise "exception" } }.should raise_error(RuntimeError)
    count.should == 1
  end

  it "propagates break out of the block passed to Array#detect" do
    count = 0
    result = @b.detect { |obj| count = count + 1; break "break" }
    count.should == 1
    result.should == "break"
  end

  it "propagates exception out of the block passed to Array#detect" do
    count = 0
    lambda { @b.detect { |obj| count = count + 1; raise "exception" } }.should raise_error(RuntimeError)
    count.should == 1
  end

  it "propagates break out of the block passed to Array#each_with_index" do
    count = 0
    result = @b.each_with_index { |obj| count = count + 1; break "break" }
    count.should == 1
    result.should == "break"
  end

  it "propagates exception out of the block passed to Array#each_with_index" do
    count = 0
    lambda { @b.each_with_index { |obj, i| count = count + 1; raise "exception" } }.should raise_error(RuntimeError)
    count.should == 1
  end

  it "propagates break out of the block passed to Array#find_all" do
    count = 0
    result = @b.find_all { |obj, i| count = count + 1; break "break" }
    count.should == 1
    result.should == "break"
  end

  it "propagates exception out of the block passed to Array#find_all" do
    count = 0
    lambda { @b.find_all { |obj| count = count + 1; raise "exception" } }.should raise_error(RuntimeError)
    count.should == 1
  end

  it "propagates break out of the block passed to Array#grep" do
    count = 0
    result = @b.grep(Fixnum) { |obj| count = count + 1; break "break" }
    count.should == 1
    result.should == "break"
  end

  it "propagates exception out of the block passed to Array#grep" do
    count = 0
    lambda { @b.grep(Fixnum) { |obj| count = count + 1; raise "exception" } }.should raise_error(RuntimeError)
    count.should == 1
  end

  it "propagates break out of the block passed to Array#inject" do
    count = 0
    result = @b.inject { |memo, obj| count = count + 1; break "break" }
    count.should == 1
    result.should == "break"
  end

  it "propagates exception out of the block passed to Array#inject" do
    count = 0
    lambda { @b.inject { |memo, obj| count = count + 1; raise "exception" } }.should raise_error(RuntimeError)
    count.should == 1
  end

  it "propagates break out of the block passed to Array#max" do
    count = 0
    result = @b.max { |a, b| count = count + 1; break "break" }
    count.should == 1
    result.should == "break"
  end

  it "propagates exception out of the block passed to Array#max" do
    count = 0
    lambda { @b.max { |a, b| count = count + 1; raise "exception" } }.should raise_error(RuntimeError)
    count.should == 1
  end

  it "propagates break out of the block passed to Array#min" do
    count = 0
    result = @b.min { |a, b| count = count + 1; break "break" }
    count.should == 1
    result.should == "break"
  end

  it "propagates exception out of the block passed to Array#min" do
    count = 0
    lambda { @b.min { |a, b| count = count + 1; raise "exception" } }.should raise_error(RuntimeError)
    count.should == 1
  end

  it "propagates break out of the block passed to Array#partition" do
    count = 0
    result = @b.partition { |obj| count = count + 1; break "break" }
    count.should == 1
    result.should == "break"
  end

  it "propagates exception out of the block passed to Array#partition" do
    count = 0
    lambda { @b.partition { |obj| count = count + 1; raise "exception" } }.should raise_error(RuntimeError)
    count.should == 1
  end

  it "propagates break out of the block passed to Array#reject" do
    count = 0
    result = @b.reject { |obj| count = count + 1; break "break" }
    count.should == 1
    result.should == "break"
  end

  it "propagates exception out of the block passed to Array#reject" do
    count = 0
    lambda { @b.reject { |obj| count = count + 1; raise "exception" } }.should raise_error(RuntimeError)
    count.should == 1
  end

  it "propagates break out of the block passed to Array#sort" do
    count = 0
    result = @b.sort { |a, b| count = count + 1; break "break" }
    count.should == 1
    result.should == "break"
  end

  it "propagates exception out of the block passed to Array#sort" do
    count = 0
    lambda { @b.sort { |a, b| count = count + 1; raise "exception" } }.should raise_error(RuntimeError)
    count.should == 1
  end

  it "propagates break out of the block passed to Array#sort_by" do
    count = 0
    result = @b.sort_by { |obj| count = count + 1; break "break" }
    count.should == 1
    result.should == "break"
  end

  it "propagates exception out of the block passed to Array#sort_by" do
    count = 0
    lambda { @b.sort_by { |obj| count = count + 1; raise "exception" } }.should raise_error(RuntimeError)
    count.should == 1
  end

  it "propagates break out of the block passed to Array#zip" do
    count = 0
    result = @b.zip { |arr| count = count + 1; break "break" }
    count.should == 1
    result.should == "break"
  end

  it "propagates exception out of the block passed to Array#zip" do
    count = 0
    lambda { @b.zip { |arr| count = count + 1; raise "exception" } }.should raise_error(RuntimeError)
    count.should == 1
  end

end
