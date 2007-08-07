using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Puzzle.NPersist.Framework;
using System.Reflection;
using Puzzle.NPersist.Framework.Mapping;
using Puzzle.NPersist.Framework.Mapping.Transformation;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using Puzzle.NPersist.Framework.Mapping.Serialization;

namespace Puzzle.FastForward.Web
{
    public partial class _Default : System.Web.UI.Page
    {
        Assembly domain = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            using (IContext context = GetContext())
            {
                Type employeeType = domain.GetType("TestDomain.Employee");
                IList employees = context.GetObjects(employeeType);
                testRepeater.DataSource = employees;
                testRepeater.DataBind();
            }
        }

        private IContext GetContext()
        {
            //Assembly asm = this.GetType().Assembly;
            //string path = "Puzzle.FastForward.Web.Test.npersist";
            string path = @"C:\Berlioz\Puzzle\FastForward\Puzzle.FastForward.Web\Test.npersist";
            IMapSerializer serialier = new DefaultMapSerializer();
            IDomainMap domainMap = DomainMap.Load(path, serialier, false, false);
            IContext context = new Context(domainMap);
            GenerateAssembly(context);
            return context;
        }

        private bool GenerateAssembly(IContext context)
        {
            try
            {
                IDomainMap domainMap = context.DomainMap;
                ModelToCodeTransformer modelToCodeTransformer = new ModelToCodeTransformer();
                CodeDomProvider provider = null;
                //if (domainMap.CodeLanguage == CodeLanguage.CSharp)
                //{
                    provider = new CSharpCodeProvider();
                //    config.SetupCSharp();
                //}
                //else if (domainMap.CodeLanguage == CodeLanguage.VB)
                //{
                //    provider = new VBCodeProvider();
                //    config.SetupVBNet();
                //}

                string code = modelToCodeTransformer.ToCode(domainMap, provider);

                CompilerResults cr = modelToCodeTransformer.ToCompilerResults(domainMap, provider);
                if (cr.Errors.Count > 0)
                {
                }
                else
                {
                    domain = cr.CompiledAssembly;
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

    }
}
