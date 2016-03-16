#region licence
// ======================================================================================
// TryMappers - compare AutoMapper and ExpressMapper for LINQ and develop flattener for ExpressMapper
// Filename: LinqMethodDto.cs
// Date Created: 2016/03/16
// 
// Under the MIT License (MIT)
// 
// Written by Jon Smith : GitHub JonPSmith, www.thereformedprogrammer.net
// ======================================================================================
#endregion
namespace TryMappers.Classes
{
    public class LinqAggregateMethodsDto
    {
        //"Average", "Max", "Min", "Sum",

        public double IntsAverage { get; set; }     //note that the average method returns a double
        public double DoublesAverage { get; set; }
        public int IntsMax { get; set; }
        public double DoublesMax { get; set; }
        public int IntsMin { get; set; }
        public double DoublesMin { get; set; }
        public int IntsSum { get; set; }
        public double DoublesSum { get; set; }
    }
}