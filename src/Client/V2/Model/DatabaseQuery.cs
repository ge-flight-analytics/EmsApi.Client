using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EmsApi.Dto.V2
{
    /// <summary>
    /// Object used to define a database query.
    /// </summary>
    public class DatabaseQuery
    {
        /// <summary>
        /// Constructs a new query wrapper.
        /// </summary>
        public DatabaseQuery()
        {
            Raw = new DbQuery
            {
                Select = new ObservableCollection<SelectColumn>()
            };
            ValueFormat = DbQueryFormat.Display;
        }

        /// <summary>
        /// The raw query object.
        /// </summary>
        public DbQuery Raw { get; set; }

        /// <summary>
        /// Gets or sets the raw JSON for the query.
        /// </summary>
        public string Json
        {
            get { return Raw.ToJson(); }
            set { Raw = DbQuery.FromJson( value ); }
        }

        /// <summary>
        /// The format to return values in. The default value is <seealso cref="DbQueryFormat.Display"/>.
        /// When this is set to display, discrete type database fields will return their string display
        /// name instead of their underlying id.
        /// </summary>
        public DbQueryFormat? ValueFormat
        {
            get { return Raw.Format; }
            set { Raw.Format = value; }
        }

        /// <summary>
        /// Limits the results to the top N rows.
        /// </summary>
        public int? Top
        {
            get { return Raw.Top; }
            set { Raw.Top = value; }
        }

        /// <summary>
        /// When true, duplicate rows are removed from the returned values.
        /// </summary>
        public bool? Distinct
        {
            get { return Raw.Distinct; }
            set { Raw.Distinct = value; }
        }

        /// <summary>
        /// Adds a field to be included in the select statement of the query.
        /// </summary>
        public void SelectField( string fieldId, SelectColumnAggregate? aggregate = null, SelectColumnFormat? format = null )
        {
            var column = new SelectColumn
            {
                FieldId = fieldId,
                Aggregate = aggregate,
                Format = format
            };

            Raw.Select.Add( column );
        }

        /// <summary>
        /// Adds a field to group results by.
        /// </summary>
        public void GroupByField( string fieldId )
        {
            if( Raw.GroupBy == null )
                Raw.GroupBy = new ObservableCollection<GroupByColumn>();

            Raw.GroupBy.Add( new GroupByColumn { FieldId = fieldId } );
        }

        /// <summary>
        /// Adds a field to order results by.
        /// </summary>
        public void OrderByField( string fieldId, OrderByColumnAggregate? aggregate = null, OrderByColumnOrder? order = null )
        {
            if( Raw.OrderBy == null )
                Raw.OrderBy = new ObservableCollection<OrderByColumn>();

            var column = new OrderByColumn
            {
                FieldId = fieldId,
                Aggregate = aggregate,
                Order = order
            };

            Raw.OrderBy.Add( column );
        }

        /// <summary>
        /// Sets the operator to use when evaluating multiple filters.
        /// </summary>
        public void SetFilterOperator( FilterOperator op )
        {
            EnsureFilterExists();
            Raw.Filter.Operator = op;
        }

        /// <summary>
        /// Adds the given filter arguments as a new filter, which is ANDed together with
        /// other filters by default.
        /// </summary>
        public void AddFilter( FilterOperator op, params FilterArgument[] arguments )
        {
            EnsureFilterExists();
            Filter filter = Raw.Filter;

            if( filter.Args == null )
                filter.Args = new ObservableCollection<FilterArgument>();

            var wrapper = new FilterArgument
            {
                Type = FilterArgumentType.Filter
            };

            var inner = new Filter
            {
                Operator = op,
                Args = new ObservableCollection<FilterArgument>()
            };

            foreach( var arg in arguments )
                inner.Args.Add( arg );

            wrapper.Value = inner;
            filter.Args.Add( wrapper );
        }

        /// <summary>
        /// Adds a filter which compares a field to the given constant.
        /// </summary>
        public void AddConstantFilter( FilterOperator op, string field, object constant )
        {
            var args = new List<FilterArgument>
            {
                new FilterArgument { Type = FilterArgumentType.Field, Value = field },
                new FilterArgument { Type = FilterArgumentType.Constant, Value = constant }
            };

            AddFilter( op, args.ToArray() );
        }

        /// <summary>
        /// Adds a filter which compares a boolean field to true or false.
        /// </summary>
        public void AddBooleanFilter( string field, bool matchState )
        {
            AddFilter( matchState ? FilterOperator.IsTrue : FilterOperator.IsFalse,
                new FilterArgument { Type = FilterArgumentType.Field, Value = field } );
        }

        private void EnsureFilterExists()
        {
            if( Raw.Filter == null )
            {
                Raw.Filter = new Filter
                {
                    Operator = FilterOperator.And
                };
            }
        }
    }
}
