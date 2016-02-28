#region licence
// ======================================================================================
// TryMappers - compare AutoMapper and ExpressMapper for LINQ and develop flattener for ExpressMapper
// Filename: Grandson.cs
// Date Created: 2016/02/26
// 
// Under the MIT License (MIT)
// 
// Written by Jon Smith : GitHub JonPSmith, www.thereformedprogrammer.net
// ======================================================================================
#endregion
namespace TryMappers.Classes
{
    public class Grandson
    {
        public int MyInt { get; set; }
        public string MyString { get; set; }

        public static Grandson CreateOne()
        {
            return new Grandson
            {
                MyInt = 3,
                MyString = "Grandson"
            };
        }
    }
}