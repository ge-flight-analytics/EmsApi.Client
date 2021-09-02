using System.Collections.Generic;
using TechTalk.SpecFlow;
using FluentAssertions;

using EmsApi.Dto.V2;

namespace EmsApi.Tests.Features
{
    [Binding, Scope( Feature = "AdminUser" )]
    public class AdminUserSteps : FeatureTest
    {
        [When( @"I run GetUsers" )]
        public void WhenIRunGetUsers()
        {
            m_result.Object = m_api.AdminUser.GetUsers();
        }

        [Then( @"An enumerable of AdminUser objects are returned" )]
        public void ThenAnEnumerableOfAdminUserObjectsReturned()
        {
            m_result.Object.ShouldNotBeNullOfType<List<AdminUser>>();
        }

        [When( @"I run GetUsers with a filter" )]
        public void WhenIRunGetUsersWithAFilter()
        {
            m_result.Object = m_api.AdminUser.GetUsers("emsweb");
        }

        [Then( @"An enumerable with one AdminUser object is returned" )]
        public void ThenAnEnumerableWithOneAdminUserObjectIsReturned()
        {
            m_result.Object.ShouldNotBeNullOfType<List<AdminUser>>();
            var users = (IEnumerable<AdminUser>)m_result.Object;
            users.Should().OnlyContain( u => u.Username == "efoqa\\EmsWeb" );
        }
    }
}
