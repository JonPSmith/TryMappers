#region licence
// ======================================================================================
// TryMappers - compare AutoMapper and ExpressMapper for LINQ and develop flattener for ExpressMapper
// Filename: Test02CompareMappersLinq.cs
// Date Created: 2016/02/28
// 
// Under the MIT License (MIT)
// 
// Written by Jon Smith : GitHub JonPSmith, www.thereformedprogrammer.net
// ======================================================================================
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using ExpressMapper;
using ExpressMapper.Extensions;
using NUnit.Framework;
using TryMappers.Classes;
using TryMappers.Helpers;

namespace TryMappers.UnitTests
{
    public class Test02CompareMappersLinq
    {
        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            ExpressMapper.Mapper.Reset();
        }

        [TestCase(1, 1)]
        [TestCase(1, 2)]
        [TestCase(100, 3)]
        [TestCase(1000, 4)]
        public void Test01AutoMapperSimpleClassOk(int numTimes, int count)
        {
            //SETUP
            AutoMapper.MapperConfiguration config;
            using (new TimerToConsole())
            {
                config = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<SimpleClass, SimpleClassDto>());
            }
            var queryData = SimpleClass.CreateMany(numTimes).AsQueryable();
            using (new TimerToConsole())
            {
                //ATTEMPT
                var list = queryData.ProjectTo<SimpleClassDto>(config).ToList();        //force IQueryable to be executed

                //VERIFY
                list.First().MyDateTime.Year.ShouldEqual(2016);
            }
        }

        [TestCase(1, 1)]
        [TestCase(1, 2)]
        [TestCase(100, 3)]
        [TestCase(1000, 4)]
        public void Test02AutoMapperGenerationFlattenDtoOk(int numTimes, int count)
        {
            //SETUP
            AutoMapper.MapperConfiguration config;
            using (new TimerToConsole())
            {
                config = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<Father, GenerationFlattenDto>());
            }
            var queryData = Father.CreateMany(numTimes).AsQueryable();
            using (new TimerToConsole())
            {
                //ATTEMPT
                var list = queryData.ProjectTo<GenerationFlattenDto>(config).ToList();        //force IQueryable to be executed

                //VERIFY
                list.First().MyString.ShouldEqual("Father");
                list.First().SonMyString.ShouldEqual("Son");
                list.First().SonGrandsonMyString.ShouldEqual("Grandson");
            }
        }

        [Test]
        public void Test03AutoMapperGenerationFlattenDtoWithNullOk()
        {
            var config = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<Father, GenerationFlattenDto>());
            var single = Father.CreateOne();
            single.Son.Grandson = null;
            var queryData = new List<Father> {single}.AsQueryable();
            using (new TimerToConsole())
            {
                //ATTEMPT
                var ex = Assert.Throws<NullReferenceException>(() =>  queryData.ProjectTo<GenerationFlattenDto>(config).ToList());

                //VERIFY
                ex.Message.ShouldEqual("Object reference not set to an instance of an object.");
            }
        }

        //--------------------------------------------------------
        //ExpressMapper

        [TestCase(1, 1)]
        [TestCase(1, 2)]
        [TestCase(100, 3)]
        [TestCase(1000, 4)]
        public void Test10ExpressMapperSimpleClassOk(int numTimes, int count)
        {
            //SETUP
            ExpressMapper.Mapper.Reset();
            using (new TimerToConsole())
            {
                ExpressMapper.Mapper.Register<SimpleClass, SimpleClassDto>();
                ExpressMapper.Mapper.Compile(CompilationTypes.Source);
            }

            var queryData = SimpleClass.CreateMany(numTimes).AsQueryable();
            using (new TimerToConsole())
            {
                //ATTEMPT
                var list = queryData.Project<SimpleClass, SimpleClassDto>().ToList(); //force IQueryable to be executed

                //VERIFY   
                list.First().MyDateTime.Year.ShouldEqual(2016);
            }
        }

        [TestCase(1, 1)]
        [TestCase(1, 2)]
        [TestCase(100, 3)]
        [TestCase(1000, 4)]
        public void Test11ExpressMapperGenerationFlattenDtoOk(int numTimes, int count)
        {
            //SETUP
            ExpressMapper.Mapper.Reset();
            using (new TimerToConsole())
            {
                ExpressMapper.Mapper.Register<Father, GenerationFlattenDto>()
                    .Member(dest => dest.SonMyInt, src => src.Son.MyInt)
                    .Member(dest => dest.SonGrandsonMyInt, src => src.Son.Grandson.MyInt)
                    .Member(dest => dest.SonMyString, src => src.Son.MyString)
                    .Member(dest => dest.SonGrandsonMyString, src => src.Son.Grandson.MyString);
                ExpressMapper.Mapper.Compile(CompilationTypes.Source);
            }

            var queryData = Father.CreateMany(numTimes).AsQueryable();
            using (new TimerToConsole())
            {
                //ATTEMPT
                var list = queryData.Project<Father, GenerationFlattenDto>().ToList();
                    //force IQueryable to be executed

                //VERIFY   
                list.First().MyString.ShouldEqual("Father");
                list.First().SonMyString.ShouldEqual("Son");
                list.First().SonGrandsonMyString.ShouldEqual("Grandson");
            }
        }

        [Test]
        public void Test13ExpressMapperGenerationFlattenDtoWithNullOk()
        {
            ExpressMapper.Mapper.Reset();
            using (new TimerToConsole())
            {
                ExpressMapper.Mapper.Register<Father, GenerationFlattenDto>()
                    .Member(dest => dest.SonMyInt, src => src.Son.MyInt)
                    .Member(dest => dest.SonGrandsonMyInt, src => src.Son.Grandson.MyInt)
                    .Member(dest => dest.SonMyString, src => src.Son.MyString)
                    .Member(dest => dest.SonGrandsonMyString, src => src.Son.Grandson.MyString);
                ExpressMapper.Mapper.Compile(CompilationTypes.Source);
            }
            var single = Father.CreateOne();
            single.Son.Grandson = null;
            var queryData = new List<Father> { single }.AsQueryable();
            using (new TimerToConsole())
            {
                //ATTEMPT
                var ex = Assert.Throws<NullReferenceException>(() => queryData.Project<Father, GenerationFlattenDto>().ToList());

                //VERIFY
                ex.Message.ShouldEqual("Object reference not set to an instance of an object.");
            }
        }
    }
}