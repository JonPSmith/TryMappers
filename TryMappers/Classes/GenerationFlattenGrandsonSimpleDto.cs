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
    public class GenerationFlattenGrandsonSimpleDto
    {
        public int MyInt { get; set; }
        public string MyString { get; set; }

        public int SonMyInt { get; set; }
        public string SonMyString { get; set; }

        public SimpleClassDto SonGrandson { get; set; }

    }
}