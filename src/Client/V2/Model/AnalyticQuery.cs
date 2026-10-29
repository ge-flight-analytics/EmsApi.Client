using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace EmsApi.Dto.V2
{
    public class AnalyticQuery
    {
        public AnalyticQuery()
        {
            Raw = new Query
            {
                Select = new Collection<AnalyticSelect>()
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
                Raw.Offsets = new Collection<double>( value );
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
        /// This optional parameter, when set to true, displays discrete values as their respective string values. This defaults to true.
        /// This only applies when performing queries using "offsetType".
        /// </summary>
        public bool? DiscretesAsStrings
        {
            get { return Raw.DiscretesAsStrings; }
            set { Raw.DiscretesAsStrings = value; }
        }

        /// <summary>
        /// This optional parameter, when set to true, removes samples where the value is DNE. This defaults to false.
        /// This only applies when performing queries using "offsetType" and where there is only a single analytic in the "Select" array.
        /// This can be useful if the client was just going to remove the DNEs themselves as it will reduce the data sent over the wire.
        /// </summary>
        public bool? RemoveDNEs
        {
            get { return Raw.RemoveDNEs; }
            set { Raw.RemoveDNEs = value; }
        }

        /// <summary>
        /// This optional parameter, when set to true, removes duplicate samples. This defaults to false.
        /// This removes samples that would not impact a client side linear interpolation of the data. For instance, if the data is 
        /// A, A, A, A, B, B, B, C, D, E, E, E, E then this would only return A, A, B, B, C, D, E, E. For a given run of a value it
        /// would return the first and last samples in that run.
        /// If both this and RemoveDNEs are set, then RemoveDNEs is applied first. This is because the removal of DNEs can create longer
        /// runs of the same value which can then be further reduced by RemoveDuplicates.
        /// This only applies when performing queries using "offsetType" and where there is only a single analytic in the "Select" array.
        /// This can be useful if the client was just going to remove the duplicate samples themselves as it will reduce the data sent
        /// over the wire.
        /// </summary>
        public bool? RemoveDuplicates
        {
            get { return Raw.RemoveDuplicates; }
            set { Raw.RemoveDuplicates = value; }
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
