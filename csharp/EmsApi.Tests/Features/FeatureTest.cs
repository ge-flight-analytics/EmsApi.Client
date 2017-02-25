using System;
using System.Collections;
using System.Collections.Generic;

using EmsApi.Client.V2;
using EmsApi.Client.V2.Model;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace EmsApi.Client.Tests
{
	/// <summary>
	/// Provides some common variables and test methods to be used for SpecFlow tests
	/// (the *.feature files and their associated .cs files).
	/// </summary>
	public abstract class FeatureTest : TestBase
	{
		protected EmsApiService m_api;
		protected Results m_result = new Results();

		[Given( @"A valid API endpoint" )]
		public void GivenAValidApiEndpoint()
		{
			m_api = NewService();
			m_api.Authenticate().Should().BeTrue();
		}

		[Given( @"The cached EMS system id of (.*)" )]
		public void GivenTheCachedEMSSystemIdOf( int p0 )
		{
			m_api.CachedEmsSystem = p0;
			m_api.CachedEmsSystem.ShouldBeEquivalentTo( p0 );
		}

		[Then( @"The Id property is (.*)" )]
		public void ThenTheIdPropertyIs( int p0 )
		{
			var model = m_result.Object as CommonModels.IdModel;
			if( model == null )
				throw new InvalidCastException( "Cannot cast m_result.Object into CommonModels.IdModel for test." );

			model.Id.ShouldBeEquivalentTo( p0 );
		}

		[Then( @"The Description is not empty" )]
		public void ThenTheDescriptionIsNotEmpty()
		{
			var model = m_result.Object as CommonModels.IdAndDescriptionModel;
			if( model == null )
				throw new InvalidCastException( "Cannot cast m_result.Object into CommonModels.IdAndDescriptionModel for test." );

			model.Description.Should().NotBeNullOrEmpty();
		}

		/// <summary>
		/// Used to pass results from one test step to another.
		/// </summary>
		protected class Results
		{
			public object Object { get; set; }

			public IEnumerable<object> Enumerable { get; set; }

			public bool Bool { get; set; }
		}
	}

	public static class TestExtensions
	{
		/// <summary>
		/// Asserts that the given object is of the specified type, and is not null.
		/// </summary>
		public static void ShouldNotBeNullOfType<T>( this object input )
		{
			input.Should().BeOfType<T>().And.Should().NotBeNull();
		}

		/// <summary>
		/// Asserts that the given enumerable is not null or empty, and all the values
		/// are of the specified type.
		/// </summary>
		public static void ShouldNotBeNullOrEmptyOfType<T>( this IEnumerable input )
		{
			// This has a problem when ANDed together, so they are asserted separately.
			input.Should().NotBeNullOrEmpty();
			input.Should().AllBeOfType<T>();
		}
	}
}
