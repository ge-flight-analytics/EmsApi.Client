Feature: Assets
	As an application developer
	I want to be able to list and query different assets of the api
	So I can associate assets with other information

Scenario: Get all fleets
	Given A valid API endpoint
	And The cached EMS system id of 1
	When I run GetAllFleets
	Then Fleet objects are returned

Scenario: Get a single fleet
	Given A valid API endpoint
	And The cached EMS system id of 1
	When I run GetFleet and enter the value 1
	Then A Fleet object is returned
	And The Id property is 1
	And The Description is not empty

Scenario: Get all aircraft
	Given A valid API endpoint
	And The cached EMS system id of 1
	When I run GetAllAircraft
	Then Aircraft objects are returned

Scenario: Get all aircraft for fleet
	Given A valid API endpoint
	And The cached EMS system id of 1
	When I run GetAllAircraft and enter a fleet id of 1
	Then Aircraft objects are returned
	And Their FleetIds property contains the value 1

Scenario: Get a single aircraft
	Given A valid API endpoint
	And The cached EMS system id of 1
	When I run GetAircraft and enter the value 1
	Then An Aircraft object is returned
	And The Id property is 1
	And The FleetIds property contains values

Scenario: Get all flight phases
	Given A valid API endpoint
	And The cached EMS system id of 1
	When I run GetAllFlightPhases
	Then FlightPhase objects are returned

Scenario: Get a single flight phase
	Given A valid API endpoint
	And The cached EMS system id of 1
	When I run GetFlightPhase and enter the value 1
	Then A FlightPhase object is returned
	And The Id property is 1
	And The Description is not empty

Scenario: Get all airports
	Given A valid API endpoint
	And The cached EMS system id of 1
	When I run GetAllAirports
	Then Airport objects are returned

Scenario: Get a single airport
	Given A valid API endpoint
	And The cached EMS system id of 1
	When I run GetAirport and enter the value 0
	Then An Airport object is returned
	And The Id property is 0
	And The AirportCode is not empty