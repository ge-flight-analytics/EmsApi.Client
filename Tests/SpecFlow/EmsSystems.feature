Feature: EmsSystems
	As an application developer
	I want to be able to list and query EMS systems
	So that I can use the information in other queries, and build my application

Scenario: Get all EMS systems
	Given A valid API endpoint
	When I run GetAll
	Then EmsSystem objects are returned

Scenario: Get a single EMS system
	Given A valid API endpoint
	When I run Get and enter the value 1
	Then An EmsSystem object is returned

Scenario: Ping an EMS system
	Given A valid API endpoint
	When I run ping and enter the value 1
	Then The result is true

Scenario: Get EMS system information
	Given A valid API endpoint
	When I run GetSystemInfo and enter the value 1
	Then An EmsSystemInfo object is returned