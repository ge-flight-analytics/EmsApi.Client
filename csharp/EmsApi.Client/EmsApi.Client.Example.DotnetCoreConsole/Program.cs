using System;
using EmsApi.Client.V2;
using EmsApi.Client.V2.Model;

namespace EmsApi.Client.Example.DotnetCoreConsole
{
    /// <summary>
    /// Example that demonstrates using the EMS API from a .NET core console application.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var config = new EmsApiServiceConfiguration( EmsApiEndpoints.Beta );

            // Allow the user to override the endpoint, but provide a default.
            Console.Write( string.Format( "Enter Endpoint URL [{0}]: ", config.Endpoint ) );
            string endpoint = Console.ReadLine();
            if( !string.IsNullOrEmpty( endpoint ) )
                config.Endpoint = endpoint;

            Console.Write( "Enter Username: " );
            config.UserName = Console.ReadLine();

            Console.Write( "Enter Password: " );
            config.Password = Console.ReadLine();

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