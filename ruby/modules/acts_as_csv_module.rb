module ActsAsCsv

	def self.included(base)
		base.extend ClassMethods
	end

	module ClassMethods
		def acts_as_csv
			include InstanceMethods
		end
	end

	module InstanceMethods

		def read
			@csv_contents = []
			filename = self.class.to_s.downcase + '.txt'
			file = File.new(filename)
			@headers = file.gets.chomp.split(', ')

			file.each do |row|
				@csv_contents << row.chomp.split(', ')
			end
		end

		attr_accessor :headers, :csv_contents

		def initialize
			read
		end

		def each
			return @csv_contents.each
		end

		def self.method_missing name, *args
			number = 
		end
	end
end

class RubyCsv 
	include ActsAsCsv
	acts_as_csv
end

m = RubyCsv.new
puts m.headers.inspect
puts m.csv_contents.inspect

m.each { |row| puts row.one }
