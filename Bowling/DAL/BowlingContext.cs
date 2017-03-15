using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Bowling.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Bowling.DAL
{
    public class BowlingContext : DbContext
    {
        public BowlingContext() : base("BowlingContext")
        {

        }
        public DbSet<Person> People { get; set; }
        public DbSet<Lane> Lanes { get; set; }
        public DbSet<Reserve> Reserves { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>() ;
        }

        public System.Data.Entity.DbSet<Bowling.Models.Appointment> Appointments { get; set; }
    }
}