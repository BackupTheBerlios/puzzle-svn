// *
// * Copyright (C) 2005 Roger Alsing : http://www.puzzleframework.com
// *
// * This library is free software; you can redistribute it and/or modify it
// * under the terms of the GNU Lesser General Public License 2.1 or later, as
// * published by the Free Software Foundation. See the included license.txt
// * or http://www.gnu.org/copyleft/lesser.html for details.
// *
// *

using System.Collections.Generic;
using System.IO;

namespace Puzzle.SourceCode
{
    /// <summary>
    /// SyntaxDefinition list class
    /// </summary>
    public class SyntaxDefinitionList
    {
        private readonly List<SyntaxDefinition> languages;


        /// <summary>
        /// 
        /// </summary>
        public SyntaxDefinitionList()
        {
            languages = new List<SyntaxDefinition>();

            string[] files = Directory.GetFiles(".", "*.syn");            
            foreach (string file in files)
            {
                var loader = new SyntaxDefinitionLoader();
                SyntaxDefinition syntax = loader.Load(file);
                languages.Add(syntax);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public SyntaxDefinition GetLanguageFromFile(string path)
        {
            string extension = Path.GetExtension(path);
            foreach (SyntaxDefinition lang in languages)
            {
                foreach (FileType ft in lang.FileTypes)
                {
                    if (extension.ToLowerInvariant() == ft.Extension.ToLowerInvariant())
                        return lang;
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<SyntaxDefinition> GetSyntaxDefinitions()
        {
            return languages;
        }
    }
}