#!/usr/bin/env ruby
require "Puzzle.RAspect.rb"
require "Puzzle.Attributes.rb"
#require "Interceptors.rb"
require "InverseManager.rb"

=begin
module MyMixin
	attr_accessor :mixed_in
	
	def mixed_meth()
		print "#{@mixed_in}\n"
	end
end

module MyOtherMixin
	attr_accessor :double_mix
end


Puzzle::RAspect.define_aspect do
	self_register
	match_by_signature "SomeClass" 			#matches by class names
	#match_by_class String 					#matches by a specific Class
	
	pointcut_by_signature "some_method", "some_prop="
	with LoggingInterceptor.new
	
	pointcut_by_attribute :getter
	with LoggingInterceptor.new, TestInterceptor.new
	
	#mixins that will be applied to the types matched by the aspect
	mixin MyMixin, MyOtherMixin	
end



class SomeClass
	meta :dal
	
	meta :some_prop, :parent_of => [:Apa,:gapa]
	attr_accessor :some_prop
	
	meta :some_method, :BizMethod
	def some_method() 
		
		print "hello from some_method\n"
	end	
	
	def SomeClass.say_hello()
		print "hello from say_hello\n"		
	end

end

String.annotate do
	meta :length, :getter
end

class String
	meta :length=, :setter
end

Puzzle::RAspect.register_type SomeClass ,String
Puzzle::RAspect.weave()

a = SomeClass.new()
a.some_method()
a.some_prop= 4
a.mixed_in = "mixed in!!!!"
a.mixed_meth
res = a.some_prop 
mystr = "hello aop"
len = mystr.length
SomeClass.say_hello






module Satan
	def say_hello
		instance_eval %Q{
		def Knulla()
			print "hej"
		end
		}
	end
end



=end



class Order
	meta :details, :child_of => [:OrderLine,:order]
	attr_reader :details
	attr_accessor :customer
	attr_accessor :order_date
end

class OrderLine
	meta :order=, :parent_of => [:Order,:details]
	attr_accessor :order
	attr_accessor :item
	attr_accessor :quantity
end






Puzzle::RAspect.register_type Order ,OrderLine
Puzzle::RAspect.weave()

order = Order.new
detail = OrderLine.new
detail.item = "Apple"
order.customer = "Customer Charlie"
#detail.order = order
#detail.order = order
order.details << detail

print "order has #{order.details.length} details\n"

print "the item in order details[0] is #{order.details[0].item}\n"

#detail.order has been assigned by "order.details << detail"
print "customer of detail is #{detail.order.customer}\n"
order2 = Order.new
order2.customer = "Customer Kaiko"
order2.details << detail

print "order has #{order.details.length} details\n"
print "order2 has #{order2.details.length} details\n"

print "customer of detail is #{detail.order.customer}\n"

print "klar"