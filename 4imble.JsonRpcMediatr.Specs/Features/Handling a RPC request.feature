Feature: Handling a RPC request

Background:
	Given I am the following user
		| Name   | IsAuthenticated |
		| Gimble | false           |

Scenario: Request with no parameters
	Given I have the following RPC request object
		| Identifier | Json                                            |
		| Request1   | {"jsonrpc": "2.0", "method": "ping", "id": "1"} |
	When I send the request with identifier 'Request1'
	Then it should respond with the following response
		 | Json                                            |
		 | {"jsonrpc": "2.0", "result": "Pong", "id": "1"} |

Scenario: Request with named parameters
	Given I have the following RPC request object
		| Identifier | Json                                                                                    |
		| Request1   | {"jsonrpc": "2.0", "method": "subtract", "params": { "left": 42, "right": 23}, "id": 1} |
	When I send the request with identifier 'Request1'
	Then it should respond with the following response
		 | Json                                      |
		 | {"jsonrpc": "2.0", "result": 19, "id": 1} |
