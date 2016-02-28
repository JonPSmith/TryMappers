#region licence
// ======================================================================================
// TryMappers - compare AutoMapper and ExpressMapper for LINQ and develop flattener for ExpressMapper
// Filename: SimpleClassDto.cs
// Date Created: 2016/02/25
// 
// Under the MIT License (MIT)
// 
// Written by Jon Smith : GitHub JonPSmith, www.thereformedprogrammer.net
// ======================================================================================
#endregion

using System;

namespace TryMappers.Classes
{
    public class SimpleClassDto
    {
        public int MyInt { get; set; }
        public string MyString { get; set; }
        public DateTime MyDateTime { get; set; }
    }
}