using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EmsApi.Dto.V2
{
    // This adds properties for derived field types in the API that are not visible in the OpenAPI spec.

    /// <summary>
    /// Indicates the way in which numeric field values should be interpreted in query results.
    /// </summary>
    public enum NumberInterpretation
    {
        /// <summary>
        /// The number value represents a piece of data, such as number of years or velocity. Data values can usually 
        /// be ordered, added, subtracted, etc.
        /// </summary>
        DataValue,

        /// <summary>
        /// The number identifies something, such as a record number. Identifier values generally should not be 
        /// manipulated arithmetically.
        /// </summary>
        Identifier
    }

    /// <summary>
    /// Indicates how the date and time information is encoded in a date time field.
    /// </summary>
    public enum DateTimeType
    {
        /// <summary>
        ///  A complete date and time UTC value.
        /// </summary>
        DateTimeUtc,

        /// <summary>
        /// UTC date information only (as "year/month/date").
        /// </summary>
        DateOnlyUtc,

        /// <summary>
        /// UTC year and month information only (as "year/month").
        /// </summary>
        YearMonthUtc
    }

    /// <summary>
    /// Indicates the suggested method for displaying and/or editing values for a string field.
    /// </summary>
    public enum StringDisplayStyle
    {
        /// <summary>
        /// Indicates the values should be displayed and/or edited with a single-line text box.
        /// </summary>
        SingleLine,

        /// <summary>
        /// Indicates the values should be displayed and/or edited with a multi-line text box.
        /// </summary>
        MultiLine
    }

    public partial class Field
    {
        /// <summary>
        /// The possible discrete values for the field, if <see cref="Type"/> is <see cref="FieldType.Discrete"/>.
        /// </summary>
        [JsonProperty( "discreteValues", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore )]
        public IDictionary<long, string> DiscreteValues { get; set; }

        /// <summary>
        /// The number edit style for the field, if <see cref="Type"/> is <see cref="FieldType.Number"/>.
        /// </summary>
        [JsonProperty( "numberEditStyle", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore )]
        public string NumberEditStyle { get; set; }

        /// <summary>
        /// The number interpretation for the field, if <see cref="Type"/> is <see cref="FieldType.Number"/>.
        /// </summary>
        [JsonProperty( "numberInterpretation", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore )]
        [JsonConverter( typeof( StringEnumConverter ) )]
        public NumberInterpretation? NumberInterpretation { get; set; }

        /// <summary>
        /// The possible minimum value for the field, if <see cref="Type"/> is <see cref="FieldType.Number"/>.
        /// </summary>
        [JsonProperty( "numberMinValue", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore )]
        public double? NumberMinValue { get; set; }

        /// <summary>
        /// The possible maximum value for the field, if <see cref="Type"/> is <see cref="FieldType.Number"/>.
        /// </summary>
        [JsonProperty( "numberMaxValue", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore )]
        public double? NumberMaxValue { get; set; }

        /// <summary>
        /// The possible number units for the field, if <see cref="Type"/> is <see cref="FieldType.Number"/>.
        /// </summary>
        [JsonProperty( "numberUnits", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore )]
        public string NumberUnits { get; set; }

        /// <summary>
        /// True if a date field is in local time, if <see cref="Type"/> is <see cref="FieldType.DateTime"/>.
        /// </summary>
        [JsonProperty( "dateTimeLocal", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore )]
        public bool? DateTimeLocal { get; set; }

        /// <summary>
        /// True if a date field is in local time, if <see cref="Type"/> is <see cref="FieldType.DateTime"/>.
        /// </summary>
        [JsonProperty( "dateTimeType", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore )]
        [JsonConverter( typeof( StringEnumConverter ) )]
        public DateTimeType? DateTimeType { get; set; }

        /// <summary>
        /// The expected length of a string field, if <see cref="Type"/> is <see cref="FieldType.String"/>.
        /// </summary>
        [JsonProperty( "stringLength", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore )]
        public int? StringLength { get; set; }

        /// <summary>
        /// The expected display style of a string field, if <see cref="Type"/> is <see cref="FieldType.String"/>.
        /// </summary>
        [JsonProperty( "stringDisplayStyle", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore )]
        [JsonConverter( typeof( StringEnumConverter ) )]
        public StringDisplayStyle? StringDisplayStyle { get; set; }
    }
}
