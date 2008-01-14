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

namespace Puzzle.ObjectMapper.Plugins.DataTableGenerator
{
    [PluginClass("Puzzle", "DataSet Generator")]
	public class PluginOutput : IPluginOutput 
	{ 
		private string _content; 
		private string _filename; 

		public string Content 
		{ 
			get { return _content; } 
			set { _content = value; } 
		} 
		public string Filename 
		{ 
			get { return _filename; } 
			set { _filename = value; } 
		} 
		public string Extension 
		{ 
			get { return "xsd"; } 
		} 
		public string TypeDescription 
		{ 
			get { return "Dataset"; } 
		} 

		public int DocumentType 
		{ 
			/* Public Enum MainDocumentType 
			Text = 0 
			CodeVbNet = 1 
			CodeCSharp = 2 
			CodeDelphi = 3 
			DTD = 3 
			XML = 4 
			NPersist = 5 
			*/
			get { return 4; } 
		} 

		public PluginOutput(string content, string filename) 
		{ 
			this.Content = content; 
			this.Filename = filename; 
		} 

	}
}
