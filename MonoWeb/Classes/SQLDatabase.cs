using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;
using System.Configuration;

namespace SQLDatabase
{
	public class DatabaseRow
	{
        Dictionary<string, string> Fields;

        public DatabaseRow()
        {
            Fields = new Dictionary<string, string>();
        }

        public void Add(string column_name, string field)
        {
            Fields.Add(column_name, field);
        }

        /// <summary>Gets or sets the item at the given column.</summary>
        /// <param name="s">String index representing column name to access.</param>
        /// <returns>String containing the item at this position.</returns>
        public string this[string column_name]
        {
            get
            {
                return Fields[column_name];
            }
            set
            {
                Fields[column_name] = value;
            }
        }

        /// <summary>Gets or sets the item at the given index (zero based).</summary>
        /// <param name="n">Index (zero based).</param>
        /// <returns>String containing the item at this position.</returns>
        public string this[int n]
        {
            get
            {
                return Fields.ElementAt(n).Value;
            }
            set
            {
                Fields[GetFieldName(n)] = value;
            }
        }

        public string GetFieldName(int n)
        {
            return Fields.ElementAt(n).Key;
        }

        public int RowLength
        {
            get
            {
                return Fields.Count;
            }
        }

        public bool ContainsField(string field)
        {
            return Fields.Keys.Contains(field);
        }
    }

    public class DatabaseTable
    {
        private List<string> ColumnNames;
        private List<DatabaseRow> Rows;

        string TableName;

        /// <summary>
        /// Load the given table from the database.
        /// </summary>
        /// <param name="table_name"></param>
        public DatabaseTable(string table_name)
        {
            Load(table_name, "SELECT * FROM " + table_name);
        }


        public DatabaseTable(string table_name, string sql_query)
        {
            Load(table_name, sql_query);
        }


        private void Load(string table_name, string sql_query)
        {
            ColumnNames = new List<string>();
            Rows = new List<DatabaseRow>();
            TableName = table_name;

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);    // Grab the conection string for accessing the database.

            SqlCommand command = new SqlCommand(sql_query, connection);    // Associate the given sql query with the connection.

            connection.Open();

            SqlDataReader reader = command.ExecuteReader(); // Send the sql query to the database for processing.

            for (int n = 0; n < reader.FieldCount; ++n)
            {
                ColumnNames.Add(reader.GetName(n));
            }

            while (reader.Read())   // Access each student in turn (continue until there are no students left to read).
            {
                DatabaseRow dr = new DatabaseRow();

                for (int column = 0; column < ColumnCount; ++column)
                {
                    dr.Add(ColumnNames[column], reader[column].ToString());
                }

                Rows.Add(dr);
            }

            connection.Close();
        }

    
        public DatabaseRow FindRow(string field, string value)
        {
            for (int r = 0; r < RowCount; ++r)
            {
                DatabaseRow dr = GetRow(r);
                
                if (dr.ContainsField(field))
                {
                    if (dr[field] == value) return dr;
                }
            }

            return null;
        }

        /// <summary>
        /// Find a given row and return the index for this in the table (i.e. which row number is this). Zero based.
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int FindRowIndex(string field, string value)
        {
            for (int r = 0; r < RowCount; ++r)
            {
                DatabaseRow dr = GetRow(r);

                if (dr.ContainsField(field))
                {
                    if (dr[field] == value) return r;
                }
            }

            return 0;
        }

        public int ColumnCount
        {
            get
            {
                return ColumnNames.Count;
            }
        }

        public DatabaseRow GetRow(int n)
        {
            return Rows[n];
        }

        public int RowCount
        {
            get
            {
                return Rows.Count;
            }
        }


        /// <summary>
        /// Returns a new/blank DatabaseRow with same columns as the owner table.
        /// </summary>
        /// <returns>DatabaseRow</returns>
        public DatabaseRow NewRow()
        {
            DatabaseRow dr = new DatabaseRow();

            for (int column = 0; column < ColumnCount; ++column)
            {
                dr.Add(ColumnNames[column], "");
            }

            return dr;
        }

        /// <summary>
        /// Insert a new row into the table and the underlying database.
        /// </summary>
        /// <param name="row_to_insert">The new DatabaseRow to insert. Assumes this contains appropriate data.</param>
        public void Insert(DatabaseRow row_to_insert)
        {
            // Create a properly formatted SQL INSERT command...

            string columns = "(";
            string values = "(";

            for (int n = 0; n < ColumnCount; ++n)
            {
                if (row_to_insert[n] != "")
                {
                    columns += ColumnNames[n] + ", ";
                    values += "'" + row_to_insert[n] + "', ";
                }
            }

            columns = columns.Substring(0, columns.Length - 2) + ")";
            values = values.Substring(0, values.Length - 2) + ")";

            // Send the INSERT...

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);    // Grab the conection string for accessing the database.

            string sql_command = "INSERT INTO " + TableName + " " + columns + " VALUES " + values; // The SQL statement to process - the INSERT statement.

            SqlCommand command = new SqlCommand(sql_command, connection);    // Associate the sql query with the connection.

            connection.Open();

            command.ExecuteNonQuery();

            connection.Close();

            Rows.Add(row_to_insert);
        }

        /// <summary>
        /// Update a given row. Assumes that ID field is present and has a unique value for each row.
        /// </summary>
        /// <param name="row_to_insert">The new DatabaseRow to update. Assumes this contains appropriate data.</param>
        public void Update(DatabaseRow row_to_update)
        {
            // Create a properly formatted SQL UPDATE command...

            string u = "";

            for (int n = 0; n < ColumnCount; ++n)
            {
                if (row_to_update[n] != "" && ColumnNames[n] != "Player")
                {
                    u += ColumnNames[n] + "='" + row_to_update[n] + "', ";
                }
            }

            u = u.Substring(0, u.Length - 2);


            // Send the UPDATE to the database...

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);    // Grab the conection string for accessing the database.

            string sql_command = "UPDATE " + TableName + " SET " + u + " WHERE PLAYER='" + row_to_update["Player"] + "';";

            SqlCommand command = new SqlCommand(sql_command, connection);    // Associate the sql query with the connection.

            connection.Open();

            command.ExecuteNonQuery();

            connection.Close();

            Rows[FindRowIndex("Player", row_to_update["Player"])] = row_to_update;  // Rewrite this new row over the existing row in the in-memory database.
        }


        /// <summary>
        /// Calculate and return the next ID number in the sequence, so it can be used to insert new rows. Assumes that the table has an integer field called 'ID'.
        /// </summary>
        /// <returns>Int containing the new ID number.</returns>
        public int GetNextID()
        {
            if (RowCount == 0) return 0;

            return int.Parse(Rows[RowCount - 1]["Player"] + 1);
        }
    }
}