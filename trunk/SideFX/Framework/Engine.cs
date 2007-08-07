using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.SideFX.Framework.Parsing;
using Puzzle.SideFX.Framework.Execution;
using System.Collections;

namespace Puzzle.SideFX.Framework
{
    public class Engine : IEngine
    {

        private IDictionary<Type, object> services = new Dictionary<Type, object>();

        public IDictionary<Type, object> Services
        {
            get { return services; }
            set { services = value; }
        }

        private IList<IExecutor> executors = new List<IExecutor>();

        public IList<IExecutor> Executors
        {
            get { return executors; }
            set { executors = value; }
        }	
	
        #region IEngine Members

        public object GetService()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        //void Execute(ITypedCommand typedCommand)
        //{
        //    IList<ITypedCommand> typedCommands = new List<ITypedCommand>();
        //    typedCommands.Add(typedCommand);
        //    Execute(typedCommands);
        //}

        //void Execute(IList<ITypedCommand> typedCommands)
        //{
        //    IList<Command> commands = new List<Command>();

        //    foreach (ITypedCommand typedCommand in typedCommands)
        //        commands.Add(typedCommand.ToCommand());

        //    Execute(commands);
        //}

        public void Execute(string text)
        {
        //    Execute(Parser.Parse(text));
        //}

        //public void Execute(IList<Command> commands)
        //{
            IList<Command> commands = Parser.Parse(text);
            EngineEventArgs engineArgs = new EngineEventArgs();
            foreach (IExecutor executor in executors)
                executor.OnBeginning(this, engineArgs);

            try
            {
                IDictionary<Command, ExecutionCancelEventArgs> commandArgs = new Dictionary<Command, ExecutionCancelEventArgs>();
                foreach (Command command in commands)
                {
                    ExecutionCancelEventArgs args = new ExecutionCancelEventArgs(command);
                    commandArgs[command] = args;
                    foreach (IExecutor executor in executors)
                    {
                        executor.OnExecuting(this, args);
                        //If one executor raises the cancel flag, 
                        //the rest of the execution for the command is canceled
                        if (args.Cancel)
                            break;
                    }
                }

                foreach (Command command in commands)
                {
                    ExecutionCancelEventArgs cancelArgs = commandArgs[command];
                    if (!cancelArgs.Cancel)
                    {
                        ExecutionEventArgs args = new ExecutionEventArgs(command);
                        foreach (IExecutor executor in executors)
                            executor.OnExecuted(this, args);
                    }
                }

                //this is really not satisfactory: 
                //should be replaced with elevating two-phase commits, i.e tx scopes
                foreach (IExecutor executor in executors)
                    executor.OnCommitting(this, engineArgs);
            }
            catch (Exception ex)
            {
                foreach (IExecutor executor in executors)
                    executor.OnAborting(this, engineArgs);
            }

        }

        public void RegisterService<T>(object service)
        {
            services[typeof(T)] = service;
        }

        public void RegisterExecutor(IExecutor executor)
        {
            executors.Add(executor);
        }

        public T GetService<T>()
        {
            return (T) services[typeof(T)];
        }

        #endregion

    }
}
