Feature: Ping Request

Scenario: When I send a ping request
	When I send a 'Ping' request
	Then I should get a 'Pong' string result
