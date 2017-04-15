using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;

namespace EmsApi.Dto.V2
{
    public class AnalyticQuery
    {
        public AnalyticQuery()
        {
            Raw = new Query();
            Raw.Select = new ObservableCollection<AnalyticSelect>();
        }

        /// <summary>
        /// The raw query object.
        /// </summary>
        public Query Raw { get; set; }

        /// <summary>
        /// Gets or sets the raw JSON for the query.
        /// </summary>
        public string Json
        {
            get { return Raw.ToJson(); }
            set { Raw = Query.FromJson( value ); }
        }

        /// <summary>
        /// Adds an analytic to be returned.
        /// </summary>
        public void SelectAnalytic( string analyticId )
        {
            var select = new AnalyticSelect { AnalyticId = analyticId };
            Raw.Select.Add( select );
        }
    }
}
