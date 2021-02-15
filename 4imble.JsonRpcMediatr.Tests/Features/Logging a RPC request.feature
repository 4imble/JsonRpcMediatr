Feature: Logging a RPC request

Background:
	Given I am the following user
		| Name   | IsAuthenticated |
		| Gimble | false           |

Scenario: Logging a Request with named parameters
	Given I have the following RPC request object
		| Identifier | Json                                                                                    |
		| Request1   | {"jsonrpc": "2.0", "method": "subtract", "params": { "left": 42, "right": 23}, "id": 1} |
	When I send the request with identifier 'Request1'
	Then it should log the following details
		| Method   | Params                      | RequestType     |
		| Subtract | { "left": 42, "right": 23 } | SubtractRequest |