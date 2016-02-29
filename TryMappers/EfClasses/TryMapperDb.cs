#region licence
// ======================================================================================
// TryMappers - compare AutoMapper and ExpressMapper for LINQ and develop flattener for ExpressMapper
// Filename: Parent.cs
// Date Created: 2016/02/29
// 
// Under the MIT License (MIT)
// 
// Written by Jon Smith : GitHub JonPSmith, www.thereformedprogrammer.net
// ======================================================================================
#endregion

using System.Data.Entity;
using TryMappers.Classes;

namespace TryMappers.EfClasses
{
    public class TryMapperDb : DbContext 
    {
        public DbSet<Father> Fathers { get; set; }
        public DbSet<Son> Sons { get; set; }
        public DbSet<Grandson> Grandsons { get; set; }
        public DbSet<FatherSons> FatherSons { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Son>()
                .HasOptional(x => x.Grandson);

            base.OnModelCreating(modelBuilder);
        }
    }
}