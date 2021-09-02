Feature: AdminUser
	As an application developer
	I want to be able to query the Admin Users
	So that I can use the information in other queries, and build my application

Scenario: Get all of the users
	Given A valid API endpoint
	When I run GetUsers
	Then An enumerable of AdminUser objects are returned

Scenario: Get a filtered set of just one user
	Given A valid API endpoint
	When I run GetUsers with a filter
	Then An enumerable with one AdminUser object is returned