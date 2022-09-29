using System.Collections.Generic;
using System.Threading.Tasks;
using EmsApi.Dto.V2;

namespace EmsApi.Client.V2.Access
{
    public class WeatherAccess : RouteAccess
    {
        /// <summary>
        /// Returns a list of all matched TAF and METAR reports for a specified flight.
        /// </summary>
        /// <param name="flightId">
        /// The flight record identifier to get the weather for.
        /// </param>
        public virtual Task<WeatherReport> GetFlightWeatherAsync( int flightId, CallContext context = null )
        {
            return CallApiTask( api => api.GetFlightWeather( flightId, context ) );
        }

        /// <summary>
        /// Returns a list of all matched TAF and METAR reports for a specified flight.
        /// </summary>
        /// <param name="flightId">
        /// The flight record identifier to get the weather for.
        /// </param>
        public virtual WeatherReport GetFlightWeather( int flightId, CallContext context = null )
        {
            return AccessTaskResult( GetFlightWeatherAsync( flightId ) );
        }

        /// <summary>
        /// Returns a list of matched METAR reports for a specified flight.
        /// </summary>
        /// <param name="flightId">
        /// The flight record identifier to get the METARs for.
        /// </param>
        public virtual Task<IEnumerable<MetarReport>> GetFlightMetarsAsync( int flightId, CallContext context = null )
        {
            return CallApiTask( api => api.GetFlightMetars( flightId, context ) );
        }

        /// <summary>
        /// Returns a list of matched METAR reports for a specified flight.
        /// </summary>
        /// <param name="flightId">
        /// The flight record identifier to get the METARs for.
        /// </param>
        public virtual IEnumerable<MetarReport> GetFlightMetars( int flightId, CallContext context = null )
        {
            return AccessTaskResult( GetFlightMetarsAsync( flightId ) );
        }

        /// <summary>
        /// Returns a list of matched TAF reports for a specified flight.
        /// </summary>
        /// <param name="flightId">
        /// The flight record identifier to get the TAFs for.
        /// </param>
        public virtual Task<IEnumerable<TafReport>> GetFlightTafsAsync( int flightId, CallContext context = null )
        {
            return CallApiTask( api => api.GetFlightTafs( flightId, context ) );
        }

        /// <summary>
        /// Returns a list of matched TAF reports for a specified flight.
        /// </summary>
        /// <param name="flightId">
        /// The flight record identifier to get the TAFs for.
        /// </param>
        public virtual IEnumerable<TafReport> GetFlightTafs( int flightId, CallContext context = null )
        {
            return AccessTaskResult( GetFlightTafsAsync( flightId ) );
        }

        /// <summary>
        /// Returns a list of matched TAF reports matching the specified search criteria.
        /// </summary>
        /// <param name="query">
        /// The search criteria for querying for TAF reports.
        /// </param>
        public virtual Task<IEnumerable<TafReport>> GetTafsAsync( TafQuery query, CallContext context = null )
        {
            return CallApiTask( api => api.GetTafs( query, context ) );
        }

        /// <summary>
        /// Returns a list of matched TAF reports matching the specified search criteria.
        /// </summary>
        /// <param name="query">
        /// The search criteria for querying for TAF reports.
        /// </param>
        public virtual IEnumerable<TafReport> GetTafs( TafQuery query, CallContext context = null )
        {
            return AccessTaskResult( GetTafsAsync( query, context ) );
        }

        /// <summary>
        /// Returns a list of matched METAR reports matching the specified search criteria.
        /// </summary>
        /// <param name="query">
        /// The search criteria for querying for METAR reports.
        /// </param>
        public virtual Task<IEnumerable<MetarReport>> GetMetarsAsync( MetarQuery query, CallContext context = null )
        {
            return CallApiTask( api => api.GetMetars( query, context ) );
        }

        /// <summary>
        /// Returns a list of matched METAR reports matching the specified search criteria.
        /// </summary>
        /// <param name="query">
        /// The search criteria for querying for METAR reports.
        /// </param>
        public virtual IEnumerable<MetarReport> GetMetars( MetarQuery query, CallContext context = null )
        {
            return AccessTaskResult( GetMetarsAsync( query, context ) );
        }

        /// <summary>
        /// Parses and validates a raw TAF report and returns parsed information.
        /// </summary>
        /// <param name="parseOptions">
        /// The TAF string to parse and an issue date.
        /// </param>
        public virtual Task<TafReport> ParseTafAsync( TafParseOptions parseOptions, CallContext context = null )
        {
            return CallApiTask( api => api.ParseTaf( parseOptions, context ) );
        }

        /// <summary>
        /// Parses and validates a raw TAF report and returns parsed information.
        /// </summary>
        /// <param name="parseOptions">
        /// The TAF string to parse and an issue date.
        /// </param>
        public virtual TafReport ParseTaf( TafParseOptions parseOptions, CallContext context = null )
        {
            return AccessTaskResult( ParseTafAsync( parseOptions, context ) );
        }

        /// <summary>
        /// Parses and validates a raw METAR report and returns parsed information.
        /// </summary>
        /// <param name="parseOptions">
        /// The METAR string to parse and an issue date.
        /// </param>
        public virtual Task<MetarReport> ParseMetarAsync( MetarParseOptions parseOptions, CallContext context = null )
        {
            return CallApiTask( api => api.ParseMetar( parseOptions, context ) );
        }

        /// <summary>
        /// Parses and validates a raw METAR report and returns parsed information.
        /// </summary>
        /// <param name="parseOptions">
        /// The METAR string to parse and an issue date.
        /// </param>
        public virtual MetarReport ParseMetar( MetarParseOptions parseOptions, CallContext context = null )
        {
            return AccessTaskResult( ParseMetarAsync( parseOptions, context ) );
        }
    }
}
