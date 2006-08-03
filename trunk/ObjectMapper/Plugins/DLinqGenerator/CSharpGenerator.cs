using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using Puzzle.NPersist.Framework.Mapping;
using Puzzle.ObjectMapper.Plugin;

namespace Puzzle.ObjectMapper.Plugins.DLinqGenerator
{
	/// <summary>
	/// Summary description for CSharpGenerator.
	/// </summary>
	[PluginClass("Puzzle")]
	public class CSharpGenerator
	{
		public CSharpGenerator()
		{
		}

		[PluginMethod(typeof(IClassMap), typeof(String), "DLinq Class")]
		public static string GetDLinqClassCsharp(IClassMap classMap)
		{
			CodeCompileUnit compileunit = CodeDomGenerator.GetDLinqClassCompileUnit(classMap);

			CodeDomProvider provider = new CSharpCodeProvider();

			string code = CodeDomGenerator.ToCode(compileunit, provider);

			return code;
		}
	
	
	}
}
