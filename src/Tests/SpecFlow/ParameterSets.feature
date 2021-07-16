Feature: ParameterSets
	As an application developer
	I want to be able to enumerate available parameter sets on an EMS system

Scenario: Get all parameter sets
	Given A valid API endpoint
	When I run GetSets
	Then A ParameterSetGroup object is returned

Scenario: Get all parameter sets for a particular group
	Given A valid API endpoint
	When I run GetSets and enter a group id of 'EMS Library'
	Then A ParameterSetGroup object is returned