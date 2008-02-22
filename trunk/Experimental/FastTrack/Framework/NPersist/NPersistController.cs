using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.NPersist.Framework;
using Puzzle.NPersist.Framework.Mapping;
using System.Collections;
using Puzzle.FastTrack.Framework.Filtering;
using Puzzle.NPersist.Framework.Querying;
using Puzzle.FastTrack.Framework.Controllers;
using Puzzle.NPersist.Framework.Mapping.Serialization;
using Puzzle.NPersist.Framework.Mapping.Transformation;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Reflection;

namespace Puzzle.FastTrack.Framework.NPersist
{
    public class NPersistController : DomainControllerBase
    {
        public NPersistController() : base()
        {
            string mapPath = System.Configuration.ConfigurationManager.AppSettings["MapPath"];
            string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            string assemblyName = System.Configuration.ConfigurationManager.AppSettings["DomainAssembly"];

            if (!string.IsNullOrEmpty(assemblyName))
                context = new Context(mapPath);
            else
                context = GetContext(mapPath);
            
            context.SetConnectionString(connectionString);
        }

        private IContext GetContext(string mapPath)
        {
            IMapSerializer serialier = new DefaultMapSerializer();
            IDomainMap domainMap = DomainMap.Load(mapPath, serialier, false, false);

            IContext context = new Context(domainMap);
            GenerateAssembly(context);

            return context;
        }

        private void GenerateAssembly(IContext context)
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
                    Assembly domain = cr.CompiledAssembly;
                    context.AssemblyManager.RegisterAssembly(domain);
                    this.DomainAssembly = domain;
                }
            }
            catch (Exception ex)
            {
                this.DomainAssembly = null;
            }
        }

        private IContext context;
        public virtual IContext Context
        {
            get { return context; }
            set { context = value; }
        }
	

        public override object GetObjectByIdentity(object identity, Type type)
        {
            return context.GetObjectById(identity, type); 
        }

        public override string GetObjectIdentity(object obj)
        {
            return context.ObjectManager.GetObjectIdentity(obj);
        }

        public override IList GetObjectsOfType(Type type)
        {
            return context.GetObjects(type);
        }

        public override IList GetObjectsOfType(Type type, Filter filter)
        {
            if (filter.FilterItems.Count < 1)
                return GetObjectsOfType(type);

            NPathQuery query = new NPathQuery();

            query.PrimaryType = type;

            string npathString = "Select * From " + GetTypeNameFromType(type) + " Where ";
            foreach (FilterItem item in filter.FilterItems)
            {
                npathString += item.PropertyName;
                switch (item.MatchCondition)
                {
                    case MatchCondition.Equals:
                        npathString += " = ";
                        break;
                    case MatchCondition.Like:
                        npathString += " LIKE ";
                        item.Value = "%" + item.Value + "%";
                        break;
                    case MatchCondition.LargerThan:
                        npathString += " > ";
                        break;
                    case MatchCondition.SmallerThan:
                        npathString += " < ";
                        break;
                }
                npathString += "? And ";

                query.Parameters.Add(item.Value);
            }
            npathString = npathString.Substring(0, npathString.Length - 4);

            query.Query = npathString;

            return context.GetObjectsByNPath(query);
        }

        public override object ExecuteCreateObject(Type type)
        {
            return context.CreateObject(type);
        }

        public override void ExecuteSaveObject(object obj)
        {
            context.Commit();
        }

        public override void ExecuteDeleteObject(object obj)
        {
            context.DeleteObject(obj);
            context.Commit();
        }

        public override bool IsListProperty(Type type, string propertyName)
        {
            IClassMap classMap = this.Context.DomainMap.GetClassMap(type);
            if (classMap != null)
            {
                IPropertyMap propertyMap = classMap.GetPropertyMap(propertyName);
                if (propertyMap != null)
                    return propertyMap.IsCollection;
            }
            return base.IsListProperty(type, propertyName);
        }

        public override Type GetListPropertyItemType(object obj, string propertyName)
        {
            IClassMap classMap = this.Context.DomainMap.GetClassMap(obj.GetType());
            if (classMap != null)
            {
                IPropertyMap propertyMap = classMap.GetPropertyMap(propertyName);
                if (propertyMap != null)
                    return GetTypeFromTypeName(propertyMap.ItemType);
            }
            return null;
        }

        public override bool IsNullableProperty(Type type, string propertyName)
        {
            IClassMap classMap = this.Context.DomainMap.GetClassMap(type);
            if (classMap != null)
            {
                IPropertyMap propertyMap = classMap.GetPropertyMap(propertyName);
                if (propertyMap != null)
                    return propertyMap.GetIsNullable();
            }
            return false;
        }

        public override bool IsReadOnlyProperty(Type type, string propertyName)
        {
            if (base.IsReadOnlyProperty(type, propertyName))
                return true;

            IClassMap classMap = this.Context.DomainMap.GetClassMap(type);
            if (classMap != null)
            {
                IPropertyMap propertyMap = classMap.GetPropertyMap(propertyName);
                if (propertyMap != null)
                {
                    if (propertyMap.IsReadOnly)
                        return true;
                    if (propertyMap.IsAssignedBySource)
                        return true;
                }
            }

            return false;
        }

        public override bool GetPropertyNullStatus(object obj, string propertyName)
        {
            IClassMap classMap = this.Context.DomainMap.GetClassMap(obj.GetType());
            if (classMap != null)
            {
                IPropertyMap propertyMap = classMap.GetPropertyMap(propertyName);
                if (propertyMap != null)
                    return this.Context.GetNullValueStatus(obj, propertyName);
            }
            return false;
        }

        public override void SetPropertyNullStatus(object obj, string propertyName, bool isNull)
        {
            IClassMap classMap = this.Context.DomainMap.GetClassMap(obj.GetType());
            if (classMap != null)
            {
                IPropertyMap propertyMap = classMap.GetPropertyMap(propertyName);
                if (propertyMap != null)
                    this.Context.SetNullValueStatus(obj, propertyName, isNull);
            }
        }

        public override IList GetTypeNames()
        {
            IList typeNames = new ArrayList();
            foreach (IClassMap classMap in this.context.DomainMap.ClassMaps)
                typeNames.Add(classMap.Name);

            return typeNames;
        }

        public override Type GetTypeFromType(Type type)
        {
            return this.Context.AssemblyManager.GetType(type);
        }

        public override string GetTypeNameFromType(Type type)
        {
            IClassMap classMap = this.context.DomainMap.GetClassMap(type);
            if (classMap != null)
            {
                return classMap.GetName();
            }
            return type.Name;
        }

        public override Type GetTypeFromTypeName(string typeName)
        {
            IClassMap classMap = this.context.DomainMap.GetClassMap(typeName);
            if (classMap != null)
            {
                Type type = this.Context.AssemblyManager.GetTypeFromClassMap(classMap);
                if (type != null)
                    return type;
            }
            return DomainAssembly.GetType(typeName);
        }


        public override void Dispose()
        {
            base.Dispose();

            if (this.context != null)
                this.context.Dispose();
        }
    }
}
