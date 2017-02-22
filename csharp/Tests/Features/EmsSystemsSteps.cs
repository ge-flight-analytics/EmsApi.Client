using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using FluentAssertions;

using EmsApi.Client.V2.Model;
using EmsApi.Client.V2.Access;

namespace EmsApi.Client.Tests.Features
{
    [Binding]
    public class EmsSystemsSteps : FeatureTestBase
    {
        private EmsSystemsAccess m_access;
        private IEnumerable<EmsSystem> m_resultSystems;

        [Given(@"A valid service to connect to")]
        public void GivenAValidServiceToConnectTo()
        {
            m_service = NewService();
            m_access = m_service.EmsSystems;
        }

        [Given(@"A valid login to the service")]
        public void GivenAValidLoginToTheService()
        {
            m_service.Authenticate().Should().BeTrue();
        }

        [When( @"I run GetAll" )]
        public void WhenIRunGetAll()
        {
            m_resultSystems = m_access.GetAll();
        }

        [Then( @"EmsSystem objects are returned" )]
        public void ThenEmsSystemObjectsAreReturned()
        {
            m_resultSystems.Should().NotBeNullOrEmpty();
        }

        [When( @"I run Get and enter the value (.*)" )]
        public void WhenIRunGetAndEnterTheValue( int p0 )
        {
            m_resultObj = m_access.Get( p0 );
        }

        [Then( @"An EmsSystem object is returned" )]
        public void ThenAnEmsSystemObjectIsReturned()
        {
            m_resultObj.Should().BeOfType<EmsSystem>().And
                .Should().NotBeNull();
        }

        [When( @"I run ping and enter the value (.*)" )]
        public void WhenIRunPingAndEnterTheValue( int p0 )
        {
            m_resultBool = false;
            m_resultBool = m_access.Ping( p0 );
        }

        [Then( @"The result is true" )]
        public void ThenTheResultIsTrue()
        {
            m_resultBool.Should().BeTrue();
        }

        [When( @"I run GetSystemInfo and enter the value (.*)" )]
        public void WhenIRunGetSystemInfoAndEnterTheValue( int p0 )
        {
            m_resultObj = m_access.GetSystemInfo( p0 );
        }

        [Then( @"An EmsSystemInfo object is returned" )]
        public void ThenAnEmsSystemInfoObjectIsReturned()
        {
            m_resultObj.Should().BeOfType<EmsSystemInfo>().And
                .Should().NotBeNull();
        }
    }
}
