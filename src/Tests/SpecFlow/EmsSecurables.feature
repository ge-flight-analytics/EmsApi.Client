Feature: EmsSecurables
	As an application developer
	I want to be able to query the EMS securable objects
	So I can check the permissions of those securables.

Scenario: Get Securables
	Given A valid API endpoint
	When I run GetEmsSecurables
	Then EmsSecurableContainer is returned

Scenario: Get Securable Effective Access - No Access
	Given A valid API endpoint
	When I run GetAccessForSecurable with securableId 'FDW:General' and accessRight 'Lookup Flight By Ident Info'
	Then EmsSecurableEffectiveAccess is returned
	And The HasAccess property is false
	
Scenario: Get Securable Effective Access - Access
	Given A valid API endpoint
	When I run GetAccessForSecurable with securableId 'FDW:General' and accessRight 'View Flights'
	Then EmsSecurableEffectiveAccess is returned
	And The HasAccess property is true