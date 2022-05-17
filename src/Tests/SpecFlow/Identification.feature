Feature: Identification
	As an application developer
	I want to be able to access flight identification information.

Scenario: Get flight identification
	Given A valid API endpoint
	When I run GetFlightIdentification for flight 1
	Then An Identification object is returned
	And It has the fleet 'Austin Digital Fleet 1'
