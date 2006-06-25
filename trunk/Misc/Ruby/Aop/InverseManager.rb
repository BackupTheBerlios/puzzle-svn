#!/usr/bin/env ruby
require 'set'

module InverseArrayMixin
	attr_accessor :owner_class
	attr_accessor :owner_property
	attr_accessor :owner_object
	
	
	def << (item)
		unless @mute
		
			@mute = true
			data = @owner_class.method_attribute @owner_property,:child_of
			klass = data[0]
			getter = data[1]
			setter = "#{getter}=".to_sym 
			item.send setter,@owner_object
			
			@mute = nil
			super			
		end
	end
end

class CtorInterceptor
	def intercept (call)
		#print call.target
		target = call.proceed()
		list_properties = call.target.class_attribute(:child_of)
		list_properties.each do |property|
			arr = []
			
			#mixin our list interceptor
			arr.instance_eval do
				class << self
					include InverseArrayMixin
				end
			end
			arr.owner_class = call.target
			arr.owner_property = property
			arr.owner_object = target
			
			target.instance_variable_set("@#{property}".to_sym, arr)
		end
		
		return target
	end
end
	
class ChildInterceptor
	def intercept (call)
		data = call.target.class.method_attribute call.method.to_sym,:child_of
		klass = data[0]
		property = data[1]
		res = call.proceed()
		if res == nil
			res = []
		end
		return res
	end
end

class ParentInterceptor
	def intercept (call)
		data = call.target.class.method_attribute call.method.to_sym,:parent_of
		klass = data[0]
		inverse = data[1]
		
		getter=call.method.to_s
		getter=getter[0,getter.length-1]
		
		current_value = call.target.send getter.to_sym
		new_value = call.args[0]
		
		if current_value != nil
			arr = current_value.send inverse
			arr.delete call.target
		end
		
		#get the new inverse list
		arr = new_value.send inverse
		#add item to list
		arr << call.target
		
		
		res = call.proceed()
		return res
	end
end

Puzzle::RAspect.define_aspect do
	self_register
	match_by_attribute :child_of
	
	pointcut_by_signature "new"
	with CtorInterceptor.new
	
	pointcut_by_attribute :child_of
	with ChildInterceptor.new
	
	pointcut_by_attribute :parent_of
	with ParentInterceptor.new
end