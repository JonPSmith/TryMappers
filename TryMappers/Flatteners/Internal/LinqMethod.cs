#region licence
// ======================================================================================
// Mvc5UsingBower - An example+library to allow an MVC project to use Bower and Grunt
// Filename: LinqMethod.cs
// Date Created: 2016/02/27
// 
// Under the MIT License (MIT)
// 
// Written by Jon Smith : GitHub JonPSmith, www.thereformedprogrammer.net
// ======================================================================================
#endregion

using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace TryMappers.Flatteners.Internal
{
    internal class LinqMethod
    {
        private static readonly string[] MethodNames = new[] {"Count", "Sum"};

        public string MethodName { get; private set; }


        private LinqMethod(string methodName)
        {
            MethodName = methodName;
        }

        public static LinqMethod MatchsWithLinqMethod(PropertyInfo destProp, string matchStart)
        {
            var endOfName = destProp.Name.Substring(matchStart.Length);
            return MethodNames.Contains(endOfName) ? new LinqMethod(endOfName) : null;
        }


    }
}