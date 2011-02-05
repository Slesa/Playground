using System;
using System.Collections.ObjectModel;
using System.Data.Services.Client;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;

namespace MediaOwl.Services
{

    /// <summary>
    /// This class extends the OData-Queries;
    /// Taken from <see cref="http://blogs.msdn.com/b/stuartleeks/archive/2008/09/15/dataservicequery-t-expand.aspx"/>
    /// </summary>
    public static class DataServiceQueryExtensions
    {
        public static DataServiceQuery<TSource> Expand<TSource, TPropType>(this DataServiceQuery<TSource> source, Expression<Func<TSource, TPropType>> propertySelector)
        {
            string expandString = BuildString(propertySelector);
            return source.Expand(expandString);
        }
        private static string BuildString(Expression propertySelector)
        {
            switch (propertySelector.NodeType)
            {
                case ExpressionType.Lambda:
                    var lambdaExpression = (LambdaExpression)propertySelector;
                    return BuildString(lambdaExpression.Body);

                case ExpressionType.Quote:
                    var unaryExpression = (UnaryExpression)propertySelector;
                    return BuildString(unaryExpression.Operand);

                case ExpressionType.MemberAccess:
                    MemberInfo propertyInfo = ((MemberExpression)propertySelector).Member;
                    return propertyInfo.Name;

                case ExpressionType.Call:
                    var methodCallExpression = (MethodCallExpression)propertySelector;
                    if (IsSubExpand(methodCallExpression.Method)) // check that it's a SubExpand call
                    {
                        // argument 0 is the expression to which the SubExpand is applied (this could be member access or another SubExpand)
                        // argument 1 is the expression to apply to get the expanded property
                        // Pass both to BuildString to get the full expression
                        return BuildString(methodCallExpression.Arguments[0]) + "/" +
                               BuildString(methodCallExpression.Arguments[1]);
                    }
                    // else drop out and throw
                    break;
            }
            throw new InvalidOperationException("Expression must be a member expression or an SubExpand call: " + propertySelector);

        }

        private static readonly MethodInfo[] SubExpandMethods;
        static DataServiceQueryExtensions()
        {
            Type type = typeof(DataServiceQueryExtensions);
            SubExpandMethods = type.GetMethods().Where(mi => mi.Name == "SubExpand").ToArray();
        }
        private static bool IsSubExpand(MethodInfo methodInfo)
        {
            if (methodInfo.IsGenericMethod)
            {
                if (!methodInfo.IsGenericMethodDefinition)
                {
                    methodInfo = methodInfo.GetGenericMethodDefinition();
                }
            }
            return SubExpandMethods.Contains(methodInfo);
        }

        public static TPropType SubExpand<TSource, TPropType>(this Collection<TSource> source, Expression<Func<TSource, TPropType>> propertySelector)
            where TSource : class
            where TPropType : class
        {
            throw new InvalidOperationException("This method is only intended for use with DataServiceQueryExtensions.Expand to generate expressions trees"); // no actually using this - just want the expression!
        }
        public static TPropType SubExpand<TSource, TPropType>(this TSource source, Expression<Func<TSource, TPropType>> propertySelector)
            where TSource : class
            where TPropType : class
        {
            throw new InvalidOperationException("This method is only intended for use with DataServiceQueryExtensions.Expand to generate expressions trees"); // no actually using this - just want the expression!
        }
    }
}