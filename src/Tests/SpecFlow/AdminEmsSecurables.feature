Feature: AdminEmsSecurables
	As an application developer
	I want to be able to query the admin EMS securable objects
	So I can check the permissions of those securables for other users.

Scenario: Admin Get Securable Effective Access - No Access
	Given A valid API endpoint
	When I run GetAccessForSecurable with securableId 'FDW:General', accessRight 'Lookup Flight By Ident Info', and username 'emsapitest'
	Then EmsSecurableEffectiveAccess is returned
	And The HasAccess property is false
	
Scenario: Admin Get Securable Effective Access - Access
	Given A valid API endpoint
	When I run GetAccessForSecurable with securableId 'FDW:General', accessRight 'View Flights', and username 'emsapitest'
	Then EmsSecurableEffectiveAccess is returned
	And The HasAccess property is true