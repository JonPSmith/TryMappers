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
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace TryMappers.Flatteners.Internal
{
    internal class LinqMethod
    {
        //Any name starting with ~ means the return type should not be checked because it is the same as source
        private static readonly string[] ListOfSupportedLinqMethods = new[]
        {
            "Any", "Count", "LongCount",
            "~First", "~FirstOrDefault", "~Last", "~LastOrDefault", "~Single", "~SingleOrDefault",
            //These are not applicable to Entity Framework as EF doesn't allow a property only holding a value
            "Average", "Max", "Min", "Sum",   
        };

        private static readonly Dictionary<string, LinqMethod> EnumerableMethodLookup;

        static LinqMethod()
        {
            EnumerableMethodLookup = 
                (from givenName in ListOfSupportedLinqMethods
                let checkReturnType = givenName[0] != '~'
                let name = checkReturnType ? givenName : givenName.Substring(1)
                select new { Key = name, Value = new LinqMethod(name, checkReturnType) })
                .ToDictionary(key => key.Key, val => val.Value);
        }

        /// <summary>
        /// Method name 
        /// </summary>
        private readonly string _methodName;

        /// <summary>
        /// If true then the return type should be checked to get the right version of the method
        /// </summary>
        private readonly bool _checkReturnType ;

        private LinqMethod(string methodName, bool checkReturnType)
        {
            _methodName = methodName;
            _checkReturnType = checkReturnType;
        }

        /// <summary>
        /// This can be called on enumerable properly to see if the ending is a valid Linq method
        /// </summary>
        /// <param name="endOfName"></param>
        /// <returns></returns>
        public static LinqMethod EnumerableEndMatchsWithLinqMethod(string endOfName)
        {
            return EnumerableMethodLookup.ContainsKey(endOfName) ? EnumerableMethodLookup[endOfName] : null;
        }

        public override string ToString()
        {
            return $".{_methodName}()";
        }

        public MethodCallExpression AsMethodCallExpression(Expression propertyExpression, PropertyInfo propertyToActOn, PropertyInfo destProperty)
        {
            var ienumerableType = propertyToActOn.PropertyType.GetGenericArguments().Single();

            var foundMethodsWithParams = (from eachMethod in typeof (Enumerable).GetMethods()
                .Where(m => m.Name == _methodName
                            && (!_checkReturnType || m.ReturnType == destProperty.PropertyType))
                let ctorParams = eachMethod.GetParameters()
                where ctorParams.Length == 1
                select new {methodInfo = eachMethod, ctorParams}).ToArray();


            if (!foundMethodsWithParams.Any())
                throw new InvalidOperationException(
                    $"We could not find the Method {_methodName} which matched the {destProperty.Name} which has the type {destProperty.PropertyType}.");

            if (foundMethodsWithParams.Length > 1)
            {
                if (!foundMethodsWithParams.First().methodInfo.IsGenericMethod)
                {
                    //There can be multiple of a non generic method, so we search for the one with the right input type
                    foundMethodsWithParams = foundMethodsWithParams
                        .Where(x => x.ctorParams.First().ParameterType.IsGenericType
                            && x.ctorParams.Single().ParameterType.GetGenericArguments().Single() == ienumerableType).ToArray();
                    if (foundMethodsWithParams.Length != 1)
                        throw new InvalidOperationException(
                            $"We could not find the Method {_methodName} which matched the {destProperty.Name} type and has the type {destProperty.PropertyType}.");
                }
                else
                {
                    throw new InvalidOperationException(
                        $"Ambiguous {_methodName} for {destProperty.Name}. This is a system error so report to author.");
                }
            }

            var method = foundMethodsWithParams.Single().methodInfo.IsGenericMethod 
                ? foundMethodsWithParams.Single().methodInfo.MakeGenericMethod(ienumerableType) 
                : foundMethodsWithParams.Single().methodInfo;

            return Expression.Call(method, propertyExpression);
        }
    }
}