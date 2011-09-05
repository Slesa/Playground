array = (1..16).to_a
line = ""
array.each_with_index do |a, i| 
	line += a.to_s + " "
	if (i+1)%4==0 then
		puts line
		line = ""
	end
end

(1..16).each_slice(4) { |a| p a }
