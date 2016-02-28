#region licence
// ======================================================================================
// TryMappers - compare AutoMapper and ExpressMapper for LINQ and develop flattener for ExpressMapper
// Filename: Test20FlattenMapper.cs
// Date Created: 2016/02/27
// 
// Under the MIT License (MIT)
// 
// Written by Jon Smith : GitHub JonPSmith, www.thereformedprogrammer.net
// ======================================================================================
#endregion

using System.Linq;
using NUnit.Framework;
using TryMappers.Classes;
using TryMappers.Flatteners;
using TryMappers.Helpers;

namespace TryMappers.UnitTests
{
    public class Test20FlattenMapper
    {
        [Test]
        public void Test01FlattenFatherClassOk()
        {
            //SETUP
            var f = new FlattenMapper<FatherSon, GenerationFlattenDto>();

            //ATTEMPT
            f.Register();

            //VERIFY
            f.FoundFlattens.Count.ShouldEqual(4);
            var mCode = f.FoundFlattens.ToList();
            var i = 0;
            mCode[i++].ToString().ShouldEqual("dest => dest.SonMyInt, src => src.Son.MyInt");
            mCode[i++].ToString().ShouldEqual("dest => dest.SonMyString, src => src.Son.MyString");
            mCode[i++].ToString().ShouldEqual("dest => dest.SonGrandsonMyInt, src => src.Son.Grandson.MyInt");
            mCode[i++].ToString().ShouldEqual("dest => dest.SonGrandsonMyString, src => src.Son.Grandson.MyString");
        }
    }
}