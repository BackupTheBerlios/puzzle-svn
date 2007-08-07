using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.SideFX.Framework;
using Puzzle.FastForward.Framework.Service;
using Puzzle.FastForward.Framework.Executors.CreateClass;
using Puzzle.FastForward.Framework.Executors.CreateProperty;
using Puzzle.FastForward.Framework.Executors.CreateObject;
using Puzzle.FastForward.Framework.Executors.DisplayObjects;
using Puzzle.FastForward.Framework.Executors.DeleteObjects;

namespace Puzzle.FastForward.Framework
{
    public class FastForwardEngine : Engine
    {
        public FastForwardEngine()
        {
            this.RegisterService<IConfigurationService>(new ConfigurationService(this));
            this.RegisterService<IDatabaseService>(new SqlServerDatabaseService(this));
            this.RegisterService<ISqlService>(new SqlServerSqlService(this));
            this.RegisterService<IRenderService>(new TextRenderService(this));
            this.RegisterService<IDisplayService>(new ConsoleDisplayService(this));
            this.RegisterService<ILoggingService>(new ConsoleLoggingService(this));

            this.RegisterExecutor(new CreateObjectDomainExecutor());
            this.RegisterExecutor(new DeleteObjectsDomainExecutor());
            this.RegisterExecutor(new DisplayObjectsDomainExecutor());

            this.RegisterExecutor(new CreateClassSchemaExecutor());
            this.RegisterExecutor(new CreateClassDatabaseExecutor());

            this.RegisterExecutor(new CreatePropertySchemaExecutor());
            this.RegisterExecutor(new CreatePropertyDatabaseExecutor());
        }
    }
}
