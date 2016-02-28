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

using System.Collections.Generic;
using System.Reflection;

namespace TryMappers.Flatteners.Internal
{
    internal class LinqMethod
    {
        private static readonly Dictionary<string, LinqMethod> MethodLookup = new Dictionary<string, LinqMethod> 
        {
            { "Count", new LinqMethod("Count") },
            { "Sum", new LinqMethod("Sum") },
        };


        private LinqMethod(string methodName)
        {
            MethodName = methodName;
        }

        /// <summary>
        /// Method name for debug
        /// </summary>
        public string MethodName { get; private set; }

        public static LinqMethod MatchsWithLinqMethod(PropertyInfo destProp, string matchStart)
        {
            var endOfName = destProp.Name.Substring(matchStart.Length);
            return MethodLookup.ContainsKey(endOfName) ? MethodLookup[endOfName] : null;
        }
    }
}