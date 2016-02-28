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
namespace TryMappers.Classes
{
    public class GrandsonClass
    {
        public int MyInt { get; set; }
        public string MyString { get; set; }

        public static GrandsonClass CreateOne()
        {
            return new GrandsonClass
            {
                MyInt = 3,
                MyString = "Grandson"
            };
        }
    }
}