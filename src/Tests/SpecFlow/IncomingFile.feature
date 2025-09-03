Feature: IncomingFile
    As an application developer
    I want to be able to query the EMS IncomingFile data.

Scenario: GetIncomingFiles with filters
    Given A valid API endpoint
    When I run GetIncomingFiles with statusModifiedDateRangeStart "2025-09-01", statusModifiedDateRangeEnd "2025-09-03", fileName "test.csv", status 1, sourceType 2, activityIds "1001,1002"
    Then IncomingFiles are returned