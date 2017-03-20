Feature: Profiles
	As an application developer
	I want to be able to access APM profile definitions and results for flights

Scenario: Get all APM profiles
	Given A valid API endpoint
	And The cached EMS system id of 1
	When I run GetAll
	Then EmsProfile objects are returned

Scenario: Get APM profile results for a specific flight
	Given A valid API endpoint
	And The cached EMS system id of 1
	When I run GetResults and enter a flight id of 1 and a profile id of 'A7483C44-9DB9-4A44-9EB5-F67681EE52B0'
	Then A ProfileResults object is returned

Scenario: Get APM profile glossary
	Given A valid API endpoint
	And The cached EMS system id of 1
	When I run GetGlossary and enter a profile id of 'A7483C44-9DB9-4A44-9EB5-F67681EE52B0'
	Then An EmsProfileGlossary object is returned