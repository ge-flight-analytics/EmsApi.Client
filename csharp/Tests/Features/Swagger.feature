Feature: Swagger
	As an application developer
	I want to be able to access the swagger specification for the api
	So that I can build automation around it

Scenario: Get specification as a raw string
	Given A valid API endpoint
	When I run GetSpecificationJson and enter the string 'v2'
	Then A raw JSON string is returned

Scenario: Get specification as a parsed JSON object
	Given A valid API endpoint
	When I run GetSpecification and enter the string 'v2'
	Then A parsed JObject is returned