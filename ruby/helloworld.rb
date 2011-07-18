require 'rubygems'
require 'pry'

class Car

	def honk
		"honk"
	end
end

car = Car.new
car.honk()

binding.pry

