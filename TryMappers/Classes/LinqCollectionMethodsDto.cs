﻿#region licence
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
    public class LinqCollectionMethodsDto
    {
        public bool SonsAny { get; set; } 
        public int SonsCount { get; set; }
        public long SonsLongCount { get; set; }

        public Son SonsFirst { get; set; }
        public Son SonsFirstOrDefault { get; set; }

        public Son SonsLast { get; set; }
        public Son SonsLastOrDefault { get; set; }
    }
}