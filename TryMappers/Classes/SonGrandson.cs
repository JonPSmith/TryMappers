#region licence
// ======================================================================================
// TryMappers - compare AutoMapper and ExpressMapper for LINQ and develop flattener for ExpressMapper
// Filename: SonGrandson.cs
// Date Created: 2016/02/26
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
    public class SonGrandson
    {
        public int MyInt { get; set; }
        public string MyString { get; set; }

        public Grandson Grandson { get; set; }

        public static SonGrandson CreateOne(Random rand = null)
        {
            rand = rand ?? new Random();
            return new SonGrandson
            {
                MyInt = rand.Next(),
                MyString = "Son",
                Grandson = Grandson.CreateOne(rand)
            };
        }

        public static IEnumerable<SonGrandson> CreateMany(int num = 5, Random rand = null)
        {
            rand = rand ?? new Random();
            for (int i = 0; i < num; i++)
            {
                yield return CreateOne(rand);
            }
        }
    }
}