#region licence
// ======================================================================================
// TryMappers - compare AutoMapper and ExpressMapper for LINQ and develop flattener for ExpressMapper
// Filename: Test30FlattenExpressMapperDirect.cs
// Date Created: 2016/03/15
// 
// Under the MIT License (MIT)
// 
// Written by Jon Smith : GitHub JonPSmith, www.thereformedprogrammer.net
// ======================================================================================
#endregion

using NUnit.Framework;
using TryMappers.Classes;
using TryMappers.Flatteners;
using TryMappers.Helpers;

namespace TryMappers.UnitTests
{
    public class Test30FlattenExpressMapperDirect
    {

        [Test]
        public void Test01ExpressMapperGenerationFlattenDtoOk()
        {
            //SETUP
            ExpressMapper.Mapper.Reset();
            ExpressMapper.Mapper.Register<Father, GenerationFlattenDto>().FlattenSource();
            ExpressMapper.Mapper.Compile();
            
            //ATTEMPT
            var dto = new GenerationFlattenDto();
            ExpressMapper.Mapper.Map(Father.CreateOne(), dto);

            //VERIFY   
            dto.MyString.ShouldEqual("Father");
            dto.SonMyString.ShouldEqual("Son");
            dto.SonGrandsonMyString.ShouldEqual("Grandson");
        }

        [Test]
        public void Test02ExpressMapperGenerationFlattenDtoOverrideOk()
        {
            //SETUP
            ExpressMapper.Mapper.Reset();
            ExpressMapper.Mapper.Register<Father, GenerationFlattenDto>()
                .Member(dest => dest.SonGrandsonMyString, src => src.MyString).FlattenSource();
            ExpressMapper.Mapper.Compile();

            //ATTEMPT
            var dto = new GenerationFlattenDto();
            ExpressMapper.Mapper.Map(Father.CreateOne(), dto);

            //VERIFY   
            dto.MyString.ShouldEqual("Father");
            dto.SonMyString.ShouldEqual("Son");
            dto.SonGrandsonMyString.ShouldEqual("Father");
        }

        [Test]
        public void Test10ExpressMapperFatherSonsCountDtoOk()
        {
            //SETUP
            ExpressMapper.Mapper.Reset();
            ExpressMapper.Mapper.Register<FatherSons, FatherSonsCountDto>().FlattenSource();
            ExpressMapper.Mapper.Compile();

            //ATTEMPT
            var dto = new FatherSonsCountDto();
            ExpressMapper.Mapper.Map(FatherSons.CreateOne(), dto);

            //VERIFY   
            dto.MyString.ShouldEqual("Father");
            dto.SonsCount.ShouldEqual(5);
        }
    }
}