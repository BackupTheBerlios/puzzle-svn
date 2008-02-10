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
            if (expression is NewExpression)
            {
                return ConvertNewExpression((NewExpression)expression);
            }
            
            throw new Exception("The method or operation is not implemented.");
        }

        private static string ConvertNewExpression(NewExpression expression)
        {
            if (expression.Constructor.DeclaringType == typeof(DateTime))
            {
                //HACK: make it typed - Roger
                string year = expression.Arguments[0].ToString().PadLeft (4,'0');
                string month = expression.Arguments[1].ToString().PadLeft(2, '0');
                string day = expression.Arguments[1].ToString().PadLeft(2, '0');
                return string.Format("#{0}-{1}-{2}#",year,month,day);
            }

            throw new NotImplementedException();
        }

        private static string ConvertUnaryExpression(UnaryExpression expression)
        {
            if (expression.NodeType == ExpressionType.Not)
            {
                string operand = ConvertExpression(expression.Operand);
                return string.Format ("not ({0})",operand);
            }
            else if (expression.NodeType == ExpressionType.Quote)
            {
                
                LambdaExpression lambda = expression.Operand as LambdaExpression;
                return ConvertExpression(lambda.Body);    
            }
            else
            {
                string operand = ConvertExpression(expression.Operand);
                return operand;
            }
        }

        private static string ConvertConstantExpression(ConstantExpression expression)
        {
            //TODO: escape string
            if (expression.Value is string)
                return string.Format ("\"{0}\"",expression.Value);

            if (expression.Value  is int)
                return string.Format(NumberFormatInfo.InvariantInfo, "{0}", expression.Value);

            if (expression.Value is double)
                return string.Format(NumberFormatInfo.InvariantInfo, "{0}", expression.Value);

            throw new Exception("The method or operation is not implemented.");
        }

        private static string ConvertParameterExpression(ParameterExpression expression)
        {
            return "";
          //  return expression.Name;
        }

        private static string ConvertMemberExpression(MemberExpression expression)
        {
            string suffix = expression.Member.Name;

            if (expression.Expression is UnaryExpression && suffix == "Count")
            {
                return ConvertSubquery((UnaryExpression)expression.Expression);
            }
            else
            {

                string prefix = ConvertExpression (expression.Expression);


                if (suffix == "Count" && typeof(IList).IsAssignableFrom (expression.Member.ReflectedType))
                {
                    suffix += "()";
                }
                else if (suffix == "Count" && expression is MemberExpression)
                {
                    return string.Format("(select count(*) from {0})", prefix);
                }

                if (prefix != "")
                    return string.Format ("{0}.{1}",prefix,suffix);
                else
                    return suffix;
            }
        }



        private static string ConvertSubquery(UnaryExpression expression)
        {
            if (expression.Operand is MethodCallExpression && ((MethodCallExpression)expression.Operand).Method.Name == "Where")
            {
                MethodCallExpression methodCall = (MethodCallExpression)expression.Operand;
                
                
                
                string propPath = ConvertExpression (methodCall.Arguments[0]);

                LambdaExpression exp = (LambdaExpression)methodCall.Arguments[1];
                
                string whereClause = ConvertExpression(exp.Body);
                return string.Format ("(select count(*) from {0} where {1})",propPath,whereClause);
            }

            throw new Exception("The method or operation is not implemented.");        
        }

        private static string ConvertMethodCallExpression(MethodCallExpression expression)
        {
            if (expression.Method.Name == "Where")
            {
                return ConvertSubWhereExpression(expression);
            }
            if (expression.Method.Name == "op_Equality")
            {
                return ConvertToEqualityExpression(expression);
            }
            if (expression.Method.Name == "op_Inequality")
            {
                return ConvertToInequalityExpression(expression);
            }
            if (expression.Method.Name == "Like" && expression.Method.ReflectedType == typeof(StringExtensions))
            {
                return ConvertLikeExpression(expression);
            }
            if (expression.Method.Name == "Soundex" && expression.Method.ReflectedType == typeof(StringExtensions))
            {
                return ConvertSoundexExpression(expression);
            }            

            throw new Exception(string.Format ("The method or operation is not implemented. : {0}",expression));
        }

        private static string ConvertSubWhereExpression(MethodCallExpression expression)
        {            
            string from = ConvertExpression(expression.Arguments[0]);
            string predicate = ConvertUnaryExpression((UnaryExpression)expression.Arguments[1]);

            return string.Format("{0} where {1}", from, predicate);
        }


        private static string ConvertToInequalityExpression(MethodCallExpression expression)
        {
            string left = ConvertExpression(expression.Arguments[0]);
            string right = ConvertExpression(expression.Arguments[1]);


            return string.Format ("{0} != {1}",left,right);
        }

        private static string ConvertSoundexExpression(MethodCallExpression expression)
        {
            string left = ConvertExpression(expression.Arguments[0]);            

            return string.Format ("soundex ({0})",left);
        }

        private static string ConvertLikeExpression(MethodCallExpression expression)
        {
            string left = ConvertExpression(expression.Arguments[0]);
            string right = ConvertExpression(expression.Arguments[1]);

            return string.Format ("{0} like {1}",left,right);
        }

        private static string ConvertToEqualityExpression(MethodCallExpression expression)
        {
            string left = ConvertExpression(expression.Arguments[0]);
            string right = ConvertExpression(expression.Arguments[1]);


            return string.Format ("{0} = {1}",left,right);
        }


        private static string ConvertBinaryExpression(BinaryExpression expression)
        {
            string left = ConvertExpression(expression.Left);
            string right = ConvertExpression (expression.Right);

            if (expression.NodeType == ExpressionType.Equal)
            {
                return string.Format ("({0} = {1})",left,right);
            }

            if (expression.NodeType == ExpressionType.NotEqual)
            {
                return string.Format("({0} != {1})", left, right);
            }

            if (expression.NodeType == ExpressionType.GreaterThan)
            {
                return string.Format ("({0} > {1})",left,right);
            }

            if (expression.NodeType == ExpressionType.LessThan)
            {
                return string.Format ("({0} < {1})",left,right);
            }

            if (expression.NodeType == ExpressionType.GreaterThanOrEqual)
            {
                return string.Format ("({0} >= {1})",left,right);
            }

            if (expression.NodeType == ExpressionType.LessThanOrEqual)
            {
                return string.Format ("({0} <= {1})",left,right);
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

        public static void CreateLoadspan<T>(NewExpression expression,LinqQuery<T> query)
        {
            NewArrayExpression newArray = (NewArrayExpression)expression.Arguments[0];

            query.SelectClause = "select ";
            int i = 0;
            foreach (Expression arg in newArray.Expressions)
            {
                string path = ConvertExpression(arg);
                query.SelectClause += path;

                i++;
                if (i<newArray.Expressions.Count)
                    query.SelectClause += ",";
            }
        }

        public static void CreateLoadspan<T>(MemberInitExpression expression,LinqQuery<T> query)
        {

            query.SelectClause = "select ";
            int i = 0;
            foreach (MemberAssignment binding in expression.Bindings)
            {
                string path = ConvertExpression(binding.Expression);
                query.SelectClause += path;

                i++;
                if (i<expression.Bindings.Count)
                    query.SelectClause += ",";
            }
        }
    }
}
