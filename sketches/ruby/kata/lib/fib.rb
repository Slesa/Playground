# Fibonacci numbers
# f(0) = 0
# f(1) = 1
# f(n) = f(n-1) + f(n-2)

def fib(number)
	case number
		when 0 then 0
		when 1 then 1
		else fib(number-1) + fib(number-2)
	end
end
