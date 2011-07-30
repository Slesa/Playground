class SimpleGrep

	attr_accessor :file_name
	attr_accessor :pattern

	def initialize(file_name, pattern)
		@file_name = file_name
		@pattern = Regexp.new(pattern)
	end

	def can_process?
		return File.exists?(file_name)
	end

	def process
		puts "Pattern is #{@pattern}"
		row = 0
		File.open(@file_name, 'r') do |fh|
			fh.each_line do |line|
				row += 1
				puts "#{row}: #{line}" unless line.grep(@pattern).empty?
			end
		end
	end
end

file_name = ARGV[0]
if (file_name=='') then
	puts 'No file name given'
	exit
end

pattern = ARGV[1]
if (pattern=='') then
	puts 'No pattern given'
	exit
end

sg = SimpleGrep.new(file_name, pattern)
if (!sg.can_process?) then
	puts 'Cannot process search'
	exit
end

puts
puts "Searching in \'#{file_name}\' for \'#{pattern}\'"
puts '----------------------------------------'
sg.process
puts '--- end --------------------------------'


