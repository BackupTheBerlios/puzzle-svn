using System;
using System.IO;
using Microsoft.CSharp;
using Microsoft.VisualBasic;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Text;

using System.Collections;
using GenerationStudio.TemplateEngine;
using System.Collections.Generic;

namespace My.Scripting
{
    public class ScriptCompiler
    {
        public ScriptCompiler()
        {
        }

        public static string ScriptLanguageName_CSharp = "CSharp";
        public static string ScriptLanguageName_VisualBasic = "VisualBasic";


        #region Properties
        public static object ThreadRoot { get { return threadRoot; } }

        public Exception Error
        {
            get { return error; }
        }
        #endregion


        #region Errors and error handling
        static void Error_ExpectedScriptLanguageStatement()
        {
            throw new UnknownScriptLanguageException(
                "Expected language definition at beginning of" +
                " script (syntax: " + getLanguageStatementSyntax() + ")"
            );
        }

        static void Error_LanguageStatementSyntaxError()
        {
            throw new UnknownScriptLanguageException("Cannot interpret the language statement " + getLanguageStatementSyntax() + ")"
            );
        }

        static void Error_CompilationFailed(string message)
        {
            throw new ScriptCompilerException("Compilation failed:\n" + message);
        }

        static void Error_IScriptNotImplemented()
        {
            throw new ScriptNotImplementedException(
                "Interface " + typeof(ITemplate).FullName + " must be implemented!");
        }

        static string getLanguageStatementSyntax()
        {
            return "#language = {" + ScriptLanguageName_CSharp + " | " + ScriptLanguageName_VisualBasic + "}";
        }

        void addError(Exception value)
        {
            error = value;
        }
        #endregion


        public ITemplate Compile(string script)
        {
            
            try
            {
                var codeDomProvider = new CSharpCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", "v3.5" } });
               
                CompilerParameters cParams = new CompilerParameters();
                cParams.GenerateExecutable = false;
                cParams.GenerateInMemory = true;
                cParams.OutputAssembly = getTemporaryOutputAssemblyName();
                cParams.MainClass = "**not used**";
                cParams.IncludeDebugInformation = false;
                

                // allow all referenced assemblies to be used by the script...
                foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
                    cParams.ReferencedAssemblies.Add(asm.Location);

                

                CompilerResults results = codeDomProvider.CompileAssemblyFromSource(cParams, script);

                if (results.Errors.Count > 0)
                {
                    StringBuilder errors = new StringBuilder();
                    foreach (CompilerError err in results.Errors)
                    {
                        errors.Append(err.ToString() + "\n");
                    }
                    Error_CompilationFailed(errors.ToString());
                    return null; // keeps compiler happy :o|
                }
                else
                {
                    // remove temporary files...
                    if (File.Exists(cParams.OutputAssembly))
                        File.Delete(cParams.OutputAssembly);
                    // get the first class that implements the IScript interface...
                    ITemplate scriptObject = getScriptObject(results.CompiledAssembly);
                    if (scriptObject == null)
                        Error_IScriptNotImplemented();
                    return scriptObject;
                }
            }
            catch (ScriptCompilerException e)
            {
                addError(e);
                return null;
            }
        }

        #region Internal stuff
        Exception error;
        static long tmpOutputAssemblyID = 0;
        static object threadRoot = new object();

        /// <summary>
        /// Creates a thread safe temporary assembly name. 
        /// The method is primarily intended for on-the-run, in-memory, compilation.
        /// </summary>
        /// <param name="language">The script language</param>
        /// <returns>A temporary assembly name.</returns>
        static string getTemporaryOutputAssemblyName()
        {
            long result;
            lock (ThreadRoot)
            {
                result = ++tmpOutputAssemblyID;
            }
            return "temp_asm" + result.ToString();
        }

        /// <summary>
        /// Returns the first class in the assembly that implements the IScript interface. 
        /// </summary>
        /// <param name="asm">
        /// The assembly that's expected to contain the IScript implementor.
        /// </param>
        /// <returns>An instance of the first class found to implement the IScript interface. 
        /// If no such class exists a null value is returned instead</returns>
        ITemplate getScriptObject(Assembly asm)
        {
            Type[] types = asm.GetTypes();
            foreach (Type type in types)
            {
                if (type.IsClass && type.GetInterface("ITemplate") != null)
                    return (ITemplate)Activator.CreateInstance(type);
            }
            return null;
        }

        #endregion

    }

    public class ScriptCompilerException : System.Exception
    {
        public ScriptCompilerException(string message) : base(message) { }
    }

    public class UnknownScriptLanguageException : ScriptCompilerException
    {
        public UnknownScriptLanguageException(string message) : base(message) { }
    }

    public class ScriptNotImplementedException : ScriptCompilerException
    {
        public ScriptNotImplementedException(string message) : base(message) { }
    }
}
