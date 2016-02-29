#region licence
// ======================================================================================
// TryMappers - compare AutoMapper and ExpressMapper for LINQ and develop flattener for ExpressMapper
// Filename: ExpressMemberInfo.cs
// Date Created: 2016/02/27
// 
// Under the MIT License (MIT)
// 
// Written by Jon Smith : GitHub JonPSmith, www.thereformedprogrammer.net
// ======================================================================================
#endregion

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TryMappers.Flatteners.Internal;

namespace TryMappers.Flatteners
{
    internal class ExpressMemberInfo
    {
        public ExpressMemberInfo(PropertyInfo destMember, PropertyInfo[] sourcePathMembers, PropertyInfo lastMemberToAdd, 
            LinqMethod linqMethodSuffix  = null)
        {
            DestMember = destMember;
            LinqMethodSuffix = linqMethodSuffix;

            var list = sourcePathMembers.ToList();
            list.Add(lastMemberToAdd);
            SourcePathMembers = list;
        }

        /// <summary>
        /// The Destination property in the DTO
        /// </summary>
        public PropertyInfo DestMember { get; private set; }

        /// <summary>
        /// The list of properties in order to get to the source property we want
        /// </summary>
        public ICollection<PropertyInfo> SourcePathMembers { get; private set; }

        /// <summary>
        /// Optional Linq Method to apply to an enumerable source
        /// </summary>
        public LinqMethod LinqMethodSuffix { get; private set; }

        public override string ToString()
        {
            var linqMethodStr = LinqMethodSuffix == null ? "" : LinqMethodSuffix.ToString();
            return $"dest => dest.{DestMember.Name}, src => src.{string.Join(".",SourcePathMembers.Select(x => x.Name))}{linqMethodStr}";
        }
    }
}