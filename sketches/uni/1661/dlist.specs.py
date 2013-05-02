import unittest
import mylist

class TestListEmpty(unittest.TestCase):

	def setUp(self):
		self.list = List.empty()

	def it_should_create_a_list(self):
		self.assertNotEqual(self.list, None)

	def it_creates_an_empty_list(self):
		self.assertEqual(self.list.first(), None)

if __name__ == '__main__':
	unittest.main()

