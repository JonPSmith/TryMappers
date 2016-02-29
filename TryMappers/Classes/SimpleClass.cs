#region licence
// ======================================================================================
// TryMappers - compare AutoMapper and ExpressMapper for LINQ and develop flattener for ExpressMapper
// Filename: SimpleClass.cs
// Date Created: 2016/02/25
// 
// Under the MIT License (MIT)
// 
// Written by Jon Smith : GitHub JonPSmith, www.thereformedprogrammer.net
// ======================================================================================
#endregion

using System;
using System.Collections.Generic;

namespace TryMappers.Classes
{
    public class SimpleClass
    {
        public int MyInt { get; set; }
        public string MyString { get; set; }
        public DateTime MyDateTime { get; set; }

        public static SimpleClass CreateOne(Random rand = null)
        {
            rand = rand ?? new Random();
            return new SimpleClass
            {
                MyInt = 1,
                MyString = (rand.Next()).ToString(),
                MyDateTime = new DateTime(2016, 1, rand.Next(1, 31))
            };
        }

        public static IEnumerable<SimpleClass> CreateMany(int num = 5)
        {
            for (int i = 0; i < num; i++)
            {
                yield return CreateOne(new Random());
            }
        }
    }
}