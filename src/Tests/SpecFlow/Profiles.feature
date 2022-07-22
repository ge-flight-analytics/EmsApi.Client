Feature: Profiles
	As an application developer
	I want to be able to access APM profile definitions and results for flights

Scenario: Get APM profile definitions
	Given A valid API endpoint
	When I run GetDefinitions
	Then Profile objects are returned

Scenario: Get APM profile results for a specific flight
	Given A valid API endpoint
	When I run GetResults and enter a flight id of 1 and a profile id of 'A7483C44-9DB9-4A44-9EB5-F67681EE52B0'
	Then A ProfileResults object is returned

Scenario: Get APM profile glossary
	Given A valid API endpoint
	When I run GetGlossary and enter a profile id of 'A7483C44-9DB9-4A44-9EB5-F67681EE52B0'
	Then A ProfileGlossary object is returned

Scenario: Get APM profile glossary with version
    Given A valid API endpoint
    When I run GetGlossary and enter a profile id of 'A7483C44-9DB9-4A44-9EB5-F67681EE52B0' with version 1
    Then A ProfileGlossary object is returned with version 1

Scenario: Get an APM profile group
	Given A valid API endpoint
	When I run GetGroup
	Then A ProfileGroup object is returned

Scenario: Get APM profile event definitions
    Given A valid API endpoint
    When I run GetEvents and enter a profile id of 'A7483C44-9DB9-4A44-9EB5-F67681EE52B0'
    Then Event objects are returned

Scenario: Get APM profile event definition for a specific event
    Given A valid API endpoint
    When I run GetEvent and enter a profile id of 'A7483C44-9DB9-4A44-9EB5-F67681EE52B0' and an event id of 163
    Then An Event object is returned
    Then The ParameterSet is not null