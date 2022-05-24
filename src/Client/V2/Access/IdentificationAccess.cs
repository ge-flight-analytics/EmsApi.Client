using System.Threading.Tasks;
using EmsApi.Dto.V2;

namespace EmsApi.Client.V2.Access
{
    public class IdentificationAccess : RouteAccess
    {
        /// <summary>
        ///  Returns the flight identification information for the flight on the EMS system.
        /// </summary>
        /// <param name="flightId">
        /// The flight to retrieve identification information for.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.</param>
        public virtual Task<Identification> GetFlightIdentificationAsync( int flightId, CallContext context = null )
        {
            return CallApiTask( api => api.GetFlightIdentification( flightId, context ) );
        }

        /// <summary>
        ///  Returns the flight identification information for the flight on the EMS system.
        /// </summary>
        /// <param name="flightId">
        /// The flight to retrieve identification information for.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.</param>
        public virtual Identification GetFlightIdentification( int flightId, CallContext context = null )
        {
            return AccessTaskResult( GetFlightIdentificationAsync( flightId, context ) );
        }
    }
}
