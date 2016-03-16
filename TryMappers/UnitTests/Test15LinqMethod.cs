#region licence
// ======================================================================================
// TryMappers - compare AutoMapper and ExpressMapper for LINQ and develop flattener for ExpressMapper
// Filename: Test15LinqMethod.cs
// Date Created: 2016/03/16
// 
// Under the MIT License (MIT)
// 
// Written by Jon Smith : GitHub JonPSmith, www.thereformedprogrammer.net
// ======================================================================================
#endregion

using System.Linq.Expressions;
using System.Reflection;
using NUnit.Framework;
using TryMappers.Classes;
using TryMappers.Flatteners.Internal;
using TryMappers.Helpers;

namespace TryMappers.UnitTests
{
    public class Test15LinqMethod
    {
        //"Any", "Count", "LongCount", "FirstOrDefault"

        [TestCase("Any")]
        [TestCase("Count")]
        [TestCase("LongCount")]
        [TestCase("FirstOrDefault")]
        public void Test01CollectionMethodsOk(string method)
        {
            //SETUP
            var sourceClass = FatherSons.CreateOne();
            var scrMember = sourceClass.GetType().GetProperty("Sons", BindingFlags.Public | BindingFlags.Instance);
            var dto = new LinqCollectionMethodsDto();
            var destMember = dto.GetType().GetProperty("Sons" + method, BindingFlags.Public | BindingFlags.Instance);
            var propExp = Expression.Property(Expression.Parameter(sourceClass.GetType(), "src"), scrMember);

            //ATTEMPT
            var lm = LinqMethod.EnumerableEndMatchsWithLinqMethod(method);
            var mce = lm.AsMethodCallExpression(propExp, scrMember, destMember);

            //VERIFY
            mce.Method.Name.ShouldEqual(method);
        }

    }
}