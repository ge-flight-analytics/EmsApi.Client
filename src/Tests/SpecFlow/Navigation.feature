Feature: Navigation
	As an application developer
	I want to be able to query the EMS Navigation data.

Scenario: Get Airports
	Given A valid API endpoint
	When I run GetAirports
	Then NavigationAirports are returned

Scenario: Get Runways
    Given A valid API endpoint
    When I run GetRunways for airport id 2686
    Then NavigationRunways are returned

Scenario: Get Procedures
    Given A valid API endpoint
    When I run GetProcedures for airport id 2686
    Then NavigationProcedures are returned

Scenario: Get Segments
    Given A valid API endpoint
    When  I run GetSegments for procedure id 26867
    Then NavigationProcedureSegments are returned

Scenario: Get Waypoint
    Given A valid API endpoint
    When I run GetWaypoint for waypoint id 39209
    Then a NavigationWaypoint is returned

Scenario:  Get Navaid
    Given A valid API endpoint
    When I run GetNavaid for navaid id 20432
    Then a NavigationNavaid is returned

Scenario: Get Flight Procedures: departures
    Given A valid API endpoint
    When I run GetFlightProcedures for flight 1 and procedure type Departure
    Then a NavigationFlightProcedure is returned

Scenario: Get Flight Procedures: approaches
    Given A valid API endpoint
    When I run GetFlightProcedures for flight 1 and procedure type Approach
    Then a NavigationFlightProcedure is returned

