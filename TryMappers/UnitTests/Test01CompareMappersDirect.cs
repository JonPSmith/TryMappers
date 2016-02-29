#region licence
// ======================================================================================
// TryMappers - compare AutoMapper and ExpressMapper for LINQ and develop flattener for ExpressMapper
// Filename: Test01CompareMappersDirect.cs
// Date Created: 2016/02/25
// 
// Under the MIT License (MIT)
// 
// Written by Jon Smith : GitHub JonPSmith, www.thereformedprogrammer.net
// ======================================================================================
#endregion

using NUnit.Framework;
using TryMappers.Classes;
using TryMappers.Helpers;

namespace TryMappers.UnitTests
{
    public class Test01CompareMappersDirect
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
                for (int i = 0; i < numTimes; i++)
                {
                    //ATTEMPT
                    var mapper = config.CreateMapper();
                    var dto = mapper.Map<SimpleClassDto>(SimpleClass.CreateOne());

                    //VERIFY
                    dto.MyDateTime.Year.ShouldEqual(2016);
                }
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
                config = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<FatherSon, GenerationFlattenDto>());
            }
            using (new TimerToConsole($"automapper-map: GenerationFlattenDto - for {numTimes}."))
            {
                for (int i = 0; i < numTimes; i++)
                {
                    //ATTEMPT
                    var mapper = config.CreateMapper();
                    var dto = mapper.Map<GenerationFlattenDto>(FatherSon.CreateOne());

                    //VERIFY
                    dto.MyString.ShouldEqual("Father");
                    dto.SonMyString.ShouldEqual("Son");
                    dto.SonGrandsonMyString.ShouldEqual("Grandson");
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
            ExpressMapper.Mapper.Reset();
            using (new TimerToConsole("ExpressMapper-setup: SimpleClass"))
            {
                ExpressMapper.Mapper.Register<SimpleClass, SimpleClassDto>();
                ExpressMapper.Mapper.Compile();
            }
            using (new TimerToConsole($"ExpressMapper-map: SimpleClass - for {numTimes}"))
            {
                for (int i = 0; i < numTimes; i++)
                {
                    //ATTEMPT
                    var dto = new SimpleClassDto();
                    ExpressMapper.Mapper.Map(SimpleClass.CreateOne(), dto);

                    //VERIFY   
                    dto.MyDateTime.Year.ShouldEqual(2016);
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
            ExpressMapper.Mapper.Reset();
            using (new TimerToConsole("ExpressMapper-setup: GenerationFlattenDto"))
            {
                ExpressMapper.Mapper.Register<FatherSon, GenerationFlattenDto>()
                    .Member(dest => dest.SonMyInt, src => src.Son.MyInt)
                    .Member(dest => dest.SonGrandsonMyInt, src => src.Son.Grandson.MyInt)
                    .Member(dest => dest.SonMyString, src => src.Son.MyString)
                    .Member(dest => dest.SonGrandsonMyString, src => src.Son.Grandson.MyString);
                ExpressMapper.Mapper.Compile();
            }
            using (new TimerToConsole($"ExpressMapper-map: GenerationFlattenDto - for {numTimes}"))
            {
                for (int i = 0; i < numTimes; i++)
                {
                    //ATTEMPT
                    var dto = new GenerationFlattenDto();
                    ExpressMapper.Mapper.Map(FatherSon.CreateOne(), dto);

                    //VERIFY   
                    dto.MyString.ShouldEqual("Father");
                    dto.SonMyString.ShouldEqual("Son");
                    dto.SonGrandsonMyString.ShouldEqual("Grandson");
                }
            }
        }
    }
}