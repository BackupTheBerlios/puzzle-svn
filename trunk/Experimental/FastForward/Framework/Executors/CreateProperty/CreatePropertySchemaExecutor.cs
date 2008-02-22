using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.SideFX.Framework;
using Puzzle.FastForward.Framework.Service;
using System.Data;
using Puzzle.SideFX.Framework.Execution;

namespace Puzzle.FastForward.Framework.Executors.CreateProperty
{
    public class CreatePropertySchemaExecutor : IExecutor   
    {

        #region IExecutor Members

        public void OnBeginning(object sender, EngineEventArgs e)
        {
        }

        public void OnExecuting(object sender, ExecutionCancelEventArgs e)
        {
            IEngine engine = sender as IEngine;
            CreatePropertyCommand createPropertyCommand = CreatePropertyCommand.Evaluate(engine, e.Command);
            if (createPropertyCommand == null)
                return;

            if (createPropertyCommand.Multiplicity == Multiplicity.None)
                CreateProperty(createPropertyCommand, sender, e);
            else
                CreateRelationship(createPropertyCommand, sender, e);
        }


        public void OnExecuted(object sender, Puzzle.SideFX.Framework.Execution.ExecutionEventArgs e)
        {
        }

        public void OnCommitting(object sender, EngineEventArgs e)
        {
            IEngine engine = sender as IEngine;
            ISchemaService schemaService = engine.GetService<ISchemaService>();
            schemaService.Commit();
        }

        public void OnAborting(object sender, EngineEventArgs e)
        {
            IEngine engine = sender as IEngine;
            ISchemaService schemaService = engine.GetService<ISchemaService>();
            schemaService.Abort();
        }

        #endregion

        public void CreateProperty(CreatePropertyCommand createPropertyCommand, object sender, ExecutionCancelEventArgs e)
        {
            IEngine engine = sender as IEngine;
            ISchemaService schemaService = engine.GetService<ISchemaService>();
            IDatabaseService databaseService = engine.GetService<IDatabaseService>();

            string className = createPropertyCommand.ClassName;
            string propertyName = createPropertyCommand.Name;

            string propertyType = createPropertyCommand.Type.ToString();

            string columnName = createPropertyCommand.ColumnName;
            DbType columnType = databaseService.GetDbType(createPropertyCommand.Type, createPropertyCommand.StringLength); ;

            string tableName = schemaService.GetTableForClass(className);

            //Add a property to the class
            schemaService.CreateProperty(className, propertyName, propertyType);

            //Set the nullability of the property
            schemaService.SetPropertyMetaData(className, propertyName, PropertyMetaData.Nullable, createPropertyCommand.Nullable);

            //Add a column to the table
            schemaService.CreateColumn(tableName, columnName, columnType);

            //Set the nullability of the column
            schemaService.SetColumnMetaData(tableName, columnName, ColumnMetaData.Nullable, createPropertyCommand.Nullable);

            //Map the property to the column in the schema
            schemaService.MapPropertyToColumn(className, propertyName, tableName, columnName);
        }

        public void CreateRelationship(CreatePropertyCommand createPropertyCommand, object sender, Puzzle.SideFX.Framework.Execution.ExecutionCancelEventArgs e)
        {
            IEngine engine = sender as IEngine;
            ISchemaService schemaService = engine.GetService<ISchemaService>();
            IDatabaseService databaseService = engine.GetService<IDatabaseService>();

            string className = createPropertyCommand.ClassName;
            string propertyName = createPropertyCommand.Name;

            string propertyType = createPropertyCommand.Type.ToString();

            string columnName = createPropertyCommand.ColumnName;
            DbType columnType = DbType.Int32; //TODO: Get the column of the identity property

            string tableName = schemaService.GetTableForClass(className);

            switch (createPropertyCommand.Multiplicity)
            {
                case Multiplicity.OneToMany:
                case Multiplicity.OneToOne:

                    //Add a property to the class
                    schemaService.CreateProperty(className, propertyName, propertyType);

                    //Set the nullability of the property
                    schemaService.SetPropertyMetaData(className, propertyName, PropertyMetaData.Nullable, createPropertyCommand.Nullable);

                    //Add a column to the table
                    schemaService.CreateColumn(tableName, columnName, columnType);

                    //Set the nullability of the column
                    schemaService.SetColumnMetaData(tableName, columnName, ColumnMetaData.Nullable, createPropertyCommand.Nullable);

                    //Map the property to the column in the schema
                    schemaService.MapPropertyToColumn(className, propertyName, tableName, columnName);

                    break;

                case Multiplicity.ManyToMany:

                    //Add a property to the class
                    schemaService.CreateListProperty(className, propertyName, propertyType);

                    //Add a many-many table
                    //schemaService.CreateTable(tableName, columnName, columnType);

                    break;

                case Multiplicity.ManyToOne:

                    //Add a property to the class
                    schemaService.CreateListProperty(className, propertyName, propertyType);

                    //Add a column to the table
                    //schemaService.CreateColumn(tableName, columnName, columnType);

                    break;

            }

        }

    }
}
