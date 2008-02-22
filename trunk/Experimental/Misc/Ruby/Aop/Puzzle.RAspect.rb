#!/usr/bin/env ruby

require "Puzzle.Attributes.rb"

class Class
	
	# renames all pointcutted methods and then
	# emits a new method with the same interface which redirects each call to the RAspect hub
	def apply_aop(aspects)
		
		print "RAspect : applying mixins #{self}\n"
		aop_mixin(aspects)
		
		print "RAspect : proxying instance methods #{self}\n"
		#proxy instance methods
		methods = self.instance_methods(true)
		aop_proxify_instance(methods,aspects)
		
		print "RAspect : proxying class methods #{self}\n"
		#proxy static methods
		methods = self.methods()
		aop_proxify_class(methods,aspects)
	end
	
	def aop_mixin(aspects)
		mixins = []
		aspects.each do |aspect|
			mixins += aspect.mixins
		end
		
		
		mixins.each do |mixin|
			self.send :include, mixin
		end
	end
	
	def aop_proxify_instance(methods,aspects)
		methods.each do |method_name|			
			interceptors = Puzzle::RAspect.get_interceptors(self,method_name,aspects)				
			if interceptors.length > 0
				#get the methodinfo
				method = self.instance_method(method_name)
				orig_method = "__aop__#{method_name}".to_sym 
				alias_method orig_method.to_sym,method_name.to_sym
				
				Puzzle::RAspect.remember_method_interceptors(self,method_name,interceptors)
				
				#proxify the method
				code = %Q{
				#meta :#{method_name}, :aop_proxy => :#{orig_method}
				def #{method_name}(*args)
					call = Puzzle::MethodInvocation.new
					call.args = args
					call.target = self
					call.method = "#{method_name}"
					call.endpoint = "__aop__#{method_name}"
					res = Puzzle::RAspect.handle_call(call)	
					return res      
				end      
				}
				
				self.module_eval code
			end
		end      
	end
	
	def aop_proxify_class(methods,aspects)
		methods.each do |method_name|		
			interceptors = Puzzle::RAspect.get_interceptors(self,method_name,aspects)				
			if interceptors.length > 0
				#get the methodinfo
				
				method = self.method(method_name)
				class_name = self.to_s
				
				orig_method = "__aop__#{method_name}"
				code= %Q{
					class << self
						alias_method "#{orig_method}".to_sym, "#{method_name}".to_sym						
					end
				}
						
				self.module_eval code
				
				Puzzle::RAspect.remember_method_interceptors(self,method_name,interceptors)
				
				#proxify the method
				
				code = %Q{
				#meta :#{method_name}, :aop_proxy => :#{orig_method}
				def #{class_name}.#{method_name}(*args)
					call = Puzzle::MethodInvocation.new
					call.args = args
					call.target = self
					call.method = "#{method_name}"
					call.endpoint = "__aop__#{method_name}"
					res = Puzzle::RAspect.handle_static_call(call)	
					return res      
				end      
				}
				
				self.module_eval code
			end
		end      
	end
end

