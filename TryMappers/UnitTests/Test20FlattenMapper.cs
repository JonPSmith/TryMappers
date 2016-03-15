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
        public void Test01FlattenFatherToStringOk()
        {
            //SETUP
            var f = new FlattenMapper<Father, GenerationFlattenDto>();

            //ATTEMPT
            f.BuildMemberMapping();

            //VERIFY
            f.FoundFlattens.Count.ShouldEqual(4);
            var mCode = f.FoundFlattens.ToList();
            var i = 0;
            mCode[i++].ToString().ShouldEqual("dest => dest.SonMyInt, src => src.Son.MyInt");
            mCode[i++].ToString().ShouldEqual("dest => dest.SonMyString, src => src.Son.MyString");
            mCode[i++].ToString().ShouldEqual("dest => dest.SonGrandsonMyInt, src => src.Son.Grandson.MyInt");
            mCode[i++].ToString().ShouldEqual("dest => dest.SonGrandsonMyString, src => src.Son.Grandson.MyString");
        }

        [Test]
        public void Test02FlattenFatherSourceAsExpressionOk()
        {
            //SETUP
            var f = new FlattenMapper<Father, GenerationFlattenDto>();

            //ATTEMPT
            f.BuildMemberMapping();

            //VERIFY
            f.FoundFlattens.Count.ShouldEqual(4);
            var mCode = f.FoundFlattens.ToList();
            var i = 0;
            mCode[i++].SourceAsExpression<Father>().ToString().ShouldEqual("src.Son.MyInt");
            mCode[i++].SourceAsExpression<Father>().ToString().ShouldEqual("src.Son.MyString");
            mCode[i++].SourceAsExpression<Father>().ToString().ShouldEqual("src.Son.Grandson.MyInt");
            mCode[i++].SourceAsExpression<Father>().ToString().ShouldEqual("src.Son.Grandson.MyString");
        }

        [Test]
        public void Test03FlattenFatherSourceAsExpressionOk()
        {
            //SETUP
            var f = new FlattenMapper<Father, GenerationFlattenDto>();

            //ATTEMPT
            f.BuildMemberMapping();

            //VERIFY
            f.FoundFlattens.Count.ShouldEqual(4);
            var mCode = f.FoundFlattens.ToList();
            var i = 0;
            mCode[i++].DestAsMemberExpression<GenerationFlattenDto>().ToString().ShouldEqual("dest.SonMyInt");
            mCode[i++].DestAsMemberExpression<GenerationFlattenDto>().ToString().ShouldEqual("dest.SonMyString");
            mCode[i++].DestAsMemberExpression<GenerationFlattenDto>().ToString().ShouldEqual("dest.SonGrandsonMyInt");
            mCode[i++].DestAsMemberExpression<GenerationFlattenDto>().ToString().ShouldEqual("dest.SonGrandsonMyString");

            var xxx = mCode[0].DestAsMemberExpression<GenerationFlattenDto>();
        }

        [Test]
        public void Test10FlattenFatherSonsOk()
        {
            //SETUP
            var f = new FlattenMapper<FatherSons, FatherSonsCountDto>();

            //ATTEMPT
            f.BuildMemberMapping();

            //VERIFY
            f.FoundFlattens.Count.ShouldEqual(1);
            var mCode = f.FoundFlattens.ToList();
            var i = 0;
            mCode[i++].ToString().ShouldEqual("dest => dest.SonsCount, src => src.Sons.Count()");
        }

        [Test]
        public void Test12FlattenFatherSonsSourceAsExpressionOk()
        {
            //SETUP
            var f = new FlattenMapper<FatherSons, FatherSonsCountDto>();

            //ATTEMPT
            f.BuildMemberMapping();

            //VERIFY
            f.FoundFlattens.Count.ShouldEqual(1);
            var mCode = f.FoundFlattens.ToList();
            var i = 0;
            mCode[i++].SourceAsExpression<FatherSons>().ToString().ShouldEqual("src.Sons.Count()");
        }

        [Test]
        public void Test13FlattenFatherSonsSourceAsExpressionOk()
        {
            //SETUP
            var f = new FlattenMapper<FatherSons, FatherSonsCountDto>();

            //ATTEMPT
            f.BuildMemberMapping();

            //VERIFY
            f.FoundFlattens.Count.ShouldEqual(1);
            var mCode = f.FoundFlattens.ToList();
            var i = 0;
            mCode[i++].DestAsMemberExpression<FatherSonsCountDto>().ToString().ShouldEqual("dest.SonsCount");
        }
    }
}