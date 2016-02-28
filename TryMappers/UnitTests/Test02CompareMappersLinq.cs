#region licence
// ======================================================================================
// Mvc5UsingBower - An example+library to allow an MVC project to use Bower and Grunt
// Filename: Test01CompareMappers.cs
// Date Created: 2016/02/25
// 
// Under the MIT License (MIT)
// 
// Written by Jon Smith : GitHub JonPSmith, www.thereformedprogrammer.net
// ======================================================================================
#endregion

using System.Diagnostics;
using System.Linq;
using AutoMapper.QueryableExtensions;
using ExpressMapper.Extensions;
using NUnit.Framework;
using Tests.Helpers;
using TryMappers.Classes;

namespace TryMappers.UnitTests
{
    public class Test02CompareMappersLinq
    {

        [TestCase(1, 1)]
        [TestCase(1, 2)]
        [TestCase(100, 3)]
        [TestCase(1000, 4)]
        public void Test01AutoMapperSimpleClassOk(int numTimes, int count)
        {
            //SETUP
            AutoMapper.MapperConfiguration config;
            using (new TimerToConsole($"automapper-setup: SimpleClass - {count} time"))
            {
                config = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<SimpleClass, SimpleClassDto>());
            }
            using (new TimerToConsole($"automapper-map: SimpleClass - for {numTimes}."))
            {
                //ATTEMPT
                var list = SimpleClass.CreateMany(numTimes).AsQueryable().ProjectTo<SimpleClassDto>(config);

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
            using (new TimerToConsole($"automapper-setup: GenerationFlattenDto - {count} time"))
            {
                config = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<FatherClass, GenerationFlattenDto>());
            }
            using (new TimerToConsole($"automapper-map: GenerationFlattenDto - for {numTimes}."))
            {
                for (int i = 0; i < numTimes; i++)
                {
                    //ATTEMPT
                    var list = FatherClass.CreateMany(numTimes).AsQueryable().ProjectTo<GenerationFlattenDto>(config);

                    //VERIFY
                    list.First().MyString.ShouldEqual("Father");
                    list.First().SonMyString.ShouldEqual("Son");
                    list.First().SonGrandsonMyString.ShouldEqual("Grandson");
                }
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
            if (count == 1)
            {
                using (new TimerToConsole("ExpressMapper-setup: SimpleClass"))
                {
                    ExpressMapper.Mapper.Register<SimpleClass, SimpleClassDto>();
                    ExpressMapper.Mapper.Compile();
                }
            }
            using (new TimerToConsole($"ExpressMapper-map: SimpleClass - for {numTimes}"))
            {
                for (int i = 0; i < numTimes; i++)
                {
                    //ATTEMPT
                    var list = SimpleClass.CreateMany(numTimes).AsQueryable().Project<SimpleClass, SimpleClassDto>();

                    //VERIFY   
                    list.First().MyDateTime.Year.ShouldEqual(2016);                
                }
            }
        }

        [TestCase(1, 1)]
        [TestCase(1, 2)]
        [TestCase(100, 3)]
        [TestCase(1000, 4)]
        public void Test11ExpressMapperGenerationFlattenDtoOk(int numTimes, int count)
        {
            //SETUP
            if (count == 1)
            {
                using (new TimerToConsole("ExpressMapper-setup: GenerationFlattenDto"))
                {
                    ExpressMapper.Mapper.Register<FatherClass, GenerationFlattenDto>()
                        .Member(dest => dest.SonMyInt, src => src.Son.MyInt)
                        .Member(dest => dest.SonGrandsonMyInt, src => src.Son.Grandson.MyInt)
                        .Member(dest => dest.SonMyString, src => src.Son.MyString)
                        .Member(dest => dest.SonGrandsonMyString, src => src.Son.Grandson.MyString);
                    ExpressMapper.Mapper.Compile();
                }
            }
            using (new TimerToConsole($"ExpressMapper-map: GenerationFlattenDto - for {numTimes}"))
            {
                for (int i = 0; i < numTimes; i++)
                {
                    //ATTEMPT
                    var list = FatherClass.CreateMany(numTimes).AsQueryable().Project<FatherClass, GenerationFlattenDto>();

                    //VERIFY   
                    list.First().MyString.ShouldEqual("Father");
                    list.First().SonMyString.ShouldEqual("Son");
                    list.First().SonGrandsonMyString.ShouldEqual("Grandson");
                }
            }
        }

    }
}