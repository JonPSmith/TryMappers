#region licence
// ======================================================================================
// Mvc5UsingBower - An example+library to allow an MVC project to use Bower and Grunt
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
        public LinqMethod LinqMethodEnding { get; private set; }


        public ExpressMemberInfo(PropertyInfo destMember, PropertyInfo[] sourcePathMembers, PropertyInfo lastMemberToAdd, 
            LinqMethod linqMethodEnding  = null)
        {
            DestMember = destMember;
            LinqMethodEnding = linqMethodEnding;

            var list = sourcePathMembers.ToList();
            list.Add(lastMemberToAdd);
            SourcePathMembers = list;
        }

        public override string ToString()
        {
            var enumMethod = LinqMethodEnding == null ? "" : $".{LinqMethodEnding}()";
            return $"DestMember: {DestMember.Name}, Source: {string.Join(".",SourcePathMembers.Select(x => x.Name))}{enumMethod}";
        }
    }
}