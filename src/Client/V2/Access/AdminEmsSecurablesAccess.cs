using System.Threading.Tasks;
using EmsApi.Client.V2;
using EmsApi.Client.V2.Access;
using EmsApi.Dto.V2;

/// <summary>
/// Provides access to EMS API admin securable routes.
/// </summary>
/// <remarks>
/// To call these routes you must have Admin level access to the EMS API.
/// </remarks>
public class AdminEmsSecurablesAccess : RouteAccess
{
    /// <summary>
    /// Returns an access check result for whether the provided user has access to the securable.
    /// </summary>
    /// <param name="securableId">
    /// The identifier of a specific EMS securable item.
    /// </param>
    /// <param name="accessRight">
    /// The securable type-specific access right to check access against.
    /// </param>
    /// <param name="username">
    /// The username of the user to check the access right and securable for.
    /// </param>
    /// <param name="context">
    /// The optional call context to include.
    /// </param>
    public virtual Task<EmsSecurableEffectiveAccess> GetAccessForSecurableAsync( string securableId, string accessRight, string username, CallContext context = null )
    {
        return CallApiTask( api => api.AdminGetEmsSecurableAccess( securableId, accessRight, username, context ) );
    }

    /// <summary>
    /// Returns an access check result for whether the provided user has access to the securable.
    /// </summary>
    /// <param name="securableId">
    /// The identifier of a specific EMS securable item.
    /// </param>
    /// <param name="accessRight">
    /// The securable type-specific access right to check access against.
    /// </param>
    /// <param name="username">
    /// The username of the user to check the access right and securable for.
    /// </param>
    /// <param name="context">
    /// The optional call context to include.
    /// </param>
    public virtual EmsSecurableEffectiveAccess GetAccessForSecurable( string securableId, string accessRight, string username, CallContext context = null )
    {
        return AccessTaskResult( GetAccessForSecurableAsync( securableId, accessRight, username, context ) );
    }
}