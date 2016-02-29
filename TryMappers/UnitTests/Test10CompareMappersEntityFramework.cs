#region licence
// ======================================================================================
// TryMappers - compare AutoMapper and ExpressMapper for LINQ and develop flattener for ExpressMapper
// Filename: Test10CompareMappersEntityFramework.cs
// Date Created: 2016/02/29
// 
// Under the MIT License (MIT)
// 
// Written by Jon Smith : GitHub JonPSmith, www.thereformedprogrammer.net
// ======================================================================================
#endregion

using System.Linq;
using AutoMapper.QueryableExtensions;
using ExpressMapper.Extensions;
using NUnit.Framework;
using TryMappers.Classes;
using TryMappers.EfClasses;
using TryMappers.Helpers;

namespace TryMappers.UnitTests
{
    public class Test10CompareMappersEntityFramework
    {
        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            using (var db = new TryMapperDb())
            {
                db.ResetDatabase();
            }
        }

        [Ignore("Used with database schema changes")]
        [Test]
        public void WipeCreateDatabase()
        {

            using (var db = new TryMapperDb())
            {
                db.DeleteCreateDatabase();
            }
        }

        [Test]
        public void Test01AutoMapperGenerationFlattenDto()
        {
            //SETUP
            AutoMapper.MapperConfiguration config = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<Father, GenerationFlattenDto>());

