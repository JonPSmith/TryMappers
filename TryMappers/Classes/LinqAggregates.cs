#region licence
// ======================================================================================
// TryMappers - compare AutoMapper and ExpressMapper for LINQ and develop flattener for ExpressMapper
// Filename: LinqAggregates.cs
// Date Created: 2016/03/16
// 
// Under the MIT License (MIT)
// 
// Written by Jon Smith : GitHub JonPSmith, www.thereformedprogrammer.net
// ======================================================================================
#endregion

using System.Collections.Generic;

namespace TryMappers.Classes
{
    public class LinqAggregates
    {

        public ICollection<int> Ints { get; set; } 
        public ICollection<double> Doubles { get; set; }

    }
}