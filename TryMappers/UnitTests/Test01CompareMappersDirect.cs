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

using System;
using ExpressMapper;
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
            using (new TimerToConsole())
            {
                config = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<SimpleClass, SimpleClassDto>());
            }
            using (new TimerToConsole())
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
            using (new TimerToConsole())
            {
                config = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<Father, GenerationFlattenDto>());
            }
            using (new TimerToConsole())
            {
                for (int i = 0; i < numTimes; i++)
                {
                    //ATTEMPT
                    var mapper = config.CreateMapper();
                    var dto = mapper.Map<GenerationFlattenDto>(Father.CreateOne());

                    //VERIFY
                    dto.MyString.ShouldEqual("Father");
                    dto.SonMyString.ShouldEqual("Son");
                    dto.SonGrandsonMyString.ShouldEqual("Grandson");
                }
            }
        }

        [TestCase(1, 1)]
        [TestCase(1, 2)]
        [TestCase(100, 3)]
        [TestCase(1000, 4)]
        public void Test03AutoMapperGenerationFlattenLowerCaseDtoOk(int numTimes, int count)
        {
            //SETUP
            AutoMapper.MapperConfiguration config;
            using (new TimerToConsole())
            {
                config = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<Father, GenerationFlattenLowerCaseDto>());
            }
            using (new TimerToConsole())
            {
                for (int i = 0; i < numTimes; i++)
                {
                    //ATTEMPT
                    var mapper = config.CreateMapper();
                    var dto = mapper.Map<GenerationFlattenLowerCaseDto>(Father.CreateOne());

                    //VERIFY
                    dto.mystring.ShouldEqual("Father");
                    dto.sonmystring.ShouldEqual("Son");
                    dto.Songrandsonmystring.ShouldEqual("Grandson");
                }
            }
        }

        [TestCase(1, 1)]
        [TestCase(1, 2)]
        [TestCase(100, 3)]
        [TestCase(1000, 4)]
        public void Test03AutoMapperGenerationFlattenGrandsonSimpleDtoOk(int numTimes, int count)
        {
            //SETUP
            AutoMapper.MapperConfiguration config;
            using (new TimerToConsole())
            {
                config = new AutoMapper.MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Grandson, SimpleClassDto>();
                    cfg.CreateMap<Father, GenerationFlattenGrandsonSimpleDto>();
                });
            }
            using (new TimerToConsole())
            {
                for (int i = 0; i < numTimes; i++)
                {
                    //ATTEMPT
                    var mapper = config.CreateMapper();
                    var dto = mapper.Map<GenerationFlattenGrandsonSimpleDto>(Father.CreateOne());

                    //VERIFY
                    dto.MyString.ShouldEqual("Father");
                    dto.SonMyString.ShouldEqual("Son");
                    dto.SonGrandson.MyString.ShouldEqual("Grandson");
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
            using (new TimerToConsole())
            {
                ExpressMapper.Mapper.Register<SimpleClass, SimpleClassDto>();
                ExpressMapper.Mapper.Compile();
            }
            using (new TimerToConsole())
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
        public void Test11ExpressMapperSimpleClassOk(int numTimes, int count)
        {
            //SETUP
            ExpressMapper.Mapper.Reset();
            using (new TimerToConsole())
            {
                ExpressMapper.Mapper.Register<SimpleClass, SimpleClassDto>();
                ExpressMapper.Mapper.Register<SimpleClassDto, SimpleClass>();       //check this doesn't clash
                ExpressMapper.Mapper.Compile(CompilationTypes.Source);
            }
            using (new TimerToConsole())
            {
                for (int i = 0; i < numTimes; i++)
                {
                    //ATTEMPT
                    var dest = new SimpleClass();
                    var source = new SimpleClassDto()
                    {
                        MyInt = 1,
                        MyString = "Dto",
                        MyDateTime = new DateTime(2015, 1,1)
                    };
                    ExpressMapper.Mapper.Map(source, dest);

                    //VERIFY   
                    dest.MyInt.ShouldEqual(1);
                    dest.MyString.ShouldEqual("Dto");
                    dest.MyDateTime.Year.ShouldEqual(2015);
                }
            }
        }

        [TestCase(1, 1)]
        [TestCase(1, 2)]
        [TestCase(100, 3)]
        [TestCase(1000, 4)]
        public void Test15ExpressMapperGenerationFlattenDtoOk(int numTimes, int count)
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
                ExpressMapper.Mapper.Compile();
            }
            using (new TimerToConsole())
            {
                for (int i = 0; i < numTimes; i++)
                {
                    //ATTEMPT
                    var dto = new GenerationFlattenDto();
                    ExpressMapper.Mapper.Map(Father.CreateOne(), dto);

                    //VERIFY   
                    dto.MyString.ShouldEqual("Father");
                    dto.SonMyString.ShouldEqual("Son");
                    dto.SonGrandsonMyString.ShouldEqual("Grandson");
                }
            }
        }

        [Test]
        public void Test50AutoMapperMyStringDtoOk()
        {
            //SETUP
            AutoMapper.MapperConfiguration config;
            using (new TimerToConsole())
            {
                config = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<MyStringDto, SimpleClass>());
            }
            
            //ATTEMPT
            var mapper = config.CreateMapper();
            var simpleClass = SimpleClass.CreateOne();
            var dto = new MyStringDto {MyString = "Only this"};
            mapper.Map(dto, simpleClass);

            //VERIFY
            simpleClass.MyInt.ShouldEqual(1);
            simpleClass.MyString.ShouldEqual("Only this");
            simpleClass.MyDateTime.Year.ShouldEqual(2016);
        }

        [Test]
        public void Test50ExpressMapperMyStringDtoOk()
        {
            //SETUP
            ExpressMapper.Mapper.Reset();
            using (new TimerToConsole())
            {
                ExpressMapper.Mapper.Register<MyStringDto, SimpleClass>();
                ExpressMapper.Mapper.Compile();
            }
            //ATTEMPT
            var simpleClass = SimpleClass.CreateOne();
            var dto = new MyStringDto { MyString = "Only this" };
            ExpressMapper.Mapper.Map(dto, simpleClass);

            //VERIFY   
            simpleClass.MyInt.ShouldEqual(1);
            simpleClass.MyString.ShouldEqual("Only this");
            simpleClass.MyDateTime.Year.ShouldEqual(2016);
        }
    }
}