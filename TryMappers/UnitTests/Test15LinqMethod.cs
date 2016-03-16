﻿#region licence
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

using System;
using System.Linq.Expressions;
using System.Reflection;
using NUnit.Framework;
using TryMappers.Classes;
using TryMappers.Flatteners;
using TryMappers.Flatteners.Internal;
using TryMappers.Helpers;

namespace TryMappers.UnitTests
{
    public class Test15LinqMethod
    {
        //"Any", "Count", "LongCount",
        //"First", "FirstOrDefault", "Last", "LastOrDefault", "Single", "SingleOrDefault"

        [TestCase("Any")]
        [TestCase("Count")]
        [TestCase("LongCount")]
        [TestCase("First")]
        [TestCase("FirstOrDefault")]
        [TestCase("Last")]
        [TestCase("LastOrDefault")]
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

        [TestCase("Single")]
        [TestCase("SingleOrDefault")]
        public void Test02CollectionMethodsSingleOk(string method)
        {
            //SETUP
            var sourceClass = FatherSons.CreateOne();
            var scrMember = sourceClass.GetType().GetProperty("Sons", BindingFlags.Public | BindingFlags.Instance);
            var dto = new LinqSingleMethodsDto();
            var destMember = dto.GetType().GetProperty("Sons" + method, BindingFlags.Public | BindingFlags.Instance);
            var propExp = Expression.Property(Expression.Parameter(sourceClass.GetType(), "src"), scrMember);

            //ATTEMPT
            var lm = LinqMethod.EnumerableEndMatchsWithLinqMethod(method);
            var mce = lm.AsMethodCallExpression(propExp, scrMember, destMember);

            //VERIFY
            mce.Method.Name.ShouldEqual(method);
        }

        // "Average", "Max", "Min", "Sum"

        [TestCase("Ints", "Average")]
        [TestCase("Ints", "Max")]
        [TestCase("Ints", "Min")]
        [TestCase("Ints", "Sum")]
        [TestCase("Doubles", "Average")]
        [TestCase("Doubles", "Max")]
        [TestCase("Doubles", "Min")]
        [TestCase("Doubles", "Sum")]
        public void Test03AggregateMethodsOk(string srcName, string method)
        {
            //SETUP
            var sourceClass = new LinqAggregates();
            var scrMember = sourceClass.GetType().GetProperty(srcName, BindingFlags.Public | BindingFlags.Instance);
            var dto = new LinqAggregateMethodsDto();
            var destMember = dto.GetType().GetProperty(srcName + method, BindingFlags.Public | BindingFlags.Instance);
            var propExp = Expression.Property(Expression.Parameter(sourceClass.GetType(), "src"), scrMember);

            //ATTEMPT
            var lm = LinqMethod.EnumerableEndMatchsWithLinqMethod(method);
            var mce = lm.AsMethodCallExpression(propExp, scrMember, destMember);

            //VERIFY
            mce.Method.Name.ShouldEqual(method);
        }
    }
}