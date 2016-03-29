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
using ExpressMapper;
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
            using (new TimerToConsole())
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
            using (new TimerToConsole())
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
            using (new TimerToConsole())
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
            using (new TimerToConsole())
            using (var db = new TryMapperDb())
            {
                var list = db.FatherSons.ProjectTo<FatherSonsCountDto>(config).ToList();        //force IQueryable to be executed

                //VERIFY
                list.Count.ShouldEqual(DatabaseHelpers.NumFathersWithGrandsons);
                list.First().MyInt.ShouldEqual(1);
                list.First().SonsCount.ShouldEqual(DatabaseHelpers.NumSonsToFatherSons);
            }
        }

        [Ignore("AutoMapper cannot handle some of the LINQ features")]
        [Test]
        public void Test07AutoMapperLinqCollectionMethodsDto()
        {
            //SETUP
            AutoMapper.MapperConfiguration config = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<FatherSons, LinqCollectionMethodsDto>());

            //ATTEMPT
            using (new TimerToConsole())
            using (var db = new TryMapperDb())
            {
                var list = db.FatherSons.ProjectTo<LinqCollectionMethodsDto>(config).ToList();

                //VERIFY
                list.Count.ShouldEqual(DatabaseHelpers.NumFathersWithGrandsons);
                list.All(x => x.SonsAny).ShouldEqual(true);
                list.All(x => x.SonsCount == 5).ShouldEqual(true);
                list.All(x => x.SonsLongCount == 5).ShouldEqual(true);
                list.All(x => x.SonsFirstOrDefault.MyString == "Son").ShouldEqual(true);
            }
        }

        //-----------------------------------------------------------------------
        //ExpressMapper

        [Test]
        public void Test10ExpressMapperGenerationFlattenDto()
        {
            //SETUP
            ExpressMapper.Mapper.Reset();
            ExpressMapper.Mapper.Register<Father, GenerationFlattenDto>().Flatten();
            ExpressMapper.Mapper.Compile(CompilationTypes.Source);

            //ATTEMPT
            using (new TimerToConsole())
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
            ExpressMapper.Mapper.Register<Father, GenerationFlattenDto>().Flatten();
            ExpressMapper.Mapper.Compile(CompilationTypes.Source);

            //ATTEMPT
            using (new TimerToConsole())
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
            ExpressMapper.Mapper.Register<FatherSons, FatherSonsCountDto>().Flatten();
            ExpressMapper.Mapper.Compile(CompilationTypes.Source);

            //ATTEMPT
            using (new TimerToConsole())
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
            ExpressMapper.Mapper.Register<FatherSons, FatherSonsCountDto>().Flatten();
            ExpressMapper.Mapper.Compile(CompilationTypes.Source);

            //ATTEMPT
            using (new TimerToConsole())
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
        public void Test17ExpressMapperLinqCollectionMethodsDto()
        {
            //SETUP
            ExpressMapper.Mapper.Reset();
            ExpressMapper.Mapper.Register<FatherSons, LinqCollectionMethodsDto>().Flatten();
            ExpressMapper.Mapper.Compile(CompilationTypes.Source);

            //ATTEMPT
            using (new TimerToConsole())
            using (var db = new TryMapperDb())
            {
                var list = db.FatherSons.Project<FatherSons, LinqCollectionMethodsDto>().ToList();

                //VERIFY
                list.Count.ShouldEqual(DatabaseHelpers.NumFathersWithGrandsons);
                list.All(x => x.SonsAny).ShouldEqual(true);
                list.All(x => x.SonsCount == 5).ShouldEqual(true);
                list.All(x => x.SonsLongCount == 5).ShouldEqual(true);
                list.All(x => x.SonsFirstOrDefault.MyString == "Son").ShouldEqual(true);
            }
        }
    }
}