using Newtonsoft.Json;

namespace EmsApi.Client.V2.Model
{
    public class Airport : CommonModels.IdModel
    {
        /// <summary>
        /// The ICAO code for the airport.
        /// </summary>
        [JsonProperty( "codeIcao" )]
        public string IcaoCode { get; set; }

        /// <summary>
        /// The FAA code for the airport.
        /// </summary>
        [JsonProperty( "codeFaa" )]
        public string FaaCode { get; set; }

        /// <summary>
        /// The preferred code for the airport. The type of code
        /// returned depends on the EMS system setting.
        /// </summary>
        [JsonProperty( "codePreferred" )]
        public string AirportCode { get; set; }

        /// <summary>
        /// The name for the airport.
        /// </summary>
        [JsonProperty]
        public string Name { get; set; }

        /// <summary>
        /// The city in which the airport resides.
        /// </summary>
        [JsonProperty]
        public string City { get; set; }

        /// <summary>
        /// The country in which the airport resides.
        /// </summary>
        [JsonProperty]
        public string Country { get; set; }

        /// <summary>
        /// The latitude for the airport.
        /// </summary>
        [JsonProperty]
        public double Latitude { get; set; }

        /// <summary>
        /// The longitude for the airport.
        /// </summary>
        [JsonProperty]
        public double Longitude { get; set; }

        /// <summary>
        /// The elevation of the airport.
        /// </summary>
        [JsonProperty]
        public double Elevation { get; set; }
    }
}
