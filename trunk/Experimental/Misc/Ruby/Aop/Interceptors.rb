#!/usr/bin/env ruby
class LoggingInterceptor
	def intercept (call)
		print "entering #{call.value_signature}\n"
		res = call.proceed()
		print "returning #{call.value_signature}	[result = #{res}]\n"
		return res
	end
end

class TestInterceptor
	def intercept (call)
		print "test test #{call.target}\n"
		res = call.proceed()
		return res
	end
end