Feature: Analytics
	As an application developer
	I want to be able to access EMS analytic definitions and values

Scenario: Search for analytics by name
	Given A valid API endpoint
	And The cached EMS system id of 1
	When I run Search and enter a search string of 'MD-80 Series' and a group id of 'DimensionValues.Dimension:ada09d23-162d-46ad-a438-a0cdaefdb3f8'
	Then A single AnalyticInfo object is returned

Scenario: Search for analytics by name and flight
	Given A valid API endpoint
	And The cached EMS system id of 1
	When I run Search and enter a flight id of 1 and a search string of 'MD-80 Series' and a group id of 'DimensionValues.Dimension:ada09d23-162d-46ad-a438-a0cdaefdb3f8'
	Then A single AnalyticInfo object is returned

Scenario: Get analytic info
	Given A valid API endpoint
	And The cached EMS system id of 1
	When I run GetInfo and enter an analytic id of 'H4sIAAAAAAAEAJ2STW/CMAyGz0PiPyAO3NIkbekHsEqTuCDBBcS0qxunEKkkrA1jP39tUWDVtstuie3ntf0mi62sTfkBeSlXKLVVhZLV6PNU6nqmnsdHa88zSq/Xq3cNPFMdqM8Yp2+b9U4c5QmI0rUFLeT4xtyJukvXHoIFYbStQFjHh5TF9AWVt69gnA0Ho9HiNoWsVpgtoVyqk9S1MvoVyotc0G/ZXvXuLEUzsFg2TbJJaedbY+zkYOftuS/iEO+x73+2bMWHg6enXoM2CAgsRT8gPPKRhBEggTBICDCBIAvMgyJpIdqjnNTGoGzvayOgXGFX6GKuplujDfhJMY0lByLzKCehSHnTaiqIz30meYxpDqxTuBONRL/3X5Y476gz8uF9z+tfX2Gv1ful+UWZABA+a2zgQcRJEQRIRJGnJMUkSrFgYRjGP4Xv+HDgkv1fmX0BGyq1a6wCAAA='
	Then An AnlyticInfo object is returned
	And It has the name 'Airframe: MD-80 Series'

Scenario: Get analytic info for flight
	Given A valid API endpoint
	And The cached EMS system id of 1
	When I run GetInfo and enter a flight id of 1 and an analytic id of 'H4sIAAAAAAAEAJ2STW/CMAyGz0PiPyAO3NIkbekHsEqTuCDBBcS0qxunEKkkrA1jP39tUWDVtstuie3ntf0mi62sTfkBeSlXKLVVhZLV6PNU6nqmnsdHa88zSq/Xq3cNPFMdqM8Yp2+b9U4c5QmI0rUFLeT4xtyJukvXHoIFYbStQFjHh5TF9AWVt69gnA0Ho9HiNoWsVpgtoVyqk9S1MvoVyotc0G/ZXvXuLEUzsFg2TbJJaedbY+zkYOftuS/iEO+x73+2bMWHg6enXoM2CAgsRT8gPPKRhBEggTBICDCBIAvMgyJpIdqjnNTGoGzvayOgXGFX6GKuplujDfhJMY0lByLzKCehSHnTaiqIz30meYxpDqxTuBONRL/3X5Y476gz8uF9z+tfX2Gv1ful+UWZABA+a2zgQcRJEQRIRJGnJMUkSrFgYRjGP4Xv+HDgkv1fmX0BGyq1a6wCAAA='
	Then An AnlyticInfo object is returned
	And It has the name 'Airframe: MD-80 Series'

Scenario: Get the contents of an analytic group
	Given A valid API endpoint
	And The cached EMS system id of 1
	When I run GetGroup and enter an an analytic group id of 'DimensionValues.Dimension:ada09d23-162d-46ad-a438-a0cdaefdb3f8'
	Then An AnalyticGroupContents object is returned
	And It contains an analytic with the name 'Airframe: MD-80 Series'

Scenario: Get analytic results
	Given A valid API endpoint
	And The cached EMS system id of 1
	When I run QueryResults and enter a flight id of 1 and a query with an analytic id of 'H4sIAAAAAAAEAJ2STW/CMAyGz0PiPyAO3NIkbekHsEqTuCDBBcS0qxunEKkkrA1jP39tUWDVtstuie3ntf0mi62sTfkBeSlXKLVVhZLV6PNU6nqmnsdHa88zSq/Xq3cNPFMdqM8Yp2+b9U4c5QmI0rUFLeT4xtyJukvXHoIFYbStQFjHh5TF9AWVt69gnA0Ho9HiNoWsVpgtoVyqk9S1MvoVyotc0G/ZXvXuLEUzsFg2TbJJaedbY+zkYOftuS/iEO+x73+2bMWHg6enXoM2CAgsRT8gPPKRhBEggTBICDCBIAvMgyJpIdqjnNTGoGzvayOgXGFX6GKuplujDfhJMY0lByLzKCehSHnTaiqIz30meYxpDqxTuBONRL/3X5Y476gz8uF9z+tfX2Gv1ful+UWZABA+a2zgQcRJEQRIRJGnJMUkSrFgYRjGP4Xv+HDgkv1fmX0BGyq1a6wCAAA='
	Then A QueryResult object is returned

Scenario: Get analytic metadata
	Given A valid API endpoint
	And The cached EMS system id of 1
	When I run GetMetadata and enter a flight id of 1 and an analytic id of 'H4sIAAAAAAAEAG2QTwuCQBDF70HfQby7u5oQiApBHQS7FELXbd10YF1td80+fkuy/aHmMDyY+fHeTHrguhc3eha8qLk0cAGuvHsnpE4g81tjhgTjaZrQtEK9anBESIhP+/LIWt7RAKQ2VDLuz8yL0M+xRjU1lPXSKMqM42NM1nhTA6oU9fPlwvPSOQVXRZ07qdGu02XfAKMixR8LX8Bx4MxmZlvr40EiQWS+UaPNg/8vVhKuo701J3OFAYmiMIitDIhrrt6+P/hy4Ybfv8sf/fX2pVIBAAA='
	Then a Metadata object is returned