﻿// *
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
using System.Reflection;

namespace Puzzle.NPersist.Framework.Linq
{
    public partial class LinqToNPathConverter
    {
        private string ConvertAnyExpression(MethodCallExpression expression)
        {
            string fromWhere = ConvertExpression(expression.Arguments[0]);

            if (expression.Arguments.Count == 1)
                return string.Format("(select count(*) from {0}) > 0", fromWhere);
            else
            {
                LambdaExpression lambda = expression.Arguments[1] as LambdaExpression;
                string anyCond = ConvertExpression(lambda.Body);
                return string.Format("(select count(*) from {0} and {1}) > 0", fromWhere,anyCond);
            }
        }

        private string ConvertAllExpression(MethodCallExpression expression)
        {
            string fromWhere = ConvertExpression(expression.Arguments[0]);

            LambdaExpression lambda = expression.Arguments[1] as LambdaExpression;
            string anyCond = ConvertExpression(lambda.Body);
            return string.Format("(select count(*) from {0}) = (select count(*) from {0} and {1})", fromWhere, anyCond);
        }

        private string ConvertContainsExpression(MethodCallExpression expression)
        {
            
            string from = ConvertExpression(expression.Object);
            string item = ConvertExpression(expression.Arguments[0]);
            ParameterInfo param = expression.Method.GetParameters().First();
            string typeName = param.ParameterType.Name;


            return string.Format("(select count(*) from {0} where {1} = {2}) > 0", from,typeName,item);
        }

        private string ConvertSoundexExpression(MethodCallExpression expression)
        {
            string left = ConvertExpression(expression.Arguments[0]);

            return string.Format("soundex ({0})", left);
        }

        private string ConvertLikeExpression(MethodCallExpression expression)
        {
            string left = ConvertExpression(expression.Arguments[0]);
            string right = ConvertExpression(expression.Arguments[1]);

            return string.Format("{0} like {1}", left, right);
        }
    }
}