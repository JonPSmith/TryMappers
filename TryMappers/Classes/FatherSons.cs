#region licence
// ======================================================================================
// TryMappers - compare AutoMapper and ExpressMapper for LINQ and develop flattener for ExpressMapper
// Filename: FatherSons.cs
// Date Created: 2016/02/28
// 
// Under the MIT License (MIT)
// 
// Written by Jon Smith : GitHub JonPSmith, www.thereformedprogrammer.net
// ======================================================================================
#endregion

using System;
using System.Collections.Generic;
using System.Linq;

namespace TryMappers.Classes
{
    public class FatherSons
    {
        public int MyInt { get; set; }
        public string MyString { get; set; }

        public ICollection<SonGrandson> Sons { get; set; }

        public static FatherSons CreateOne(int numSons = 5, Random rand = null)
        {
            rand = rand ?? new Random();
            return new FatherSons
            {
                MyInt = 1,
                MyString = "Father",
                Sons = SonGrandson.CreateMany(numSons, rand).ToList()
            };
        }

        public static IEnumerable<FatherSons> CreateMany(int num = 5, int numSons = 5)
        {
            var rand = new Random();
            for (int i = 0; i < num; i++)
            {
                yield return CreateOne(numSons, rand);
            }
        }
    }
}