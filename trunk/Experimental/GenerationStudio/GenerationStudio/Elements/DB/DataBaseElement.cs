using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenerationStudio.Attributes;
using GenerationStudio.Gui;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Odbc;
using System.Windows.Forms;
using System.Data.OracleClient;
using System.Data.Common;
using GenerationStudio.AppCore;

namespace GenerationStudio.Elements
{
    public enum ProviderType
    {

        SqlServer,
        OleDb,
        Odbc,
        Oracle,
    }

    [Serializable]
    [ElementParent(typeof(RootElement))]
    [ElementName("DataBase")]
    [ElementIcon("GenerationStudio.Images.database.gif")]
    public class DataBaseElement : NamedElement
    {
        public string ConnectionString { get; set; }

        [OptionalField]
        private ProviderType providerType = ProviderType.SqlServer;
        public ProviderType ProviderType 
        {
            get
            {
                return providerType;
            }
            set
            {
                providerType = value;
            }
        }

        [ElementVerb("Test connection")]
        public void TestConnection(IHost host)
        {
            IDbConnection connection = GetConnection();
            try
            {
                connection.Open();
                MessageBox.Show("Connection was successful");
            }
            catch
            {
                MessageBox.Show("Connection failed");
            }
        }

        public IDbConnection GetConnection()
        {
            if (ProviderType == ProviderType.SqlServer)
                return new SqlConnection(ConnectionString);

            if (ProviderType == ProviderType.OleDb)
                return new OleDbConnection(ConnectionString);

            if (ProviderType == ProviderType.Odbc)
                return new OdbcConnection(ConnectionString);

            if (ProviderType == ProviderType.Oracle)
                return new OracleConnection(ConnectionString);

            throw new NotSupportedException("Unknown provider");
        }

        [ElementVerb("Reload structure from DB")]
        public void SyncFromDataSourceToTableModel(IHost host)
        {
            using (IDbConnection connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    DataTable schema = GetSchema(connection);
                    DataTable tables = GetSchema(connection, "tables");
                    DataTable columns = GetSchema(connection,"columns");

                    Dictionary<string, TableElement> tableElements = new Dictionary<string, TableElement>();
                    foreach (TableElement child in AllChildren)
                    {
                        string key = child.GetDisplayName ();
                        tableElements.Add(key, child);
                    }

                    ClearChildren();

                    Engine.MuteNotify();

                    
                    foreach (DataRow row in tables.Rows)
                    {
                        
                        string tableName = row["table_name"].ToString ();

                        TableElement table = null;
                        if (!tableElements.ContainsKey(tableName))
                        {
                            table = new TableElement();
                            table.Name = tableName;
                            tableElements.Add(tableName, table);                         
                        }

                        table = tableElements[tableName];
                        AddChild(table);

                        IDbCommand command = connection.CreateCommand();
                        command.CommandText = string.Format("select * from [{0}]", tableName);
                        IDataReader reader = command.ExecuteReader();
                        DataTable tableSchema = reader.GetSchemaTable();
                        reader.Close();

                        table.ClearChildren();
                        foreach (DataRow columnRow in tableSchema.Rows)
                        {
                            ColumnElement column = new ColumnElement();
                            column.Name = (string)columnRow["ColumnName"];
                            column.IsNullable = (bool)columnRow["AllowDBNull"];
                            column.IsAutoIncrement = (bool)columnRow["IsAutoIncrement"];
                            column.IsIdentity = (bool)columnRow["IsIdentity"];
                            column.DefaultValue = "";
                            column.NativeType = (Type)columnRow["DataType"];
                            column.IsUnique = (bool)columnRow["IsUnique"];
                            column.Ordinal = (int)columnRow["ColumnOrdinal"];
                            column.DbType = (string)columnRow["DataTypeName"];
                            column.MaxLength = (int)columnRow["ColumnSize"];
                            table.AddChild(column);
                        }
                    }

                    //foreach (DataRow row in columns.Rows)
                    //{
                    //    string tableName = row["table_name"].ToString();
                    //    string columnName = row["column_name"].ToString();

                    //    TableElement table = tableElements[tableName];
                    //    ColumnElement column = new ColumnElement();
                    //    column.Name = columnName;
                    //    column.IsNullable = false;
                    //    column.IsPK = false;
                    //    column.DefaultValue = "";


                    //    table.AddChild(column);

                    //}

                    Engine.EnableNotify();
                }
                catch
                {
                    MessageBox.Show("Connection failed");
                }

                host.RefreshProjectTree();
            }
        }

        private DataTable GetSchema(IDbConnection connection)
        {
            if (connection is DbConnection)
            {
                return ((DbConnection)connection).GetSchema();
            }

            throw new NotSupportedException("Unknown provider");
        }

        private DataTable GetSchema(IDbConnection connection,string collectionName)
        {
            if (connection is DbConnection)
            {
                return ((DbConnection)connection).GetSchema(collectionName);
            }

            throw new NotSupportedException("Unknown provider");
        }


        [ElementVerb("Save structure to DB")]
        public void SyncTableModelToDataSource(IHost host)
        {
        }
    }
}
