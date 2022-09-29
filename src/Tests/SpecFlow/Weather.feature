Feature: Weather
	As an application developer
	I want to be able to query the EMS Weather data.

Scenario: GetFlightWeather
	Given A valid API endpoint
	When I run GetFlightWeather for flight 1
	Then A WeatherReport is returned

Scenario: GetFlightMetars
	Given A valid API endpoint
	When I run GetFlightMetars for flight 125
	Then MetarReports are returned

Scenario: GetFlightTafs
	Given A valid API endpoint
	When I run GetFlightTafs for flight 125
	Then TafReports are returned

Scenario: GetMetars
	Given A valid API endpoint
	When I run GetMetars for airport ICAO "LOWS"
	Then MetarReports are returned

Scenario: GetTafs
	Given A valid API endpoint
	When I run GetTafs for airport ICAO "ORMM"
	Then TafReports are returned


Scenario: ParseMetar
	Given A valid API endpoint
	When I run ParseMetar with the METAR "KRYY 282100Z 23008G16KT 10SM OVC080 16/08 A3005 RMK AO2 SLP899 T01560083 10172 20144 58000"
	Then A MetarReport is returned

Scenario: ParseTaf
	Given A valid API endpoint
	When I run ParseTaf with the TAF "KATL 282000Z 2820/2924 03010G18KT P6SM FEW200 SCT250 FM290200 03010KT P6SM SCT250 FM291000 05011G20KT P6SM SCT200 FM291500 05014G24KT P6SM SCT200"
    Then A TafReport is returned