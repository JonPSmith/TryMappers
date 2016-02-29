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

using System;
using System.ComponentModel.DataAnnotations;

namespace TryMappers.Classes
{
    public class Grandson
    {
        [Key]
        public int Id { get; set; }

        public int MyInt { get; set; }
        public string MyString { get; set; }

        public static Grandson CreateOne(Random rand = null)
        {
            rand = rand ?? new Random();
            return new Grandson
            {
                MyInt = rand.Next(),
                MyString = "Grandson"
            };
        }
    }
}