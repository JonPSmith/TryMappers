#region licence
// ======================================================================================
// TryMappers - compare AutoMapper and ExpressMapper for LINQ and develop flattener for ExpressMapper
// Filename: Test05CompareMappersLinqMethods.cs
// Date Created: 2016/02/28
// 
// Under the MIT License (MIT)
// 
// Written by Jon Smith : GitHub JonPSmith, www.thereformedprogrammer.net
// ======================================================================================
#endregion

using System;
using System.Linq;
using AutoMapper.QueryableExtensions;
using ExpressMapper.Extensions;
using NUnit.Framework;
using TryMappers.Classes;
using TryMappers.Helpers;

namespace TryMappers.UnitTests
{
    public class Test05CompareMappersLinqMethods
    {

        [TestCase(1, 1)]
        [TestCase(1, 2)]
        [TestCase(100, 3)]
        [TestCase(1000, 4)]
        public void Test01AutoMapperFatherSonsCountDtoOk(int numTimes, int count)
        {
            //SETUP
            AutoMapper.MapperConfiguration config;
            using (new TimerToConsole())
            {
                config = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<FatherSons, FatherSonsCountDto>());
            }
            var queryData = FatherSons.CreateMany(numTimes).AsQueryable();
            using (new TimerToConsole())
            {
                //ATTEMPT
                var list = queryData.ProjectTo<FatherSonsCountDto>(config).ToList();//force IQueryable to be executed

                //VERIFY
                list.First().MyString.ShouldEqual("Father");
                list.First().SonsCount.ShouldEqual(5);
            }
        }

        [TestCase(1, 1)]
        [TestCase(1, 2)]
        [TestCase(100, 3)]
        [TestCase(1000, 4)]
        public void Test02AutoMapperFatherSonsCountWithWhereDtoOk(int numTimes, int count)
        {
            //SETUP
            AutoMapper.MapperConfiguration config;
            using (new TimerToConsole())
            {
                config = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<FatherSons, FatherSonsCountDto>()
                .ForMember(dest => dest.SonsCount,
                    opt => opt.MapFrom(c => c.Sons.Count(e => e.MyInt > Int32.MaxValue/2))));
            }
            var queryData = FatherSons.CreateMany(numTimes).AsQueryable();
            using (new TimerToConsole())
            {
                //ATTEMPT
                var list = queryData.ProjectTo<FatherSonsCountDto>(config).ToList();//force IQueryable to be executed

                //VERIFY
                list.First().MyString.ShouldEqual("Father");
                (list.First().SonsCount >= 0 && list.First().SonsCount <= 5).ShouldEqual(true);
            }
        }

        //--------------------------------------------------------
        //ExpressMapper


        [TestCase(1, 1)]
        [TestCase(1, 2)]
        [TestCase(100, 3)]
        [TestCase(1000, 4)]
        public void Test10ExpressMapperFatherSonsCountDtoOk(int numTimes, int count)
        {
            //SETUP
            ExpressMapper.Mapper.Reset();
            using (new TimerToConsole())
            {
                ExpressMapper.Mapper.Register<FatherSons, FatherSonsCountDto>()
                    .Member(dest => dest.SonsCount, src => src.Sons.Count());
                ExpressMapper.Mapper.Compile();
            }

            var queryData = FatherSons.CreateMany(numTimes).AsQueryable();
            using (new TimerToConsole())
            {
                //ATTEMPT
                var list = queryData.Project<FatherSons, FatherSonsCountDto>().ToList();//force IQueryable to be executed

                //VERIFY   
                list.First().MyString.ShouldEqual("Father");
                list.First().SonsCount.ShouldEqual(5);
            }
        }

        [TestCase(1, 1)]
        [TestCase(1, 2)]
        [TestCase(100, 3)]
        [TestCase(1000, 4)]
        public void Test11ExpressMapperFatherSonsWithWhereCountDtoOk(int numTimes, int count)
        {
            //SETUP
            ExpressMapper.Mapper.Reset();
            using (new TimerToConsole())
            {
                ExpressMapper.Mapper.Register<FatherSons, FatherSonsCountDto>()
                    .Member(dest => dest.SonsCount, src => src.Sons.Count(e => e.MyInt > Int32.MaxValue/2));
                ExpressMapper.Mapper.Compile();
            }

            var queryData = FatherSons.CreateMany(numTimes).AsQueryable();
            using (new TimerToConsole())
            {
                //ATTEMPT
                var list = queryData.Project<FatherSons, FatherSonsCountDto>().ToList();//force IQueryable to be executed

                //VERIFY   
                list.First().MyString.ShouldEqual("Father");
                (list.First().SonsCount >= 0 && list.First().SonsCount <= 5).ShouldEqual(true);
            }
        }
    }
}