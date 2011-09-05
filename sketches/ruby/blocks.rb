class Fixnum
	def many_times
		i = self
		while i > 0
			i = i - 1
			yield
		end
	end
end

3.times { puts 'mangy moose' }

def call_block(&block)
	block.call
end

def pass_block(&block)
	call_block(&block)
end

pass_block { puts 'Hello, block!' }