module Puzzle

	class RAspect
		
		#lookup for endpoints
		@methods = Hash.new()
		
		#registered types
		@@types = []
		@@aspects = []
		
		def RAspect.register_type(*types)
			@@types += types
		end
		
		def RAspect.get_interceptors(klass,method_name,aspects)
			interceptors = []
			aspects.each do |aspect|
				aspect.pointcuts.each do |pointcut|						
					if pointcut.match_method? klass,method_name
						pointcut.interceptors.each do |interceptor|
						interceptors << interceptor
						end
					end						
				end
			end
			return interceptors
		end
		
		def RAspect.remember_method_interceptors(type,method,interceptors)
			key = "#{type}.#{method}"		
			@methods[key] = interceptors		
		end
		
		def RAspect.get_method_interceptors(type,method)
			key = "#{type}.#{method}"
			interceptors = @methods[key] 
			
			#hack, subclasses get nil interceptors
			if interceptors == nil
				interceptors = []
			end
			
			return interceptors
		end
	
		#the interception hub for instance methods
		def RAspect.handle_call(call)
			args = call.args
			interceptors = get_method_interceptors(call.target.class,call.method)
			call.interceptors = interceptors
			res = call.proceed()		
			return res
		end
		
		#the interception hub for class methods
		def RAspect.handle_static_call(call)
			args = call.args
			interceptors = get_method_interceptors(call.target,call.method)
			call.interceptors = interceptors
			res = call.proceed()		
			return res
		end
		
		
		def RAspect.register_aspect(aspect)
			@@aspects << aspect
		end
		
		def RAspect.weave()
			
			
			@@types.each do |type|
				print "RAspect : weaving #{type}\n"
				aspects_for_type = get_aspects_for_type(type,@@aspects)
				print "RAspect : got aspects for #{type}\n"
				type.apply_aop(aspects_for_type)
				print "RAspect : weaved #{type}\n"
			end
			
			#clear the type list, so if we weave again after registering new type
			#they wont get weaved again
			@@types = []
		end
		
		def RAspect.get_aspects_for_type(target_type,aspects)
			aspects_for_type = []
			aspects.each do |aspect|
				if aspect.match_type? target_type
					aspects_for_type << aspect
				end
			end
			return aspects_for_type
		end
		
		def RAspect.define_aspect(&definition)
			aspect = Aspect.new
			aspect.instance_eval(&definition)
			return aspect
		end
	end
	
	class Aspect
	
		
		def initialize()
			@mixins = []
			@matchers = []
			@pointcuts = []
			@pointcutStack = []
		end
	
		
		def match_type?(type_name)
			
			matchers = @matchers
			matchers.each do |matcher|
				if matcher.match type_name
					return true
				end
			end
			
			return false
		end
			
		def method_missing(method_sym,*args)
			method_name = method_sym.to_s
			if method_name[0,9] == "match_by_"
				matcher_name = "Aspect#{method_name[9,method_name.length-9].capitalize}Matcher"
				matchclass = eval(matcher_name)		
				matcher = matchclass.new(*args)
				@matchers << matcher
			elsif method_name[0,12] == "pointcut_by_"
				class_name = "#{method_name[12,method_name.length-12].capitalize}Pointcut"
				cutclass = eval(class_name)				
				pointcut = cutclass.new(*args)
				@pointcutStack << pointcut
				@pointcuts << pointcut
			else
				return super(method_name,*args)
			end			
		end
		

		
		def with(*interceptors)
			pointcut = @pointcutStack.pop
			pointcut.interceptors += interceptors		
		end
		
		def pointcuts
			return @pointcuts
		end
		
		def mixins
			return @mixins
		end
	
		def self_register
			RAspect.register_aspect(self)
		end	
		
		def mixin(*mixinTypes)
			@mixins += mixinTypes
		end
	end
	
	
	
	class SignaturePointcut
		attr_accessor :methods
		attr_accessor :interceptors
		
		def initialize(*methods)
			@methods = methods
			@interceptors = []
		end
		
		def match_method?(klass,method_name)
			methods.each do |method|
				if method == method_name
					return true
				end
			end
			return false	
		end
	end
	
	class AttributePointcut
		attr_accessor :attributes
		attr_accessor :interceptors
		
		def initialize(*attributes)
			@attributes = attributes
			@interceptors = []
		end
		
		def match_method?(klass,method_name)
			@attributes.each do |attribute|
				if klass.method_attribute(method_name.to_sym,attribute) != nil
					return true
				end
			end
			return false
		end
	end
	
	
	#represents an intercepted call
	class MethodInvocation
		attr_accessor :target		#the object on which the call was invoked
		attr_accessor :args			#the args passed to the method
		attr_accessor :method		#the invoked method
		attr_accessor :endpoint		#the base implementation of the method, called at the end of the interceptor flow
		attr_accessor :interceptors	#list of interceptors that should be included in the call flow
		
		def initialize()
			@callindex = 0			#we start at the first interceptor
		end
		
		def proceed()
			if @callindex == interceptors.length
				#call the base implementation
				args = self.args			
				res = self.target.send self.endpoint.to_sym,*args
				return res
			else			
				#call the next interceptor			
				interceptor = current_interceptor
				@callindex += 1
				res = interceptor.intercept(self)			
				return res
			end
		end
		
		def current_interceptor
			return @interceptors[@callindex]
		end
		
		def value_signature
			"#{self.target.class}.#{self.method}(#{self.args})"		
		end
	end
	
	
	#this class will be used by an aspect to match types,
	#in this case, it will simply match a type based on its name
	#if the match is true , the aspect should be applied to the matched type
	class AspectSignatureMatcher
		
		attr_reader :type_names
		
		def initialize(*type_names)
			@type_names = type_names		
		end
		
		def match(target_type)
			index = @type_names.index(target_type.to_s)
			return index != nil
		end
	end
	
	class AspectClassMatcher
		
		attr_reader :types
		
		def initialize(*types)
			@types = types		
		end
		
		def match(target_type)
			index = @types.index(target_type)
			return index != nil
		end
	end
	
	class AspectAllMatcher		
		
		def initialize(*args)	
		end
		
		def match(target_type)
			return true
		end
	end
	
	class AspectAttributeMatcher		
		
		def initialize(*args)	
		end
		
		def match(target_type)
			return true
		end
	end

end