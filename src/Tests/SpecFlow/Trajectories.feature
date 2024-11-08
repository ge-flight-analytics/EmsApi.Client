Feature: Trajectories
	As an application developer
	I want to be able to access flight trajectories

Scenario: Get all trajectory configurations
	Given A valid API endpoint
	When I run GetAllConfigurations
	Then TrajectoryConfiguration objects are returned

Scenario: Get a single trajectory
	Given A valid API endpoint
	When I run GetTrajectory and enter a value of 1
	Then A TrajectoryValueArray object is returned

@ignore
Scenario: Get a single trajectory KML document
	Given A valid API endpoint
	When I run GetTrajectoryKml and enter a value of 1 and a value of 'KML Trajectory'
	Then An XML string representing the KML document is returned