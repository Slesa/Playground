Feature: Cash register ordering system
	In order to manage order items
	As a waiter
	I want to be told the sum of two numbers

Scenario: Add an item to an order
	Given I order 2 times article 'TestArticle' of price 0.99
	When I add the item to the order
	Then the amount should be 0.99 
