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

using System.Collections.Generic;

namespace TryMappers.Classes
{
    public class FatherSon
    {
        public int MyInt { get; set; }
        public string MyString { get; set; }

        public SonGrandson Son { get; set; }

        public static FatherSon CreateOne()
        {
            return new FatherSon
            {
                MyInt = 1,
                MyString = "Father",
                Son = SonGrandson.CreateOne()
            };
        }

        public static IEnumerable<FatherSon> CreateMany(int num = 5)
        {
            for (int i = 0; i < num; i++)
            {
                yield return CreateOne();
            }
        }
    }
}