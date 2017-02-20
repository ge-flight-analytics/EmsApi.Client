using System;
using EmsApi.Client.V2;
using EmsApi.Client.V2.Model;


namespace EmsApi.Example.DotnetFrameworkConsole
{
    /// <summary>
    /// Example that demonstrates using the EMS API from a .NET framework console application.
    /// The application must target .NET 4.6.1 at a minimum. You must install System.Net.HTTP 4.3 
    /// and Refit 3.0.1 from nuget until the EMS API library is distributed as a nuget package.
    /// </summary>
    class Program
    {
        static void Main( string[] args )
        {
            Console.WriteLine( "Hello from .NET framework." );
            var config = new EmsApiServiceConfiguration();

            // Allow the user to override the endpoint, but provide a default.
            Console.Write( "Enter Endpoint URL [{0}]: ", config.Endpoint );
            string endpoint = Console.ReadLine();
            if( !string.IsNullOrEmpty( endpoint ) )
                config.Endpoint = endpoint;

            Console.Write( "Enter Username [{0}]: ", config.UserName );
            string user = Console.ReadLine();
            if( !string.IsNullOrEmpty( user ) )
                config.UserName = user;

            string defaultPw = !string.IsNullOrEmpty( config.Password ) ? "********" : string.Empty;
            Console.Write( "Enter Password [{0}]: ", defaultPw );
            string password = Console.ReadLine();
            if( !string.IsNullOrEmpty( password ) )
                config.Password = password;

            using( var api = new EmsApiService( config ) )
            {
                // List all the connected EMS systems on the command line.
                foreach( EmsSystem ems in api.EmsSystems.GetAll() )
                {
                    // Retrieve server details about the system.
                    EmsSystemInfo server = api.EmsSystems.GetSystemInfo( ems.Id );
                    Console.WriteLine( string.Format( "{0} - {1} - {2} - EMS version {3} - {4}", ems.Id, ems.Name, ems.Description, server.ServerVersion, server.UtcTimeStamp ) );
                }
            }

            Console.WriteLine( "Press any key to close..." );
            Console.ReadKey();
        }
    }
}
