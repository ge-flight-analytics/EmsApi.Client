using System;
using EmsApi.Client.V2;
using EmsApi.Dto.V2;

namespace EmsApi.Example.UraTree
{
	class Program
	{
		static void Main( string[] args )
		{
			using( var api = new EmsApiService() )
			{
				api.CachedEmsSystem = 1;
				AnalyticGroupContents rootGroup = api.Analytics.GetGroup();
				EnumerateRecursive( rootGroup, api );
			}
		}

		private static void EnumerateRecursive( AnalyticGroupContents group, EmsApiService api, int indent = 0 )
		{
			// Calculate indent string.
			string indentStr = string.Empty;
			for( int i = 0; i < indent; ++i )
				indentStr += "\t";

			// For this example we are only showing groups, but individual analytics are easy as well:
			// foreach( AnalyticInfo analytic in group.Analytics )
			//		Console.WriteLine( string.Format( "{0} | Analytic: {1}", indentStr, analytic.Name ) );

			// Recurse into the groups.
			foreach( AnalyticGroup innerGroup in group.Groups )
			{
				Console.WriteLine( string.Format( "{0} | {1}", indentStr, innerGroup.Name ) );
				EnumerateRecursive( api.Analytics.GetGroup( innerGroup.Id ), api, indent + 1 );
			}
		}
	}
}
