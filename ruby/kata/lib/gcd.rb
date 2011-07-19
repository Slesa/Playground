# Greatest common divisor
# gcd(a,0) = a
# gcd(b, a mod b)  -> Euklid's algorithm

def gcd(a,b)
	#puts "a=#{a}, b=#{b}"
	if b==0 then 
		#puts "b is 0"
		a
	else
		#a if b==0
		#puts "b is obviously not 0"
		if a%b!=0 then 
			gcd(b, a%b)
		else
	#gcd(b, a%b) unless a%b!=0
			b
		end
	end
end
