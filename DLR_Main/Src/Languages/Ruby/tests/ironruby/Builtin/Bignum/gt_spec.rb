require File.dirname(__FILE__) + '/../../spec_helper'

describe "Bignum#>" do
  it "returns true if self is greater than other, where other is Bignum" do
	(0x80000000 > 0x80000000).should_not == true
    (0x80000002 > 0x80000001).should == true
    (0x80000001 > 0x80000002).should == false
    (-0x80000002 > 0x80000001).should == false
    (-0x80000001 > 0x80000002).should == false
    (0x80000002 > -0x80000001).should == true
    (0x80000001 > -0x80000002).should == true
    (-0x80000002 > -0x80000001).should == false
    (-0x80000001 > -0x80000002).should == true
  end
  it "returns true if self is greater than other, where other is Fixnum" do
	(0x80000000 > 10).should == true
	(-0x80000000 > 10).should == false
	(0x80000000 > -10).should == true
	(-0x80000000 > -10).should == false
  end
  it "returns true if self is greater than other, where other is Float" do
    (0x80000000 > 14.6).should == true
    (-0x80000000 > 14.6).should == false
    (0x80000000 > -14.6).should == true
    (-0x80000000 > -14.6).should == false
    
    (0x80000000 > 2147483648.0).should_not == true
    (0x80000000 > 2147483648.5).should == false
    (0x80000000 > 2147483647.5).should == true
    (-0x80000000 > -2147483648.5).should == true
    (-0x80000000 > -2147483647.5).should == false    
  end
end
