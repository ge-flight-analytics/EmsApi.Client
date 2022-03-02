using System;
using System.Collections.ObjectModel;
using EmsApi.Client.V2;
using EmsApi.Dto.V2;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace EmsApi.Tests.SpecFlow
{
    [Binding, Scope( Feature = "AnalyticSets" )]
    public class AnalyticSetsSteps : FeatureTest
    {
        private NewAnalyticCollection m_newAnalyticCollection;
        private UpdateAnalyticCollection m_updateAnalyticCollection;
        private NewAnalyticSet m_newAnalyticSet;
        private UpdateAnalyticSet m_updateAnalyticSet;
        private NewAnalyticSetGroup m_newAnalyticSetGroup;
        private UpdateAnalyticSetGroup m_updateAnalyticSetGroup;

        [When( @"I run GetAnalyticSetGroups" )]
        public void WhenIRunGetAnalyticSetGroups()
        {
            m_result.Object = m_api.AnalyticSets.GetAnalyticSetGroups();
        }

        [When( @"I run GetAnalyticSetGroup and enter an analytic group id of '(.*)'" )]
        public void WhenIRunGetAnalyticSetGroupAndEnterAnAnalyticGroupIdOf( string p0 )
        {
            m_result.Object = m_api.AnalyticSets.GetAnalyticSetGroup( p0 );
        }

        [When( @"I run GetAnalyticSet and enter a group id of '(.*)' and a set name of '(.*)'" )]
        public void WhenIRunGetAnalyticSetAndEnterAGroupIdOfAndASetNameOf( string p0, string p1 )
        {
            m_result.Object = m_api.AnalyticSets.GetAnalyticSet( p0, p1 );
        }

        [Then( @"An AnalyticSetGroup object is returned" )]
        public void ThenAnAnalyticSetGroupObjectIsReturned()
        {
            m_result.Object.ShouldNotBeNullOfType<AnalyticSetGroup>();
        }

        [Then( @"An AnalyticSet object is returned" )]
        public void ThenAnAnalyticSetObjectIsReturned()
        {
            m_result.Object.ShouldNotBeNullOfType<AnalyticSet>();
        }

        [Then( @"It has the name '(.*)'" )]
        public void ThenItHasTheName( string p0 )
        {
            m_result.GetPropertyValue<string>( "Name" ).Should().Be( p0 );
        }

        [Given( @"I have a NewAnalyticSetGroup with name '(.*)'" )]
        public void GivenIHaveANewAnalyticSetGroupWithName( string p0 )
        {
            m_newAnalyticSetGroup = new NewAnalyticSetGroup
            {
                Name = p0,
                ParentGroupId = "Root"
            };
        }

        [When( @"I run CreateAnalyticSetGroup with group id '(.*)'" )]
        public void WhenIRunCreateAnalyticSetGroupWithGroupId( string p0 )
        {
            m_result.Object = m_api.AnalyticSets.CreateAnalyticSetGroup( m_newAnalyticSetGroup );
        }

        [Then( @"An AnalyticSetGroupCreated object is returned" )]
        public void ThenAnAnalyticSetGroupCreatedObjectIsReturned()
        {
            m_result.Object.ShouldNotBeNullOfType<AnalyticSetGroupCreated>();
        }

        [Given( @"I have a UpdateAnalyticSetGroup" )]
        public void GivenIHaveAUpdateAnalyticSetGroup()
        {
            m_updateAnalyticSetGroup = new UpdateAnalyticSetGroup
            {
                Name = "Updated Analytic Set Group",
                ParentGroupId = "Root"
            };
        }

        [When( @"I run UpdateAnalyticSetGroup with group id '(.*)'" )]
        public void WhenIRunUpdateAnalyticSetGroupWithGroupId( string p0 )
        {
            m_result.Object = m_api.AnalyticSets.UpdateAnalyticSetGroup( p0, m_updateAnalyticSetGroup );
        }

        [Then( @"An AnalyticSetGroupUpdated object is returned" )]
        public void ThenAnAnalyticSetGroupUpdatedObjectIsReturned()
        {
            m_result.Object.ShouldNotBeNullOfType<AnalyticSetGroupUpdated>();
        }

        [When( @"I run GetAnalyticSetGroup with group id '(.*)'" )]
        public void WhenIRunGetAnalyticSetGroupWithGroupId( string p0 )
        {
            m_result.Object = m_api.AnalyticSets.GetAnalyticSetGroup( p0 );
        }

        [When( @"I run DeleteAnalyticSetGroup with group id '(.*)'" )]
        public void WhenIRunDeleteAnalyticSetGroupWithGroupId( string p0 )
        {
            m_api.AnalyticSets.DeleteAnalyticSetGroup( p0 );
        }

        [Then( @"the analytic set group with id '(.*)' does not exist" )]
        public void ThenTheAnalyticSetGroupWithIdDoesNotExist( string p0 )
        {
            Action getDeletedCollection = () =>
            {
                m_api.AnalyticSets.GetAnalyticSetGroup( p0 );
            };
            getDeletedCollection.Should().Throw<EmsApiException>()
                .WithMessage( "One or more errors occurred. (Response status code does not indicate success: 404 (Not Found).)" );
        }

        [Given( @"I have a NewAnalyticSet with name '(.*)'" )]
        public void GivenIHaveANewAnalyticSetWithName( string p0 )
        {
            m_newAnalyticSet = new NewAnalyticSet
            {
                Name = p0,
                Description = "new analytic set",
                Items = new Collection<NewAnalyticSetItem>
                {
                    new NewAnalyticSetItem {
                        AnalyticId = "H4sIAAAAAAAEAJ2STW/CMAyGz0PiPyAO3NIkbekHsEqTuCDBBcS0qxunEKkkrA1jP39tUWDVtstuie3ntf0mi62sTfkBeSlXKLVVhZLV6PNU6nqmnsdHa88zSq/Xq3cNPFMdqM8Yp2+b9U4c5QmI0rUFLeT4xtyJukvXHoIFYbStQFjHh5TF9AWVt69gnA0Ho9HiNoWsVpgtoVyqk9S1MvoVyotc0G/ZXvXuLEUzsFg2TbJJaedbY+zkYOftuS/iEO+x73+2bMWHg6enXoM2CAgsRT8gPPKRhBEggTBICDCBIAvMgyJpIdqjnNTGoGzvayOgXGFX6GKuplujDfhJMY0lByLzKCehSHnTaiqIz30meYxpDqxTuBONRL/3X5Y476gz8uF9z+tfX2Gv1ful+UWZABA+a2zgQcRJEQRIRJGnJMUkSrFgYRjGP4Xv+HDgkv1fmX0BGyq1a6wCAAA="
                    }
                }
            };
        }

        [When( @"I run CreateAnalyticSet with group id '(.*)'" )]
        public void WhenIRunCreateAnalyticSetWithGroupId( string p0 )
        {
            m_result.Object = m_api.AnalyticSets.CreateAnalyticSet( p0, m_newAnalyticSet );
        }

        [Then( @"An AnalyticSetCreated object is returned" )]
        public void ThenAnAnalyticSetCreatedObjectIsReturned()
        {
            m_result.Object.ShouldNotBeNullOfType<AnalyticSetCreated>();
        }

        [When( @"I run GetAnalyticSet with group id '(.*)' and analytic name '(.*)'" )]
        public void WhenIRunGetAnalyticSetWithGroupIdAndAnalyticName( string p0, string p1 )
        {
            m_result.Object = m_api.AnalyticSets.GetAnalyticSet( p0, p1 );
        }

        [Then( @"it has the description '(.*)'" )]
        public void ThenItHasTheDescription( string p0 )
        {
            m_result.GetPropertyValue<string>( "Description" ).Should().Be( p0 );
        }

        [Given( @"I have a UpdateAnalyticSet" )]
        public void GivenIHaveAUpdateAnalyticSet()
        {
            m_updateAnalyticSet = new UpdateAnalyticSet
            {
                Description = "updated analytic set",
                Items = new Collection<NewAnalyticSetItem>
                {
                    new NewAnalyticSetItem
                    {
                        AnalyticId = "H4sIAAAAAAAEAJ2STW/CMAyGz0PiPyAO3NIkbekHsEqTuCDBBcS0qxunEKkkrA1jP39tUWDVtstuie3ntf0mi62sTfkBeSlXKLVVhZLV6PNU6nqmnsdHa88zSq/Xq3cNPFMdqM8Yp2+b9U4c5QmI0rUFLeT4xtyJukvXHoIFYbStQFjHh5TF9AWVt69gnA0Ho9HiNoWsVpgtoVyqk9S1MvoVyotc0G/ZXvXuLEUzsFg2TbJJaedbY+zkYOftuS/iEO+x73+2bMWHg6enXoM2CAgsRT8gPPKRhBEggTBICDCBIAvMgyJpIdqjnNTGoGzvayOgXGFX6GKuplujDfhJMY0lByLzKCehSHnTaiqIz30meYxpDqxTuBONRL/3X5Y476gz8uF9z+tfX2Gv1ful+UWZABA+a2zgQcRJEQRIRJGnJMUkSrFgYRjGP4Xv+HDgkv1fmX0BGyq1a6wCAAA="
                    }
                }
            };
        }

        [When( @"I run UpdateAnalyticSet with group id '(.*)' and analytic set name '(.*)'" )]
        public void WhenIRunUpdateAnalyticSetWithGroupId( string p0, string p1 )
        {
            m_api.AnalyticSets.UpdateAnalyticSet( p0, p1, m_updateAnalyticSet );
        }

        [When( @"I run DeleteAnalyticSet with group id '(.*)' and analytic set name '(.*)'" )]
        public void WhenIRunDeleteAnalyticSetWithGroupIdAndAnalyticSetName( string p0, string p1 )
        {
            m_api.AnalyticSets.DeleteAnalyticSet( p0, p1 );
        }

        [Then( @"the analytic set in group id '(.*)' and name '(.*)' does not exist" )]
        public void ThenTheAnalyticSetInGroupIdAndNameDoesNotExist( string p0, string p1 )
        {
            Action getDeletedSet = () =>
            {
                m_api.AnalyticSets.GetAnalyticSet( p0, p1 );
            };
            getDeletedSet.Should().Throw<EmsApiException>()
                .WithMessage( $"Unable to find an analytic set with the name {p1} in group {p0}" );
        }

        [Given( @"I have a NewAnalyticCollection with name '(.*)'" )]
        public void GivenIHaveANewAnalyticCollectionWithName( string p0 )
        {
            m_newAnalyticCollection = new NewAnalyticCollection
            {
                Name = p0,
                Description = "new analytic collection",
                Items = new Collection<NewAnalyticCollectionItem>
                {
                    new NewAnalyticCollectionItem {
                        AnalyticId = "H4sIAAAAAAAEAJ2STW/CMAyGz0PiPyAO3NIkbekHsEqTuCDBBcS0qxunEKkkrA1jP39tUWDVtstuie3ntf0mi62sTfkBeSlXKLVVhZLV6PNU6nqmnsdHa88zSq/Xq3cNPFMdqM8Yp2+b9U4c5QmI0rUFLeT4xtyJukvXHoIFYbStQFjHh5TF9AWVt69gnA0Ho9HiNoWsVpgtoVyqk9S1MvoVyotc0G/ZXvXuLEUzsFg2TbJJaedbY+zkYOftuS/iEO+x73+2bMWHg6enXoM2CAgsRT8gPPKRhBEggTBICDCBIAvMgyJpIdqjnNTGoGzvayOgXGFX6GKuplujDfhJMY0lByLzKCehSHnTaiqIz30meYxpDqxTuBONRL/3X5Y476gz8uF9z+tfX2Gv1ful+UWZABA+a2zgQcRJEQRIRJGnJMUkSrFgYRjGP4Xv+HDgkv1fmX0BGyq1a6wCAAA="
                    }
                }
            };
        }

        [When( @"I run CreateAnalyticCollection with group id '(.*)'" )]
        public void WhenIRunCreateAnalyticCollectionWithGroupId( string p0 )
        {
            m_result.Object = m_api.AnalyticSets.CreateAnalyticCollection( p0, m_newAnalyticCollection );
        }

        [Then( @"An AnalyticCollectionCreated object is returned" )]
        public void ThenAnAnalyticCollectionCreatedObjectIsReturned()
        {
            m_result.Object.ShouldNotBeNullOfType<AnalyticCollectionCreated>();
        }

        [When( @"I run GetAnalyticCollection with group id '(.*)' and analytic name '(.*)'" )]
        public void WhenIRunGetAnalyticCollectionWithGroupIdAndAnalyticName( string p0, string p1 )
        {
            m_result.Object = m_api.AnalyticSets.GetAnalyticCollection( p0, p1 );
        }

        [Then( @"An AnalyticCollection object is returned" )]
        public void ThenAnAnalyticCollectionObjectIsReturned()
        {
            m_result.Object.ShouldNotBeNullOfType<AnalyticCollection>();
        }

        [Given( @"I have a UpdateAnalyticCollection" )]
        public void GivenIHaveAUpdateAnalyticCollection()
        {
            m_updateAnalyticCollection = new UpdateAnalyticCollection
            {
                Description = "updated analytic collection",
                Items = new Collection<NewAnalyticCollectionItem>
                {
                    new NewAnalyticCollectionItem
                    {
                        AnalyticId = "H4sIAAAAAAAEAJ2STW/CMAyGz0PiPyAO3NIkbekHsEqTuCDBBcS0qxunEKkkrA1jP39tUWDVtstuie3ntf0mi62sTfkBeSlXKLVVhZLV6PNU6nqmnsdHa88zSq/Xq3cNPFMdqM8Yp2+b9U4c5QmI0rUFLeT4xtyJukvXHoIFYbStQFjHh5TF9AWVt69gnA0Ho9HiNoWsVpgtoVyqk9S1MvoVyotc0G/ZXvXuLEUzsFg2TbJJaedbY+zkYOftuS/iEO+x73+2bMWHg6enXoM2CAgsRT8gPPKRhBEggTBICDCBIAvMgyJpIdqjnNTGoGzvayOgXGFX6GKuplujDfhJMY0lByLzKCehSHnTaiqIz30meYxpDqxTuBONRL/3X5Y476gz8uF9z+tfX2Gv1ful+UWZABA+a2zgQcRJEQRIRJGnJMUkSrFgYRjGP4Xv+HDgkv1fmX0BGyq1a6wCAAA="
                    }
                }
            };
        }

        [When( @"I run UpdateAnalyticCollection with group id '(.*)' and analytic collection name '(.*)'" )]
        public void WhenIRunUpdateAnalyticCollectionWithGroupId( string p0, string p1 )
        {
            m_api.AnalyticSets.UpdateAnalyticCollection( p0, p1, m_updateAnalyticCollection );
        }

        [When( @"I run DeleteAnalyticCollection with group id '(.*)' and analytic collection name '(.*)'" )]
        public void WhenIRunDeleteAnalyticCollectionWithGroupIdAndAnalyticCollectionName( string p0, string p1 )
        {
            m_api.AnalyticSets.DeleteAnalyticCollection( p0, p1 );
        }

        [Then( @"the analytic collection in group id '(.*)' and name '(.*)' does not exist" )]
        public void ThenTheAnalyticCollectionInGroupIdAndNameDoesNotExist( string p0, string p1 )
        {
            Action getDeletedCollection = () =>
            {
                m_api.AnalyticSets.GetAnalyticCollection( p0, p1 );
            };
            getDeletedCollection.Should().Throw<EmsApiException>()
                .WithMessage( $"Unable to find an analytic collection with the name {p1} in group {p0}" );
        }
    }
}
