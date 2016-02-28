#region licence
// ======================================================================================
// TryMappers - compare AutoMapper and ExpressMapper for LINQ and develop flattener for ExpressMapper
// Filename: FatherSonsCountDto.cs
// Date Created: 2016/02/28
// 
// Under the MIT License (MIT)
// 
// Written by Jon Smith : GitHub JonPSmith, www.thereformedprogrammer.net
// ======================================================================================
#endregion
namespace TryMappers.Classes
{
    public class FatherSonsCountDto
    {
        public int MyInt { get; set; }
        public string MyString { get; set; }

        public int SonsCount { get; set; }
    }
}