using TechTalk.SpecFlow;
using Newtonsoft.Json.Linq;

namespace EmsApi.Client.Tests.Features
{
	[Binding, Scope( Feature = "Swagger" )]
	public class SwaggerSteps : FeatureTest
	{
		[When( @"I run GetSpecificationJson and enter the string '(.*)'" )]
		public void WhenIRunGetSpecificationJsonAndEnterTheString( string p0 )
		{
			m_result.Object = m_api.Swagger.GetSpecificationJson( p0 );
		}

		[When( @"I run GetSpecification and enter the string '(.*)'" )]
		public void WhenIRunGetSpecificationAndEnterTheString( string p0 )
		{
			m_result.Object = m_api.Swagger.GetSpecification( p0 );
		}

		[Then( @"A raw JSON string is returned" )]
		public void ThenARawJSONStringIsReturned()
		{
			m_result.Object.ShouldNotBeNullOfType<string>();
		}

		[Then( @"A parsed JObject is returned" )]
		public void ThenAParsedJObjectIsReturned()
		{
			m_result.Object.ShouldNotBeNullOfType<JObject>();
		}
	}
}
