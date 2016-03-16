#region licence
// ======================================================================================
// TryMappers - compare AutoMapper and ExpressMapper for LINQ and develop flattener for ExpressMapper
// Filename: ExpresMapperExtension.cs
// Date Created: 2016/03/15
// 
// Under the MIT License (MIT)
// 
// Written by Jon Smith : GitHub JonPSmith, www.thereformedprogrammer.net
// ======================================================================================
#endregion

using ExpressMapper;

namespace TryMappers.Flatteners
{
    public static class ExpressMapperExtension
    {
        public static void FlattenSource<TSource, TDest>(this IMemberConfiguration<TSource, TDest> register)
        {
            var f = new FlattenMapper<TSource, TDest>();
            f.BuildMemberMapping();
            foreach (var flattenInfo in f.FoundFlattens)
            {
                register.MemberComputed(flattenInfo.DestAsMemberExpression<TDest>(), flattenInfo.SourceAsExpression<TSource>());
            }
        }
    }
}