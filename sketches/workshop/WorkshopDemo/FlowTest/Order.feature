Feature: OrderDefinition
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario: Add an item to an order
	Given I order 2 times article 'TestArticle' of price 0.99
	When I add the item to the order
	Then the amount should be 0.99 
