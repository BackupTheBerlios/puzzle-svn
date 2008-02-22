#!/usr/bin/env ruby

module Puzzle
	class AttributeStore
		@@metadata = Hash.new{|h,k|h[k] = Hash.new{|h,k|h[k] ={}}}
		
		def AttributeStore.add_meta(klass,method_name,data)
			classHash = @@metadata[klass.to_s]
			methodHash = classHash[method_name]		
			
			if data.is_a? Symbol
				#handle cases like "meta.my_meth :foo
				methodHash[data] = true
			elsif data.is_a? Hash
				#handle cases like "meta.my_meth :foo => :bar
				key = data.keys[0]
				value = data.values[0]
				methodHash[key] = value
			else
				raise "Illegal meta value for #{klass}.#{method_name} : '#{data}'"
			end
			
		end
		
		def AttributeStore.class_attribute(klass,key)
			methods = []
			classHash = @@metadata[klass.to_s]
			classHash.each do |a|
				method = a[0]
				data = a[1]
				if data.length == 1
					found_key = data.keys[0]
					if key == found_key
						methods << method
					end					
				end
			end
			return methods
		end
		
		def AttributeStore.class_attributes(klass)
			classHash = @@metadata[klass.to_s]
			return classHash
		end
		
		def AttributeStore.method_attributes(klass,method_name)
			classHash = @@metadata[klass.to_s]
			methodHash = classHash[method_name]
			return methodHash
		end
		
		def AttributeStore.method_attribute(klass,method_name,key)
			classHash = @@metadata[klass.to_s]
			
			if classHash.has_key? method_name
				methodHash = classHash[method_name]
				if methodHash.has_key? key
					return methodHash[key]
				end
			end
			
			return nil
		end
		
		
	end

end
class Class
	def annotate(&block)
		self.module_eval(&block)
	end
	
	def class_attribute(key)
		return Puzzle::AttributeStore.class_attribute(self,key)
	end
	
	def class_attributes()
		return Puzzle::AttributeStore.class_attributes(self)
	end
	
	def method_attributes(method)
		return Puzzle::AttributeStore.method_attributes(self,method)
	end
	
	def method_attribute(method,key)
		return Puzzle::AttributeStore.method_attribute(self,method,key)
	end
	
	def meta(*args)
		
		if args.length == 1
			method_name = self
			data = args[0]
			Puzzle::AttributeStore.add_meta(self,method_name,data)
		end		
		
		if args.length == 2
			method_name = args[0]
			data = args[1]
			Puzzle::AttributeStore.add_meta(self,method_name,data)
		end
		
	end
end
