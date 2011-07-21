def hash_to_array(hash)
	array = []
	hash.each_value { |value| array.push value }
	return array
end

def array_to_hash(array)
	hash = {}
	array.each_with_index { |value, index| hash[index] = value }
	return hash
end

hash = { "Eins" => 1, 2 => "Zwei", 3 => 42, "zweiundvierzig" => "antwort" }
puts "Hash is:"
puts "--------"
hash.each_pair { |key, value| puts "#{key} = #{value}" }

puts "Array is:"
puts "---------"
array = hash_to_array hash
puts array

puts "Hash is:"
puts "--------"
hash = array_to_hash array
hash.each_pair { |key, value| puts "#{key} = #{value}" }


