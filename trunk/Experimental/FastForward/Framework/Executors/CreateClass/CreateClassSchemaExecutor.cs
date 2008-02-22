using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.SideFX.Framework;
using Puzzle.SideFX.Framework.Execution;
using System.Data;
using Puzzle.FastForward.Framework.Service;

namespace Puzzle.FastForward.Framework.Executors.CreateClass
{
    public class CreateClassSchemaExecutor : IExecutor
    {
        #region IExecutor Members

        public void OnBeginning(object sender, EngineEventArgs e)
        {
        }

        public void OnExecuting(object sender, ExecutionCancelEventArgs e)
        {
            CreateClassCommand createClassCommand = CreateClassCommand.Evaluate(e.Command);
            if (createClassCommand == null)
                return;
            IEngine engine = sender as IEngine;
            ISchemaService schemaService = engine.GetService<ISchemaService>();

            string name = createClassCommand.Name;
            string propertyName = "Id";
            string propertyType = "System.Int32";
            string columnName = name + "Id";
            DbType columnType = DbType.Int32;

            //Add the class to the schema
            schemaService.CreateClass(name);

            //Add a property to the class
            schemaService.CreateProperty(name, propertyName, propertyType);

            //Mark the property as an identity property
            schemaService.SetPropertyMetaData(name, propertyName, PropertyMetaData.Identity, true);

            //Mark the property as not nullable
            schemaService.SetPropertyMetaData(name, propertyName, PropertyMetaData.Nullable, false);

            //Mark the property as assigned by the data source
            schemaService.SetPropertyMetaData(name, propertyName, PropertyMetaData.SourceAssigned, true);

            //Add the table to the schema
            schemaService.CreateTable(createClassCommand.TableName);

            //Add a column to the table
            schemaService.CreateColumn(createClassCommand.TableName, columnName, columnType);

            //Mark the column as a primary key column
            schemaService.SetColumnMetaData(createClassCommand.TableName, columnName, ColumnMetaData.PrimaryKey, true);

            //Mark the column as not nullable
            schemaService.SetColumnMetaData(createClassCommand.TableName, columnName, ColumnMetaData.Nullable, false);

            //Mark the column as an auto increasing column
            schemaService.SetColumnMetaData(createClassCommand.TableName, columnName, ColumnMetaData.AutoIncreaser, true);

            //Map the class to the table in the schema
            schemaService.MapClassToTable(name, createClassCommand.TableName);

            //Map the property to the column in the schema
            schemaService.MapPropertyToColumn(name, propertyName, createClassCommand.TableName, columnName);
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
    }
}
