Feature: Handling requests that throw unhandled errors

Scenario: Request with unhandled error
	Given I am the following user
		| Name   | IsAuthenticated |
		| Gimble | false           |
	Given I have the following RPC request object
		| Identifier | Json                                                                                    |
		| Request201 | {"jsonrpc": "2.0", "method": "exception", "id": 201} |
	When I send the request with identifier 'Request201'
	Then it should respond with the following response error
		| Json                                                                                       |
		| {"jsonrpc": "2.0", "error": {"code": -32000 , "message": "Server error"}, "id": 201} |