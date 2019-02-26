Feature: Handling a secured requests

Scenario: Request that is secured and user is not authenticated
	Given I am the following user
		| Name   | IsAuthenticated |
		| Gimble | false           |
	Given I have the following RPC request object
		| Identifier | Json                                               |
		| Request1   | {"jsonrpc": "2.0", "method": "secured", "id": "1"} |
	When I send the request with identifier 'Request1'
	Then it should respond with the following response error
		 | Json                                                                                     |
		 | {"jsonrpc": "2.0", "error": {"code": -00001, "message": "User not authorized"}, "id": 1} |

Scenario: Request that is secured and user is authenticated
	Given I am the following user
		| Name   | IsAuthenticated |
		| Gimble | true            |
	Given I have the following RPC request object
		| Identifier | Json                                               |
		| Request1   | {"jsonrpc": "2.0", "method": "secured", "id": "1"} |
	When I send the request with identifier 'Request1'
	Then it should respond with the following response
		 | Json                                      |
		 | {"jsonrpc": "2.0", "result": {}, "id": 1} | 