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

using System.Collections.Generic;

namespace TryMappers.Classes
{
    public class SonGrandson
    {
        public int MyInt { get; set; }
        public string MyString { get; set; }

        public Grandson Grandson { get; set; }

        public static SonGrandson CreateOne()
        {
            return new SonGrandson
            {
                MyInt = 2,
                MyString = "Son",
                Grandson = Grandson.CreateOne()
            };
        }

        public static IEnumerable<SonGrandson> CreateMany(int num = 5)
        {
            for (int i = 0; i < num; i++)
            {
                yield return CreateOne();
            }
        }
    }
}