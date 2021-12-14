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
	When I run GetAccessForSecurable with securableId 'FDW:General' and accessRight 'View Aircraft Version'
	Then EmsSecurableEffectiveAccess is returned
	And The HasAccess property is false
	
Scenario: Get Securable Effective Access - Access
	Given A valid API endpoint
	When I run GetAccessForSecurable with securableId 'EmsGeneral:Applications' and accessRight 'Explorer'
	Then EmsSecurableEffectiveAccess is returned
	And The HasAccess property is true