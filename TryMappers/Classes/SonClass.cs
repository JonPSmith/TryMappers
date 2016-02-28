#region licence
// ======================================================================================
// Mvc5UsingBower - An example+library to allow an MVC project to use Bower and Grunt
// Filename: ChildClass.cs
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
    public class SonClass
    {
        public int MyInt { get; set; }
        public string MyString { get; set; }

        public GrandsonClass Grandson { get; set; }

        public static SonClass CreateOne()
        {
            return new SonClass
            {
                MyInt = 2,
                MyString = "Son",
                Grandson = GrandsonClass.CreateOne()
            };
        }
    }
}