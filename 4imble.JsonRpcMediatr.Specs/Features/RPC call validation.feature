@Validation
Feature: RPC call validation

Background:
	Given I am the following user
		| Name   | IsAuthenticated |
		| Gimble | false           |

Scenario: Sending RPC call without JSON-RPC protocol
	Given I have the following RPC request object
		| Identifier | Json                            |
		| Request1   | {"method": "subtract", "id": 1} |
	When I send the request with identifier 'Request1'
	Then it should respond with the following response error
		 | Json                                                                                 |
		 | {"jsonrpc": "2.0", "error": {"code": -32600, "message": "Invalid Request"}, "id": 1} |

Scenario: Sending RPC call with invalid JSON
	Given I have the following RPC request object
		| Identifier | Json                                                         |
		| Request1   | {"jsonrpc": "2.0", "method": "foobar, "params": "bar", "baz] |
	When I send the request with identifier 'Request1'
	Then it should respond with the following response error
		 | Json                                                                                |
		 | {"jsonrpc": "2.0", "error": {"code": -32700, "message": "Parse error"}, "id": null} |

Scenario: Sending RPC call with invalid method not a string
	Given I have the following RPC request object
		| Identifier | Json                             |
		| Request1   | {"jsonrpc": "2.0", "method": 1 } |
	When I send the request with identifier 'Request1'
	Then it should respond with the following response error
		 | Json                                                                                    |
		 | {"jsonrpc": "2.0", "error": {"code": -32600, "message": "Invalid Request"}, "id": null} |

Scenario: Sending RPC call with invalid method
	Given I have the following RPC request object
		| Identifier | Json                                              |
		| Request1   | {"jsonrpc": "2.0", "method": "foobar", "id": "1"} |
	When I send the request with identifier 'Request1'
	Then it should respond with the following response error
		 | Json                                                                                    |
		 | {"jsonrpc": "2.0", "error": {"code": -32601, "message": "Method not found"}, "id": "1"} |

Scenario: Request with ordered parameters
	Given I have the following RPC request object
		| Identifier | Json                                                                  |
		| Request1   | {"jsonrpc": "2.0", "method": "subtract", "params": [42, 23], "id": 1} |
	When I send the request with identifier 'Request1'
	Then it should respond with the following response error
		 | Json                                                                                 |
		 | {"jsonrpc": "2.0", "error": {"code": -32700, "message": "Parse error"}, "id": null } |