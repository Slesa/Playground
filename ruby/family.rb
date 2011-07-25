class Tree

	attr_accessor :children
	attr_accessor :parent
	attr_accessor :node_name

	def initialize(children={}, name="ROOT", parent=nil)
		puts 'Initializing...'
		@node_name = name
		@parent = parent
		@children = {}
		children.each_pair do |key, value| 
			puts "inserting #{key}"
			@children[key] = Tree.new(value, key, self)
		end
		puts '...done'
	end

	def print_info
		puts '--------------------------'
		puts "Hello, my name is #{@node_name}" 
		puts "My parent is #{@parent.node_name}" unless @parent==nil
		puts "I have #{@children.count} children" unless @children.count==0
		puts
	end

	def visit_all
		print_info 
		children.each_pair { |key, value| value.visit_all }
	end
end

ruby_tree = Tree.new( { 'grandpa' => { 'dad' => { 'child 1' => {}, 'child 2' => {} },
	'uncle' => { 'child 3' => {}, 'child 4' => {}}} } )

puts 'Get root info'
ruby_tree.print_info #{ | puts "#{key} is #{value}" }

puts 'Visiting the family'
ruby_tree.visit_all #{ |key,value| puts "This is #{key}" }

