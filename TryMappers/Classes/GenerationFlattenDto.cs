#region licence
// ======================================================================================
// TryMappers - compare AutoMapper and ExpressMapper for LINQ and develop flattener for ExpressMapper
// Filename: GenerationFlattenDto.cs
// Date Created: 2016/02/26
// 
// Under the MIT License (MIT)
// 
// Written by Jon Smith : GitHub JonPSmith, www.thereformedprogrammer.net
// ======================================================================================
#endregion

using System;

namespace TryMappers.Classes
{
    public class GenerationFlattenDto
    {
        public int MyInt { get; set; }
        public string MyString { get; set; }

        public int SonMyInt { get; set; }
        public string SonMyString { get; set; }

        public int SonGrandsonMyInt { get; set; }
        public string SonGrandsonMyString { get; set; }


        public static GenerationFlattenDto CreateOne(Random rand = null)
        {
            rand = rand ?? new Random();
            return new GenerationFlattenDto
            {
                MyInt = rand.Next(),
                MyString = "Father",
                SonMyInt = 2,
                SonMyString = "Son",
                SonGrandsonMyInt = 3,
                SonGrandsonMyString = "Grandson"
            };
        }
    }
}