Feature: AnalyticSets
	As an application developer
	I want to be able to access EMS analytic set definitions and values.

Scenario: Get analytic set groups
	Given A valid API endpoint
	When I run GetAnalyticSetGroups
	Then An AnalyticSetGroup object is returned
	And It has the name 'Misc'

Scenario: Get analytic set group
	Given A valid API endpoint
	When I run GetAnalyticSetGroup and enter an analytic group id of 'EMS Library'
	Then An AnalyticSetGroup object is returned
	And It has the name 'EMS Library'

Scenario: Get analytic set
    Given A valid API endpoint
    When I run GetAnalyticSet and enter a group id of 'EMS Library' and a set name of 'Uncommanded Pitch'
    Then An AnalyticSet object is returned
    And It has the name 'Uncommanded Pitch'

Scenario: Create, update, read, and delete an analytic set group
    Given A valid API endpoint
    Given I have a NewAnalyticSetGroup with name 'New Analytic Set Group'
    When I run CreateAnalyticSetGroup with group id 'Root'
    Then An AnalyticSetGroupCreated object is returned
    When I run GetAnalyticSetGroup and enter an analytic group id of 'New Analytic Set Group'
    Then An AnalyticSetGroup object is returned
    And It has the name 'New Analytic Set Group'
    Given I have a UpdateAnalyticSetGroup
    When I run UpdateAnalyticSetGroup with group id 'New Analytic Set Group'
    Then An AnalyticSetGroupUpdated object is returned
    When I run GetAnalyticSetGroup with group id 'Updated Analytic Set Group'
    Then An AnalyticSetGroup object is returned
    And It has the name 'Updated Analytic Set Group'
    When I run DeleteAnalyticSetGroup with group id 'Updated Analytic Set Group'
    Then the analytic set group with id 'Updated Analytic Set Group' does not exist

Scenario: Create, update, read, and delete an analytic set
    Given A valid API endpoint
    Given I have a NewAnalyticSet with name 'New Analytic Set'
    When I run CreateAnalyticSet with group id 'Root'
    Then An AnalyticSetCreated object is returned
    When I run GetAnalyticSet with group id 'Root' and analytic name 'New Analytic Set'
    Then An AnalyticSet object is returned
    And it has the description 'new analytic set'
    Given I have a UpdateAnalyticSet
    When I run UpdateAnalyticSet with group id 'Root' and analytic set name 'New Analytic Set'
    When I run GetAnalyticSet with group id 'Root' and analytic name 'New Analytic Set'
    Then An AnalyticSet object is returned
    And it has the description 'updated analytic set'
    When I run DeleteAnalyticSet with group id 'Root' and analytic set name 'New Analytic Set'
    Then the analytic set in group id 'Root' and name 'New Analytic Set' does not exist

Scenario: Create, update, read, and delete an analytic collection
    Given A valid API endpoint
    Given I have a NewAnalyticCollection with name 'New Analytic Collection'
    When I run CreateAnalyticCollection with group id 'Root'
    Then An AnalyticCollectionCreated object is returned
    When I run GetAnalyticCollection with group id 'Root' and analytic name 'New Analytic Collection'
    Then An AnalyticCollection object is returned
    And it has the description 'new analytic collection'
    Given I have a UpdateAnalyticCollection
    When I run UpdateAnalyticCollection with group id 'Root' and analytic collection name 'New Analytic Collection'
    When I run GetAnalyticCollection with group id 'Root' and analytic name 'New Analytic Collection'
    Then An AnalyticCollection object is returned
    And it has the description 'updated analytic collection'
    When I run DeleteAnalyticCollection with group id 'Root' and analytic collection name 'New Analytic Collection'
    Then the analytic collection in group id 'Root' and name 'New Analytic Collection' does not exist
    
    
