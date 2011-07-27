class ActsAsCsv
	def read
		file = File.new(self.class.to_s.downcase + '.txt')
		@headers = file.gets.chomp.split(', ')

		file.each do |row|
			@result << row.chomp.split(', ')
		end
	end

	def headers
		@headers
	end

	def csv_content
		@result
	end

	def initialize
		@result = []
		read
	end
end

class RubyCsv < ActsAsCsv
end

m = RubyCsv.new
puts m.headers.inspect
puts m.csv_content.inspect

