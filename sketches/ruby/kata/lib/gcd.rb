# Greatest common divisor
# gcd(a,0) = a
# gcd(b, a mod b)  -> Euklid's algorithm

def gcd(a,b)
	return a if b==0
	return b if a%b==0
	gcd(b, a%b)
end
