gesucht = rand(10)
puts "Zahl eingeben: "
while line = gets
	eingabe = line.to_i
	puts "Gesuchte Zahl ist groesser" if eingabe < gesucht
	puts "Gesuchte Zahl ist kleiner" if eingabe > gesucht
	if eingabe==gesucht
		puts "Treffer"
		exit
	end
end

