using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace EmsApi.Dto.V2
{
    /// <summary>
    /// Wrapper for database query results.
    /// </summary>
    public class DatabaseQueryResult
    {
        internal DatabaseQueryResult( IEnumerable<DbQueryResultHeader> headers )
        {
            Headers = headers.ToArray();
            m_orderedColumnIds = new List<string>();
            m_orderedColumnNames = new List<string>();
            foreach( DbQueryResultHeader header in Headers )
            {
                m_orderedColumnIds.Add( header.Id );
                m_orderedColumnNames.Add( header.Name );
            }

            Rows = new List<Row>();
        }

        /// <summary>
        /// The rows from the query. Each row contains multiple values (one for each column).
        /// </summary>
        public List<Row> Rows { get; private set; }

        /// <summary>
        /// The headers from the query. These describe the columns returned in the query.
        /// The index or column ids of these headers can be used to index into the <seealso cref="Rows"/>
        /// collection.
        /// </summary>
        public DbQueryResultHeader[] Headers { get; private set; }

        /// <summary>
        /// The column ids in an ordered list.
        /// </summary>
        private List<string> m_orderedColumnIds;

        /// <summary>
        /// The column names in an ordered list.
        /// </summary>
        private List<string> m_orderedColumnNames;

        /// <summary>
        /// Converts query result rows into Row objects, and adds them to the collection.
        /// </summary>
        internal void AddRows( DatabaseQuery query, IEnumerable<object> rows )
        {
            if( query.Raw.OrderBy == null || query.Raw.OrderBy.Count == 0 )
            {
                // We can access the enumerable in parallel, the order doesn't matter.
                Rows.AddRange( rows.AsParallel().Select(
                    obj => CreateRowFromObservableCollection( obj ) ) );

                return;
            }

            // Access the enumerable in a series, because the order matters.
            Rows.AddRange( rows.Select( obj => CreateRowFromObservableCollection( obj ) ) );
        }

        /// <summary>
        /// Converts query result rows into Row objects, and then executes the callback
        /// with each row. This is intended to be used by code that wants to perform some
        /// other aggregation over the returned query rows, to avoid the double enumeration
        /// cost of converting to the Row format and then reading those back in a different
        /// loop. This should be more efficient in that case because the data retrieval is
        /// network bound, so we have CPU time to spare.
        /// </summary>
        internal void CallbackRows( DatabaseQuery query, IEnumerable<object> rows, Action<Row> callback )
        {
            if( query.Raw.OrderBy == null || query.Raw.OrderBy.Count == 0 )
            {
                // We can fire callbacks in parallel, the order doesn't matter.
                rows.AsParallel().ForAll( obj => callback( CreateRowFromObservableCollection( obj ) ) );
                return;
            }

            // Access the enumerable in a series, because the order matters.
            foreach( object raw in rows )
                callback( CreateRowFromObservableCollection( raw ) );
        }

        /// <summary>
        /// Converts the raw object to an observable collection of objects. This is the format used by
        /// the simple query routes.
        /// </summary>
        private Row CreateRowFromObservableCollection( object collection )
        {
            var converted = (ObservableCollection<object>)collection;
            return new Row( m_orderedColumnIds, m_orderedColumnNames, converted.ToArray() );
        }

        /// <summary>
        /// A single data row from a database query. These may be accessed by column index, id, or name.
        /// </summary>
        public class Row
        {
            internal Row( List<string> columnIds, List<string> columnNames, object[] values )
            {
                m_columnIds = columnIds;
                m_columnNames = columnNames;
                m_values = values;
            }

            /// <summary>
            /// Returns the column value at the given index.
            /// </summary>
            public object this[int index]
            {
                get { return ScrubReturnValue( index ); }
            }

            /// <summary>
            /// Returns the column value for the given column id.
            /// </summary>
            public object this[string columnId]
            {
                get
                {
                    int index = m_columnIds.IndexOf( columnId );
                    return ScrubReturnValue( index );
                }
            }

            /// <summary>
            /// Returns the value with the given column name.
            /// </summary>
            public object GetValueByColumnName( string columnName )
            {
                int index = m_columnNames.IndexOf( columnName );
                return ScrubReturnValue( index );
            }

            /// <summary>
            /// Handles formatting for well-known value types.
            /// </summary>
            private object ScrubReturnValue( int columnIndex )
            {
                object raw = m_values[columnIndex];

                // Trim strings, some database fields are fixed width.
                if( raw is string )
                {
                    return ((string)raw).Trim();
                }

                return raw;
            }

            // Note: These are lists of strings so we only keep a pointer to them.
            // An array of strings actually copies the values, since in .NET a string
            // acts like a nullable value type.
            private List<string> m_columnIds;
            private List<string> m_columnNames;
            private object[] m_values;
        }
    }
}
