#region licence
// ======================================================================================
// TryMappers - compare AutoMapper and ExpressMapper for LINQ and develop flattener for ExpressMapper
// Filename: FlattenMapper.cs
// Date Created: 2016/02/26
// 
// Under the MIT License (MIT)
// 
// Written by Jon Smith : GitHub JonPSmith, www.thereformedprogrammer.net
// ======================================================================================
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TryMappers.Flatteners.Internal;

namespace TryMappers.Flatteners
{
    public class FlattenMapper<TSource, TDest>
    {
        private readonly PropertyInfo[] _allDestProps;
        private readonly PropertyInfo[] _allSourceProps;

        private readonly List<PropertyInfo> _filteredDestProps;

        private readonly List<ExpressMemberInfo> _foundFlattens = new List<ExpressMemberInfo>();

        public FlattenMapper()
        {
            _allDestProps = GetPropertiesRightAccess<TDest>();
            _allSourceProps = GetPropertiesRightAccess<TSource>();
            //ExpressMapper with match the top level properties, so we ignore those
            _filteredDestProps = FilterOutExactMatches(_allDestProps, _allSourceProps);
        }

        internal IReadOnlyCollection<ExpressMemberInfo> FoundFlattens => _foundFlattens.AsReadOnly();

        public void Register()
        {
            var filteredSourceProps = FilterOutExactMatches(_allSourceProps, _allDestProps);
            ScanSourceProps(filteredSourceProps);
        }


        private void ScanSourceProps(List<PropertyInfo> sourcePropsToScan, 
            string prefix = "", PropertyInfo[] sourcePropPath = null)
        {
            foreach (var destProp in _filteredDestProps.ToList())
                //scan source property name against dest that has no direct match with any of the source property names
                if (_filteredDestProps.Contains(destProp))
                    //This allows for entries to be removed from the list
                    ScanSourceClassRecursively(sourcePropsToScan, destProp, prefix, sourcePropPath ?? new PropertyInfo [] {});
        }

        private void ScanSourceClassRecursively(IEnumerable<PropertyInfo> sourceProps, PropertyInfo destProp, 
            string prefix, PropertyInfo [] sourcePropPath)
        {

            foreach (var matchedStartSrcProp in sourceProps.Where(x => destProp.Name.StartsWith(prefix+x.Name)))
            {
                var matchStart = prefix + matchedStartSrcProp.Name;
                if (destProp.Name == matchStart)
                {
                    //direct match

                    if (destProp.PropertyType == matchedStartSrcProp.PropertyType)
                    {
                        _foundFlattens.Add( new ExpressMemberInfo(destProp, sourcePropPath, matchedStartSrcProp));
                        _filteredDestProps.Remove(destProp);        //matched, so take it out
                    }

                    return;
                }

                if (!matchedStartSrcProp.PropertyType.IsClass)
                    //no expectation of going deeper or finding a Ienumerable method so skip on
                    continue;


                if (!typeof (IEnumerable<>).IsAssignableFrom(destProp.PropertyType))
                {
                    var classProps = GetPropertiesRightAccess(matchedStartSrcProp.PropertyType);
                    var clonedList = sourcePropPath.ToList();
                    clonedList.Add(matchedStartSrcProp);
                    ScanSourceClassRecursively(classProps, destProp, matchStart, clonedList.ToArray());
                }
                else if (destProp.PropertyType != typeof (string))
                {
                    //its an enumerable class so see if it matches a an enumrable method

                    var enumeableMethod = LinqMethod.MatchsWithLinqMethod(destProp, matchStart);
                    if (enumeableMethod != null)
                    {
                        _foundFlattens.Add(new ExpressMemberInfo(destProp, sourcePropPath, matchedStartSrcProp, enumeableMethod));
                        _filteredDestProps.Remove(destProp);        //matched, so take it out
                    }
                }
            }
        }

        private static PropertyInfo[] GetPropertiesRightAccess<T>()
        {
            return GetPropertiesRightAccess(typeof (T));
        }

        private static PropertyInfo[] GetPropertiesRightAccess(Type classType)
        {
            return classType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
        }

        private static List<PropertyInfo> FilterOutExactMatches(PropertyInfo[] propsToFilter, PropertyInfo[] filterAgainst)
        {
            var filterNames = filterAgainst.Select(x => x.Name).ToArray();
            return propsToFilter.Where(x => !filterNames.Contains(x.Name)).ToList();

        }
    }
}