            //ATTEMPT
            using (var db = new TryMapperDb())
            {
                
                var list = db.Fathers.Where(x => x.Son.Grandson != null).ProjectTo<GenerationFlattenDto>(config).ToList();        //force IQueryable to be executed

                //VERIFY
                list.Count.ShouldEqual(DatabaseHelpers.NumFathersWithGrandsons);
                list.Count(x => x.SonMyString == null).ShouldEqual(0);
                list.Count(x => x.SonGrandsonMyString == null).ShouldEqual(0);
            }
        }

        [Test]
        public void Test02AutoMapperGenerationFlattenDtoAgain()
        {
            //SETUP
            AutoMapper.MapperConfiguration config = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<Father, GenerationFlattenDto>());

            //ATTEMPT
            using (var db = new TryMapperDb())
            {

                var list = db.Fathers.Where(x => x.Son.Grandson != null).ProjectTo<GenerationFlattenDto>(config).ToList();        //force IQueryable to be executed

                //VERIFY
                list.Count.ShouldEqual(DatabaseHelpers.NumFathersWithGrandsons);
                list.Count(x => x.SonMyString == null).ShouldEqual(0);
                list.Count(x => x.SonGrandsonMyString == null).ShouldEqual(0);
            }
        }


        [Test]
        public void Test05AutoMapperFatherSonsCountDto()
        {
            //SETUP
            AutoMapper.MapperConfiguration config = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<FatherSons, FatherSonsCountDto>());

            //ATTEMPT
            using (var db = new TryMapperDb())
            {
                var list = db.FatherSons.ProjectTo<FatherSonsCountDto>(config).ToList();        //force IQueryable to be executed

                //VERIFY
                list.Count.ShouldEqual(DatabaseHelpers.NumFathersWithSons);
                list.First().MyInt.ShouldEqual(1);
                list.First().SonsCount.ShouldEqual(DatabaseHelpers.NumSonsToFatherSons);
            }
        }

        [Test]
        public void Test06AutoMapperFatherSonsCountDtoAgain()
        {
            //SETUP
            AutoMapper.MapperConfiguration config = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<FatherSons, FatherSonsCountDto>());

            //ATTEMPT
            using (var db = new TryMapperDb())
            {
                var list = db.FatherSons.ProjectTo<FatherSonsCountDto>(config).ToList();        //force IQueryable to be executed

                //VERIFY
                list.Count.ShouldEqual(DatabaseHelpers.NumFathersWithGrandsons);
                list.First().MyInt.ShouldEqual(1);
                list.First().SonsCount.ShouldEqual(DatabaseHelpers.NumSonsToFatherSons);
            }
        }


        //-----------------------------------------------------------------------
        //ExpressMapper

        [Test]
        public void Test10ExpressMapperGenerationFlattenDto()
        {
            //SETUP
            ExpressMapper.Mapper.Reset();
            ExpressMapper.Mapper.Register<Father, GenerationFlattenDto>()
                .Member(dest => dest.SonMyInt, src => src.Son.MyInt)
                .Member(dest => dest.SonGrandsonMyInt, src => src.Son.Grandson.MyInt)
                .Member(dest => dest.SonMyString, src => src.Son.MyString)
                .Member(dest => dest.SonGrandsonMyString, src => src.Son.Grandson.MyString);
            ExpressMapper.Mapper.Compile();

            //ATTEMPT
            using (var db = new TryMapperDb())
            {
                var list = db.Fathers.Where(x => x.Son.Grandson != null).Project<Father, GenerationFlattenDto>().ToList();
                //var list = db.Fathers.Project<Father, GenerationFlattenDto>().ToList();

                //VERIFY
                list.Count.ShouldEqual(DatabaseHelpers.NumFathersWithGrandsons);
                list.Count(x => x.SonMyString == null).ShouldEqual(0);
                list.Count(x => x.SonGrandsonMyString == null).ShouldEqual(0);
            }
        }

        [Test]
        public void Test11ExpressMapperGenerationFlattenDtoAgain()
        {
            //SETUP
            ExpressMapper.Mapper.Reset();
            ExpressMapper.Mapper.Register<Father, GenerationFlattenDto>()
                .Member(dest => dest.SonMyInt, src => src.Son.MyInt)
                .Member(dest => dest.SonGrandsonMyInt, src => src.Son.Grandson.MyInt)
                .Member(dest => dest.SonMyString, src => src.Son.MyString)
                .Member(dest => dest.SonGrandsonMyString, src => src.Son.Grandson.MyString);
            ExpressMapper.Mapper.Compile();

            //ATTEMPT
            using (var db = new TryMapperDb())
            {
                var list = db.Fathers.Where(x => x.Son.Grandson != null).Project<Father, GenerationFlattenDto>().ToList();
                //var list = db.Fathers.Project<Father, GenerationFlattenDto>().ToList();

                //VERIFY
                list.Count.ShouldEqual(DatabaseHelpers.NumFathersWithGrandsons);
                list.Count(x => x.SonMyString == null).ShouldEqual(0);
                list.Count(x => x.SonGrandsonMyString == null).ShouldEqual(0);
            }
        }

        [Test]
        public void Test15ExpressMapperFatherSonsCountDto()
        {
            //SETUP
            ExpressMapper.Mapper.Reset();
            ExpressMapper.Mapper.Register<FatherSons, FatherSonsCountDto>()
                .Member(dest => dest.SonsCount, src => src.Sons.Count());
            ExpressMapper.Mapper.Compile();

            //ATTEMPT
            using (var db = new TryMapperDb())
            {
                var list = db.FatherSons.Project<FatherSons, FatherSonsCountDto>().ToList();

                //VERIFY
                list.Count.ShouldEqual(DatabaseHelpers.NumFathersWithGrandsons);
                list.First().MyInt.ShouldEqual(1);
                list.First().SonsCount.ShouldEqual(DatabaseHelpers.NumSonsToFatherSons);
            }
        }

        [Test]
        public void Test16ExpressMapperFatherSonsCountDtoAgain()
        {
            //SETUP
            ExpressMapper.Mapper.Reset();
            ExpressMapper.Mapper.Register<FatherSons, FatherSonsCountDto>()
                .Member(dest => dest.SonsCount, src => src.Sons.Count());
            ExpressMapper.Mapper.Compile();

            //ATTEMPT
            using (var db = new TryMapperDb())
            {
                var list = db.FatherSons.Project<FatherSons, FatherSonsCountDto>().ToList();

                //VERIFY
                list.Count.ShouldEqual(DatabaseHelpers.NumFathersWithGrandsons);
                list.First().MyInt.ShouldEqual(1);
                list.First().SonsCount.ShouldEqual(DatabaseHelpers.NumSonsToFatherSons);
            }
        }
    }
}