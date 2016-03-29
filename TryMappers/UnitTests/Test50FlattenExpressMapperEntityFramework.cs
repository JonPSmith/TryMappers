#region licence
// ======================================================================================
// TryMappers - compare AutoMapper and ExpressMapper for LINQ and develop flattener for ExpressMapper
// Filename: Test50FlattenExpressMapperEntityFramework.cs
// Date Created: 2016/03/16
// 
// Under the MIT License (MIT)
// 
// Written by Jon Smith : GitHub JonPSmith, www.thereformedprogrammer.net
// ======================================================================================
#endregion

using System.Linq;
using ExpressMapper.Extensions;
using NUnit.Framework;
using TryMappers.Classes;
using TryMappers.EfClasses;
using TryMappers.Helpers;

namespace TryMappers.UnitTests
{
    public class Test50FlattenExpressMapperEntityFramework
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
        public void Test10ExpressMapperGenerationFlattenDto()
        {
            //SETUP
            ExpressMapper.Mapper.Reset();
            ExpressMapper.Mapper.Register<Father, GenerationFlattenDto>().Flatten();
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
            ExpressMapper.Mapper.Register<Father, GenerationFlattenDto>().Flatten();
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
            ExpressMapper.Mapper.Register<FatherSons, FatherSonsCountDto>().Flatten();
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
            ExpressMapper.Mapper.Register<FatherSons, FatherSonsCountDto>().Flatten();
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
        public void Test20ExpressMapperLinqCollectionMethodsDto()
        {
            //SETUP
            ExpressMapper.Mapper.Reset();
            ExpressMapper.Mapper.Register<FatherSons, LinqCollectionMethodsDto>().Flatten();
            ExpressMapper.Mapper.Compile();

            //ATTEMPT
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