using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.NPersist.Framework;
using System.Reflection;
using Puzzle.SideFX.Framework;
using Puzzle.NPersist.Framework.Mapping;
using Puzzle.NPersist.Framework.Mapping.Serialization;
using Puzzle.NPersist.Framework.Mapping.Transformation;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Collections;
using Puzzle.NPersist.Framework.Querying;

namespace Puzzle.FastForward.Framework.Service
{
    public class NPersistObjectService : ObjectServiceBase
    {
        public NPersistObjectService(IEngine engine) : base(engine)
        {
        }

        private IContext context;
        private Assembly domain;

        public IContext GetContext()
        {
            if (context == null)
            {
                ILoggingService loggingService = engine.GetService<ILoggingService>();
                if (loggingService != null)
                    loggingService.LogInfo(this, "Creating NPersist context");

                IConfigurationService configurationService = engine.GetService<IConfigurationService>();

                string path = configurationService.SchemaFilePath;

                IMapSerializer serialier = new DefaultMapSerializer();
                IDomainMap domainMap = DomainMap.Load(path, serialier, false, false);

                context = new Context(domainMap);
                GenerateAssembly(context);

                context.SetConnectionString(configurationService.ConnectionString);
                context.AutoTransactions = false;
            }
            return context;
        }
        
        private bool GenerateAssembly(IContext context)
        {
            try
            {
                IDomainMap domainMap = context.DomainMap;
                ModelToCodeTransformer modelToCodeTransformer = new ModelToCodeTransformer();
                CodeDomProvider provider = null;

                provider = new CSharpCodeProvider();

                string code = modelToCodeTransformer.ToCode(domainMap, provider);

                CompilerResults cr = modelToCodeTransformer.ToCompilerResults(domainMap, provider);
                if (cr.Errors.Count > 0)
                {
                }
                else
                {
                    domain = cr.CompiledAssembly;
                    context.AssemblyManager.RegisterAssembly(domain);
                }
            }
            catch (Exception ex)
            {
                domain = null;
            }
            if (domain == null)
                return false;

            return true;
        }

        #region IObjectService Members

        public override object CreateObject(Type type)
        {
            ILoggingService loggingService = engine.GetService<ILoggingService>();
            if (loggingService != null)
                loggingService.LogInfo(this, String.Format("Creating object of class: {0}", type.ToString()));

            IContext context = GetContext();
            return context.CreateObject(type);
        }

        public override void DeleteObject(object obj)
        {
            ILoggingService loggingService = engine.GetService<ILoggingService>();
            if (loggingService != null)
                loggingService.LogInfo(this, String.Format("Deleting {0}.{1}",
                    GetTypeName(obj),
                    GetIdentity(obj)));

            IContext context = GetContext();
            context.DeleteObject(obj);
        }

        public override object GetIdentity(object obj)
        {
            IContext context = GetContext();
            return context.ObjectManager.GetObjectIdentity(obj);
        }

        public override string GetTypeName(Type type)
        {
            IContext context = GetContext();
            IClassMap classMap = context.DomainMap.GetClassMap(type);
            if (classMap != null)
                return classMap.Name;
            return type.ToString();
        }

        public override Type GetTypeByName(string className)
        {
            IContext context = GetContext();
            IClassMap classMap = context.DomainMap.MustGetClassMap(className);
            return context.AssemblyManager.GetTypeFromClassMap(classMap);
        }

        public override IList GetObjects(Type type, IDictionary<string, object> match)
        {
            NPathQuery query = CreateQuery(type, match);
            return GetContext().GetObjectsByNPath(query);
        }

        public override IList GetObjects(Type type, string where)
        {
            NPathQuery query = CreateQuery(type, where);
            return GetContext().GetObjectsByNPath(query);
        }

        public override bool IsNull(object obj, string propertyName)
        {
            IContext context = GetContext();
            return context.GetNullValueStatus(obj, propertyName);
        }

        public override void Commit()
        {
            if (this.context != null)
            {
                ILoggingService loggingService = engine.GetService<ILoggingService>();
                if (loggingService != null)
                    loggingService.LogInfo(this, "Committing NPersist unit of work");

                context.Commit();
                context.Dispose();
                context = null;
            }
        }

        public override void Abort()
        {
            if (this.context != null)
            {
                ILoggingService loggingService = engine.GetService<ILoggingService>();
                if (loggingService != null)
                    loggingService.LogInfo(this, "Aborting NPersist unit of work");

                context.Dispose();
                context = null;
            }
        }

        #endregion

        private NPathQuery CreateQuery(Type type, IDictionary<string, object> match)
        {
            IDomainMap domainMap = GetContext().DomainMap;
            IClassMap classMap = domainMap.MustGetClassMap(type);
            StringBuilder query = new StringBuilder();
            query.Append("Select * From " + classMap.Name);
            if (match != null)
            {
                if (match.Keys.Count > 0)
                {
                    query.Append(" Where ");
                    foreach (string propertyName in match.Keys)
                    {
                        query.Append(propertyName + " = ");
                        query.Append("'");
                        query.Append(match[propertyName].ToString());
                        query.Append("'");
                        query.Append(" And ");
                    }
                    query.Length -= 5;
                }
            }

            return new NPathQuery(query.ToString(), type);
        }

        private NPathQuery CreateQuery(Type type, string where)
        {
            IDomainMap domainMap = GetContext().DomainMap;
            IClassMap classMap = domainMap.MustGetClassMap(type);
            StringBuilder query = new StringBuilder();
            query.Append("Select * From " + classMap.Name);
            query.Append(" Where " + where);

            return new NPathQuery(query.ToString(), type);
        }

    }
}
