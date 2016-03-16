#region licence
// ======================================================================================
// TryMappers - compare AutoMapper and ExpressMapper for LINQ and develop flattener for ExpressMapper
// Filename: LinqMethodDto.cs
// Date Created: 2016/03/16
// 
// Under the MIT License (MIT)
// 
// Written by Jon Smith : GitHub JonPSmith, www.thereformedprogrammer.net
// ======================================================================================
#endregion
namespace TryMappers.Classes
{
    public class LinqSingleMethodsDto
    {
        public Son SonsSingle { get; set; }
        public Son SonsSingleOrDefault { get; set; }
    }
}