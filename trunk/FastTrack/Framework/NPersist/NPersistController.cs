using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.NPersist.Framework;
using Puzzle.NPersist.Framework.Mapping;
using System.Collections;
using Puzzle.FastTrack.Framework.Filtering;
using Puzzle.NPersist.Framework.Querying;

namespace Puzzle.FastTrack.Framework.Controllers
{
    public class NPersistController : DomainControllerBase
    {
        public NPersistController() : base()
        {
            string mapPath = System.Configuration.ConfigurationManager.AppSettings["MapPath"];
            string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];

            context = new Context(mapPath);
            context.SetConnectionString(connectionString);
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

        public override object CreateObject(Type type)
        {
            return context.CreateObject(type);
        }

        public override void SaveObject(object obj)
        {
            context.Commit();
        }

        public override void DeleteObject(object obj)
        {
            context.DeleteObject(obj);
            context.Commit();
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
