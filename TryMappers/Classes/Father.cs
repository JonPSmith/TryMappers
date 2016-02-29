#region licence
// ======================================================================================
// TryMappers - compare AutoMapper and ExpressMapper for LINQ and develop flattener for ExpressMapper
// Filename: FatherSon.cs
// Date Created: 2016/02/26
// 
// Under the MIT License (MIT)
// 
// Written by Jon Smith : GitHub JonPSmith, www.thereformedprogrammer.net
// ======================================================================================
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TryMappers.Classes
{
    public class Father
    {
        [Key]
        public int Id { get; set; }

        public int MyInt { get; set; }
        public string MyString { get; set; }

        public Son Son { get; set; }


        public static Father CreateOne(Random rand = null )
        {
            rand = rand ?? new Random();
            return new Father
            {
                MyInt = rand.Next(),
                MyString = "Father",
                Son = Son.CreateOne()
            };
        }

        public static IEnumerable<Father> CreateMany(int num = 5)
        {
            var rand = new Random();
            for (int i = 0; i < num; i++)
            {
                yield return CreateOne(rand);
            }
        }
    }
}