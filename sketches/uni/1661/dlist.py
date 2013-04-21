class DElement(object):
	pass

class DPos:
	
	def __init__(self, value):
		this.value = value
		this.pred = None
		this.succ = None


# algebra list
# sorts list, elem
class DList(DPos):

	# op count: list -> int
	def count(self):
		return 0	

	# op first: list -> elem
	def front(self):
		return this;

	# op rest: list -> list
	# def rest(self):
		
	# op append: list x elem -> list
	# def append(self, elem)
		

