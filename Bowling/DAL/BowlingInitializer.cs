using Bowling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Bowling.DAL
{
    public class BowlingInitializer : System.Data.Entity. DropCreateDatabaseIfModelChanges<BowlingContext>
    {
        protected override void Seed(BowlingContext context)
        {
            var people = new List<Person>
            {
                new Person {FirstName= "Bob", LastName="Bokio", Email= "bobbokio@email.com"},
                new Person {FirstName= "Robert", LastName="Wilfred", Email= "robFred@email.com"},
                new Person {FirstName= "Angela", LastName="Bee", Email= "AngeBee@email.com"},
                new Person {FirstName= "Lily", LastName="Dar", Email= "LilyDar@email.com"},
                new Person {FirstName= "Marcus", LastName="Kim", Email= "MarcKim@email.com"}
            };

            people.ForEach(s => context.People.Add(s));
            context.SaveChanges();
            var lanes = new List<Lane>
            {
                new Lane {Name="Lane 1", NumbOfPeople= 5,},
                new Lane {Name="Lane 2", NumbOfPeople= 5},
                new Lane {Name="Lane 3", NumbOfPeople= 3},
                new Lane {Name="Lane 4", NumbOfPeople= 5},
                new Lane {Name="Lane 5", NumbOfPeople= 7},

            };
            lanes.ForEach(p => context.Lanes.Add(p));
            context.SaveChanges();
        }
    }
}