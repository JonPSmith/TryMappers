#region licence
// ======================================================================================
// Mvc5UsingBower - An example+library to allow an MVC project to use Bower and Grunt
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
    public class FatherClass
    {
        public int MyInt { get; set; }
        public string MyString { get; set; }

        public SonClass Son { get; set; }

        public static FatherClass CreateOne()
        {
            return new FatherClass
            {
                MyInt = 1,
                MyString = "Father",
                Son = SonClass.CreateOne()
            };
        }

        public static IEnumerable<FatherClass> CreateMany(int num = 5)
        {
            for (int i = 0; i < num; i++)
            {
                yield return CreateOne();
            }
        }
    }
}