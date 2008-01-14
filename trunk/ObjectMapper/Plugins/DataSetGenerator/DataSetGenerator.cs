// *
// * Copyright (C) 2008 Jeremy Longo : http://www.puzzleframework.com
// *
// * This library is free software; you can redistribute it and/or modify it
// * under the terms of the GNU Lesser General Public License 2.1 or later, as
// * published by the Free Software Foundation. See the included license.txt
// * or http://www.gnu.org/copyleft/lesser.html for details.
// *
// *

using System; 
using System.Collections.Generic; 
using System.Text; 
using Puzzle.NPersist.Framework.Mapping; 
using Puzzle.ObjectMapper.Plugin; 
using System.Reflection; 
using Puzzle.NPersist.Framework; 
using Puzzle.NPersist.Framework.Mapping.Transformation; 
using System.Data; 
using System.IO; 
using System.CodeDom.Compiler; 
using Puzzle.NPersist.Framework.Enumerations; 
using Microsoft.CSharp; 
using Microsoft.VisualBasic;
using Puzzle.NCore.Framework.Exceptions; 

namespace Puzzle.ObjectMapper.Plugins.DataTableGenerator
{
	[PluginClass("Puzzle", "DataSet Generator")] 
	public class DataTableGenerator
	{
		public DataTableGenerator()
		{
		}

        // Taken from DomainExplorer
        private static Assembly GenerateAssembly(IContext context)
        {
            Assembly domain = null;
            try
            {
                IDomainMap domainMap = context.DomainMap;
                ModelToCodeTransformer modelToCodeTransformer = new ModelToCodeTransformer();
                CodeDomProvider provider = null;
                if (domainMap.CodeLanguage == CodeLanguage.CSharp)
                    provider = new CSharpCodeProvider();
                else if (domainMap.CodeLanguage == CodeLanguage.VB)
                    provider = new VBCodeProvider();
                else
                    throw new IAmOpenSourcePleaseImplementMeException();

                string code = modelToCodeTransformer.ToCode(domainMap, provider);

                CompilerResults cr = modelToCodeTransformer.ToCompilerResults(domainMap, provider);
                if (cr.Errors.Count > 0)
                {
                    throw new Exception("Domain model could not be compiled!");
                }
                else
                {
                    domain = cr.CompiledAssembly;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Domain model could not be compiled! The compiler gave the following exception: " + ex.ToString());
            }

            return domain;
        }

        private static Assembly GetDomain(IDomainMap domainMap)
        {
            IContext context = null;

            if (domainMap != null)
            {
                context = new Context(domainMap);
                return GenerateAssembly(context);
            }

            return null;
        }


        public static DataTable ClassToTable(Type objectType, string tableName, string tableNamespace)
        {
            DataTable table = new DataTable(tableName, tableNamespace);
            PropertyInfo[] properties = objectType.GetProperties();
            DataColumn column;
            foreach (PropertyInfo property in properties)
            {
                column = new DataColumn();
                column.ColumnName = (string)property.Name;
                if (property.PropertyType.IsGenericType && (property.PropertyType.GetGenericTypeDefinition() == typeof(System.Nullable<>)))
                    column.DataType = Nullable.GetUnderlyingType(property.PropertyType);
                else
                    column.DataType = property.PropertyType;
                table.Columns.Add(column);
            }
            return table;
        }

        public static DataTable ClassToTable(IDomainMap domainMap, Assembly assembly, string className)
        {
            Type t = assembly.GetType(domainMap.RootNamespace + "." + className);
            return ClassToTable(t, className, "");
        }

        private static string GetDataSet(List<DataTable> dataTableList, string dataSetName)
        {
            DataSet ds = new DataSet(dataSetName);
            foreach (DataTable dataTable in dataTableList)
                ds.Merge(dataTable);
            return ds.GetXmlSchema();
        }

        [PluginMethod(typeof(IClassMap), typeof(IPluginOutput), "Generate DataTable")]
        public static PluginOutput GetDataSet(IClassMap classMap)
        {
            Assembly domainAssembly = GetDomain(classMap.DomainMap);
            if (domainAssembly != null)
            {
                List<DataTable> dtList = new List<DataTable>();
                dtList.Add(ClassToTable(classMap.DomainMap, domainAssembly, classMap.Name));
                string res = GetDataSet(dtList, classMap.DomainMap.Name + "DataSet");
                res = res.Replace(domainAssembly.FullName, classMap.DomainMap.AssemblyName);
                return new PluginOutput(res, classMap.DomainMap.Name + "DataSet");
            }
            else
                throw new Exception("Could not compile domain model !");
        }

        [PluginMethod(typeof(IDomainMap), typeof(IPluginOutput), "Generate Dataset")]
        public static PluginOutput GetDataSet(IDomainMap domainMap)
        {
            Assembly domainAssembly = GetDomain(domainMap);
            if (domainAssembly != null)
            {
                List<DataTable> dtList = new List<DataTable>();
                foreach (ClassMap classMap in domainMap.ClassMaps)
                    dtList.Add(ClassToTable(domainMap, domainAssembly, classMap.Name));
                string res = GetDataSet(dtList, domainMap.Name + "DataSet");
                res = res.Replace(domainAssembly.FullName, domainMap.AssemblyName);
                return new PluginOutput(res, domainMap.Name + "DataSet");
            }
            else
                throw new Exception("Could not compile domain model !");
        }

    }
}
