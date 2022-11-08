namespace EmsApi.Dto.V2
{
    /// <summary>
    /// Represents a column to be modified.
    /// </summary>
    public class UpdateColumn : Column
    {
        /// <summary>
        /// The value for the field. This is an override of the generated  <see cref="Column.Value"/> property to allow for nulls.
        /// After trying to get the EMS API swagger to support allowing nulls and/or manually setting the spec to make this field null-able
        /// (https://stackoverflow.com/questions/55970499/change-nswagstudio-serialization-setting-to-allow-for-nulls)
        /// this was the only solution that worked. 
        /// </summary>
        [Newtonsoft.Json.JsonProperty( "value", Required = Newtonsoft.Json.Required.AllowNull )]
        public new object Value { get; set; } = new object();
    }
}
