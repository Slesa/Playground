def with_block
	puts "Opening file with block:"
	puts "------------------------"
	File.open("fileaccess.rb").each { |line|
		puts line
	}
	puts "------------------------"
end

def without_block
	puts "Opening file with block:"
	puts "------------------------"
	file = File.new("fileaccess.rb")
	begin
		while (line = file.readline)
			line.chomp
			puts line
		end
		rescue EOFError
		file.close
	end
	puts "------------------------"
end
	
with_block

without_block
