using System;
using System.Collections.Generic;
using System.Text;
using System.Expressions;
using Puzzle.NPath.Framework.CodeDom;
using System.Collections;

namespace Puzzle.NPersist.Linq
{
    public class LinqToNPathConverter
    {
        public static string ConvertToString<T>(Expression<T> expression)
        {
            return ConvertExpression(expression);
        }

        public static string ConvertExpression<T>(Expression<T> expression)
        {
            return ConvertExpression(expression.Body);
           
        }

        public static string ConvertExpression(Expression expression)
        {
            if (expression is MethodCallExpression)
            {
                return ConvertMethodCallExpression((MethodCallExpression) expression);
            }
            if (expression is MemberExpression)
            {
                return ConvertMemberExpression((MemberExpression) expression);
            }
            if (expression is ParameterExpression)
            {
                return ConvertParameterExpression((ParameterExpression) expression);
            }
            if (expression is ConstantExpression)
            {
                return ConvertConstantExpression((ConstantExpression) expression);
            }
            if (expression is BinaryExpression)
            {
                return ConvertBinaryExpression((BinaryExpression) expression);
            }
            if (expression is UnaryExpression)
            {
                return ConvertUnaryExpression((UnaryExpression) expression);
            }


        


            throw new Exception("The method or operation is not implemented.");
        }

        private static string ConvertUnaryExpression(UnaryExpression expression)
        {
            string operand = ConvertExpression(expression.Operand);
            return operand;
        }

        private static string ConvertConstantExpression(ConstantExpression expression)
        {
            if (expression.Value is string)
                return string.Format ("\"{0}\"",expression.Value);

            if (expression.Value  is int)
                return string.Format ("{0}",expression.Value);

            throw new Exception("The method or operation is not implemented.");
        }

        private static string ConvertParameterExpression(ParameterExpression expression)
        {
            return "";
          //  return expression.Name;
        }

        private static string ConvertMemberExpression(MemberExpression expression)
        {
            string prefix = ConvertExpression (expression.Expression);
            string suffix = expression.Member.Name;

            if (suffix == "Count" && expression.Member.ReflectedType == typeof(ICollection))
            {
                suffix += "()";
            }

            if (prefix != "")
                return string.Format ("{0}.{1}",prefix,suffix);
            else
                return suffix;
        }

        private static string ConvertMethodCallExpression(MethodCallExpression expression)
        {
            if (expression.Method.Name == "op_Equality")
            {
                return ConvertToEqualityExpression(expression);
            }
            if (expression.Method.Name == "Like" && expression.Method.ReflectedType == typeof(StringExtensions))
            {
                return ConvertLikeExpression(expression);
            }
            if (expression.Method.Name == "Soundex" && expression.Method.ReflectedType == typeof(StringExtensions))
            {
                return ConvertSoundexExpression(expression);
            }

            throw new Exception("The method or operation is not implemented.");
        }

        private static string ConvertSoundexExpression(MethodCallExpression expression)
        {
            string left = ConvertExpression (expression.Parameters[0]);            

            return string.Format ("soundex ({0})",left);
        }

        private static string ConvertLikeExpression(MethodCallExpression expression)
        {
            string left = ConvertExpression (expression.Parameters[0]);
            string right = ConvertExpression (expression.Parameters[1]);

            return string.Format ("{0} like {1}",left,right);
        }

        private static string ConvertToEqualityExpression(MethodCallExpression expression)
        {
            string left = ConvertExpression (expression.Parameters[0]);
            string right = ConvertExpression (expression.Parameters[1]);


            return string.Format ("{0} = {1}",left,right);
        }


        private static string ConvertBinaryExpression(BinaryExpression expression)
        {
            string left = ConvertExpression(expression.Left);
            string right = ConvertExpression (expression.Right);

            if (expression.NodeType == ExpressionType.EQ)
            {
                return string.Format ("({0} = {1})",left,right);
            }

            if (expression.NodeType == ExpressionType.GT)
            {
                return string.Format ("({0} > {1})",left,right);
            }

            if (expression.NodeType == ExpressionType.LT)
            {
                return string.Format ("({0} < {1})",left,right);
            }

            if (expression.NodeType == ExpressionType.AndAlso)
            {
                return string.Format ("({0} and {1})",left,right);
            }

            if (expression.NodeType == ExpressionType.OrElse)
            {
                return string.Format ("({0} or {1})",left,right);
            }

             throw new Exception("The method or operation is not implemented.");
        }
    }
}
