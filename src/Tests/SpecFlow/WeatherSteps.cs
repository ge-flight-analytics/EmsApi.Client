using EmsApi.Dto.V2;
using TechTalk.SpecFlow;

namespace EmsApi.Tests.SpecFlow
{
    [Binding, Scope( Feature = "Weather" )]
    public class WeatherSteps : FeatureTest
    {
        [When( @"I run GetFlightWeather for flight (.*)" )]
        public void WhenIRunGetFlightWeather(int flight)
        {
            m_result.Object = m_api.Weather.GetFlightWeather(flight);
        }

        [Then( @"A WeatherReport is returned" )]
        public void ThenAWeatherReportIsReturned()
        {
            m_result.Object.ShouldNotBeNullOfType<WeatherReport>();
        }

        [When( @"I run GetFlightMetars for flight (.*)" )]
        public void WhenIRunGetFlightMetarsForFlight( int flight )
        {
            m_result.Enumerable = m_api.Weather.GetFlightMetars( flight );
        }

        [Then( @"MetarReports are returned" )]
        public void ThenMetarReportsAreReturned()
        {
            m_result.Enumerable.ShouldNotBeNullOrEmptyOfType<MetarReport>();
        }

        [When( @"I run GetFlightTafs for flight (.*)" )]
        public void WhenIRunGetFlightTafsForFlight( int flight )
        {
            m_result.Enumerable = m_api.Weather.GetFlightTafs( flight );
        }

        [Then( @"TafReports are returned" )]
        public void ThenTafReportsAreReturned()
        {
            m_result.Enumerable.ShouldNotBeNullOrEmptyOfType<TafReport>();
        }

        [When( @"I run GetMetars for airport ICAO ""([^""]*)""" )]
        public void WhenIRunGetMetarsForAirportICAO( string airportIcao )
        {
            m_result.Enumerable = m_api.Weather.GetMetars( new MetarQuery { AirportIcao = airportIcao, MaxResults = 5 } );
        }

        [When( @"I run GetTafs for airport ICAO ""([^""]*)""" )]
        public void WhenIRunGetTafsForAirportICAO( string airportIcao )
        {
            m_result.Enumerable = m_api.Weather.GetTafs( new TafQuery { AirportIcao = airportIcao, MaxResults = 5 } );
        }

        [When( @"I run ParseMetar with the METAR ""([^""]*)""" )]
        public void WhenIRunParseMetarWithTheMETAR( string metar )
        {
            m_result.Object = m_api.Weather.ParseMetar( new MetarParseOptions { Metar = metar } );
        }

        [Then( @"A MetarReport is returned" )]
        public void ThenAMetarReportIsReturned()
        {
            m_result.Object.ShouldNotBeNullOfType<MetarReport>();
        }

        [When( @"I run ParseTaf with the TAF ""([^""]*)""" )]
        public void WhenIRunParseTafWithTheTAF( string taf )
        {
            m_result.Object = m_api.Weather.ParseTaf( new TafParseOptions { Taf = taf } );
        }

        [Then( @"A TafReport is returned" )]
        public void ThenATafReportIsReturned()
        {
            m_result.Object.ShouldNotBeNullOfType<TafReport>();
        }
    }
}
