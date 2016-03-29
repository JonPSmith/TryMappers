#region licence
// ======================================================================================
// TryMappers - compare AutoMapper and ExpressMapper for LINQ and develop flattener for ExpressMapper
// Filename: GenerationFlattenDto.cs
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
    public class GenerationFlattenLowerCaseDto
    {
        public int myint { get; set; }
        public string mystring { get; set; }

        public int sonmyint { get; set; }
        public string sonmystring { get; set; }

        public int songrandsonmyint { get; set; }
        public string Songrandsonmystring { get; set; }


    }
}