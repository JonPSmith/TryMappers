#region licence
// ======================================================================================
// TryMappers - compare AutoMapper and ExpressMapper for LINQ and develop flattener for ExpressMapper
// Filename: LinqMethod.cs
// Date Created: 2016/02/27
// 
// Under the MIT License (MIT)
// 
// Written by Jon Smith : GitHub JonPSmith, www.thereformedprogrammer.net
// ======================================================================================
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using TryMappers.Classes;

namespace TryMappers.Flatteners.Internal
{
    internal class LinqMethod
    {
        private static readonly Dictionary<string, LinqMethod> EnumerableMethodLookup = new Dictionary<string, LinqMethod> 
        {
            { "Count", new LinqMethod("Count") }
        };

        private LinqMethod(string methodName)
        {
            MethodName = methodName;
        }

        /// <summary>
        /// Method name for debug (rest not implmented yet)
        /// </summary>
        public string MethodName { get; private set; }

        /// <summary>
        /// This can be called on enumerable properly to see if the ending is a valid Linq method
        /// </summary>
        /// <param name="destProp"></param>
        /// <param name="matchStart"></param>
        /// <returns></returns>
        public static LinqMethod EnumerableEndMatchsWithLinqMethod(PropertyInfo destProp, string matchStart)
        {
            var endOfName = destProp.Name.Substring(matchStart.Length);
            return EnumerableMethodLookup.ContainsKey(endOfName) ? EnumerableMethodLookup[endOfName] : null;
        }

        public override string ToString()
        {
            return $".{MethodName}()";
        }

        public MethodCallExpression AsMethodCallExpression(Expression propertyExpression, PropertyInfo propertyToActOn)
        {
            var ienumerableType = propertyToActOn.PropertyType.GetGenericArguments().Single();

            var method = typeof(Enumerable).GetMethods()
                                           .Where(m => m.Name == MethodName)
                                           .Single(m => m.GetParameters().Length == 1)
                                           .MakeGenericMethod(ienumerableType);

            return Expression.Call(method, propertyExpression);
        }
    }
}