#region licence
// ======================================================================================
// TryMappers - compare AutoMapper and ExpressMapper for LINQ and develop flattener for ExpressMapper
// Filename: DatabaseHelpers.cs
// Date Created: 2016/02/29
// 
// Under the MIT License (MIT)
// 
// Written by Jon Smith : GitHub JonPSmith, www.thereformedprogrammer.net
// ======================================================================================
#endregion

using System.ComponentModel.Design.Serialization;
using TryMappers.Classes;

namespace TryMappers.EfClasses
{
    public static class DatabaseHelpers
    {
        public const int NumFathersWithGrandsons = 100;
        public const int NumFathersWithNoGrandsons = 1;
        public const int NumFathersWithSons = 100;
        public const int NumSonsToFatherSons = 5;

        public static void DeleteCreateDatabase(this TryMapperDb db)
        {
            if (db.Database.Exists())
                db.Database.Delete();

            db.Database.Create();
        }

        public static void ResetDatabase(this TryMapperDb db)
        {
            db.Fathers.RemoveRange(db.Fathers);
            db.FatherSons.RemoveRange(db.FatherSons);
            db.Sons.RemoveRange(db.Sons);
            db.Grandsons.RemoveRange(db.Grandsons);
            db.SaveChanges();

            //add 100 Father->Son->Grandson
            db.Fathers.AddRange(Father.CreateMany(NumFathersWithGrandsons));
            var father = Father.CreateOne();
            father.Son.Grandson = null;
            //add one Father->Son (no Grandson)
            db.Fathers.Add(father);

            //add 100 Father's with five sons
            db.FatherSons.AddRange(FatherSons.CreateMany(NumFathersWithSons, NumSonsToFatherSons));
            db.SaveChanges();
        }
    }
}