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
            Raw = new Query
            {
                Select = new ObservableCollection<AnalyticSelect>()
            };
        }

        /// <summary>
        /// The raw query object.
        /// </summary>
        public Query Raw { get; set; }

        /// <summary>
        /// The optional start offset in the data, in seconds from the start of the data. 
        /// If not specified, the beginning of the available data is used.
        /// </summary>
        public double? Start
        {
            get { return Raw.Start; }
            set { Raw.Start = value; }
        }

        /// <summary>
        ///  The optional end offset in the data, in seconds from the start of the data. 
        ///  If not specified, the end of the available data is used.
        /// </summary>
        public double? End
        {
            get { return Raw.End; }
            set { Raw.End = value; }
        }

        /// <summary>
        /// The optional set of offsets to return in query results. Use as an alternative to 
        /// specifying a range of values using start and end.
        /// </summary>
        public double[] Offsets
        {
            get
            {
                if( Raw.Offsets == null )
                    return new double[0];

                return Raw.Offsets.ToArray();
            }
            set
            {
                Raw.Offsets = new ObservableCollection<double>( value );
            }
        }

        /// <summary>
        /// The optional limit that specifies the total number of offsets and values to return 
        /// in query results
        /// </summary>
        public int? Size
        {
            get { return Raw.Size; }
            set { Raw.Size = value; }
        }

        /// <summary>
        /// This determines how to treat data values for offsets that are not sampled. If left unset 
        /// this defaults to 'leaveBlank'.
        /// </summary>
        public QueryUnsampledDataMode? UnsampledDataMode
        {
            get { return Raw.UnsampledDataMode; }
            set { Raw.UnsampledDataMode = value; }
        }

        /// <summary>
        /// This optional parameter replaces any unsampled (blank) values with a constant value. This 
        /// defaults to a null value.
        /// </summary>
        public string UnsampledValue
        {
            get { return Raw.UnsampledValue; }
            set { Raw.UnsampledValue = value; }
        }

        /// <summary>
        /// This optional parameter replaces any values that come back as DNE( does not exist) with a 
        /// constant value.This defaults to "DNE".
        /// </summary>
        public string DoesNotExistValue
        {
            get { return Raw.DoesNotExistValue; }
            set { Raw.DoesNotExistValue = value; }
        }

        /// <summary>
        /// This defines the way in which the offsets are determined. This is required if Offsets or 
        /// Size has not been set.
        /// </summary>
        public OffsetType OffsetType
        {
            get { return Raw.OffsetType; }
            set { Raw.OffsetType = value; }
        }

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
