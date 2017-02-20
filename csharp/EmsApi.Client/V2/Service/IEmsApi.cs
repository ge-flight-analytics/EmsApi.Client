using System.Collections.Generic;
using System.Threading.Tasks;

using Refit;
using EmsApi.Client.V2.Model;

namespace EmsApi.Client.V2
{
    /// <summary>
    /// The interface methods that match the REST signature for the EMS API.
    /// </summary>
    /// <remarks>
    /// These methods are used by the Refit library to generate a implementation to
    /// make the actual HTTP calls, so they need to mirror the exposed routes exactly.
    /// The library uses code generation to compile the stub implementation into this
    /// assembly, so every time this project is built a RefitStubs.g.cs file is generated
    /// in the obj folder and included.
    /// 
    /// Note: It's important to not use constants in the REST attributes below, the library
    /// does not properly generate stubs for these (it seems to omit them).
    /// </remarks>
    public interface IEmsApi
    {
        /// <summary>
        /// Returns a set of EMS systems the currently logged in user is able to access.
        /// </summary>
        [Get( "/v2/ems-systems" )]
        Task<IEnumerable<EmsSystem>> GetEmsSystems();

        /// <summary>
        /// Returns some additional server information about the ems system.
        /// </summary>
        /// <param name="emsSystemId">
        /// The unique identifier of the EMS system.
        /// </param>
        [Get( "/v2/ems-systems/{emsSystemId}" )]
        Task<EmsSystemInfo> GetEmsSystemInfo( int emsSystemId );

        /// <summary>
        /// Ping an EMS system to verify that the specified system is currently up and running.
        /// </summary>
        /// <param name="emsSystemId">
        /// The unique identifier of the EMS system.
        /// </param>
        [Get( "/v2/ems-systems/{emsSystemId}/ping" )]
        Task<bool> PingEmsSystem( int emsSystemId );
    }
}
