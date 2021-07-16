Feature: EmsSystem
	As an application developer
	I want to be able to query the EMS system
	So that I can use the information in other queries, and build my application

Scenario: Get a single EMS system
	Given A valid API endpoint
	When I run Get
	Then An EmsSystem object is returned

Scenario: Ping an EMS system
	Given A valid API endpoint
	When I run ping
	Then The result is true

Scenario: Get EMS system information
	Given A valid API endpoint
	When I run GetSystemInfo
	Then An EmsSystemInfo object is returned