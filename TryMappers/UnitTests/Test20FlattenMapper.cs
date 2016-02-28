#region licence
// ======================================================================================
// Mvc5UsingBower - An example+library to allow an MVC project to use Bower and Grunt
// Filename: FlattenMapper.cs
// Date Created: 2016/02/25
// 
// Under the MIT License (MIT)
// 
// Written by Jon Smith : GitHub JonPSmith, www.thereformedprogrammer.net
// ======================================================================================
#endregion

using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using Tests.Helpers;
using TryMappers.Classes;
using TryMappers.Flatteners;

namespace TryMappers.UnitTests
{
    public class Test20FlattenMapper
    {

        [Test]
        public void Test01AutoMapperSimpleClassOk()
        {
            //SETUP
            var f = new FlattenMapper<FatherClass, GenerationFlattenDto>();

            //ATTEMPT
            f.Register();

            //VERIFY
            f.FoundFlattens.Count.ShouldEqual(4);

        }

        

    }
}