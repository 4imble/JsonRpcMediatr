Feature: Subtract Request

Scenario Outline: When I send a subtract request
	When I send a 'Subtract' request with parameters
		| Parameters                       |
		| { left: <left>, right: <right> } |
	Then I should get a '<result>' number result

Examples: 
	| left | right | result |
	| 10   | 6     | 4      |
	| 10   | 2     | 8      |
	| 5    | 20    | -15    |
	| -5   | -10   | 5      |
