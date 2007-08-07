using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.SideFX.Framework;
using Puzzle.FastForward.Framework.Service;
using Puzzle.SideFX.Framework.Execution;

namespace Puzzle.FastForward.Framework.Executors.CreateObject
{
    public class CreateObjectDomainExecutor : IExecutor
    {

        #region IExecutor Members

        public void OnBeginning(object sender, EngineEventArgs e)
        {
        }

        public void OnExecuting(object sender, ExecutionCancelEventArgs e)
        {
            IEngine engine = sender as IEngine;

            CreateObjectCommand createObjectCommand = CreateObjectCommand.Evaluate(engine, e.Command);
            if (createObjectCommand == null)
                return;

            IObjectService objectService = engine.GetService<IObjectService>();
            ISchemaService schemaService = engine.GetService<ISchemaService>();
            IDatabaseService databaseService = engine.GetService<IDatabaseService>();

            databaseService.EnsureTransaction();

            Type type = objectService.GetTypeByName(createObjectCommand.ClassName);

            //Create a new object of the class
            object obj = objectService.CreateObject(type);

            //Set the properties of the new object
            if (createObjectCommand.Values != null)
            {
                foreach (string propertyName in createObjectCommand.Values.Keys)
                {
                    if (schemaService.HasProperty(createObjectCommand.ClassName, propertyName))
                    {
                        objectService.SetProperty(obj, propertyName, createObjectCommand.Values[propertyName]);
                    }
                }
            }
        }

        public void OnExecuted(object sender, Puzzle.SideFX.Framework.Execution.ExecutionEventArgs e)
        {
        }

        public void OnCommitting(object sender, EngineEventArgs e)
        {
            IEngine engine = sender as IEngine;
            IObjectService objectService = engine.GetService<IObjectService>();
            IDatabaseService databaseService = engine.GetService<IDatabaseService>();
            objectService.Commit();
            databaseService.CommitEventualTransaction();
        }

        public void OnAborting(object sender, EngineEventArgs e)
        {
            IEngine engine = sender as IEngine;
            IObjectService objectService = engine.GetService<IObjectService>();
            IDatabaseService databaseService = engine.GetService<IDatabaseService>();
            objectService.Abort();
            databaseService.AbortEventualTransaction();
        }

        #endregion
    }
}
