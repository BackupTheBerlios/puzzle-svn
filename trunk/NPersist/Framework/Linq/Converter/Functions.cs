// *
// * Copyright (C) 2005 Roger Alsing : http://www.puzzleframework.com
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
using System.Linq;
using Puzzle.NPath.Framework.CodeDom;
using System.Collections;
using Puzzle.NPersist.Framework.Linq.Strings;
using System.Linq.Expressions;
using System.Globalization;

namespace Puzzle.NPersist.Framework.Linq
{
    public partial class LinqToNPathConverter
    {
        private static string ConvertSoundexExpression(MethodCallExpression expression)
        {
            string left = ConvertExpression(expression.Arguments[0]);

            return string.Format("soundex ({0})", left);
        }

        private static string ConvertLikeExpression(MethodCallExpression expression)
        {
            string left = ConvertExpression(expression.Arguments[0]);
            string right = ConvertExpression(expression.Arguments[1]);

            return string.Format("{0} like {1}", left, right);
        }
    }
}