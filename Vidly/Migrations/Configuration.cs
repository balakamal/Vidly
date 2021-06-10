using Vidly.Models;
using System.Collections.Generic;
namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Vidly.Models.VidlyDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Vidly.Models.VidlyDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            List<MembershipType> membershipTypes = new List<MembershipType>
            {
                new MembershipType{Id = 1, Name = "Pay as you go", SignUpFee = 0, DurationInMonths =0, DiscountRate = 0},
                new MembershipType{Id = 2, Name="Monthly" , SignUpFee = 30, DurationInMonths =1, DiscountRate = 10},
                new MembershipType{Id = 3, Name ="Quartly", SignUpFee = 90, DurationInMonths =3, DiscountRate = 15},
                new MembershipType{Id = 3, Name ="Quartly", SignUpFee = 160, DurationInMonths =3, DiscountRate = 25},
                new MembershipType{Id = 4, Name = "Annual",SignUpFee = 300, DurationInMonths =12, DiscountRate = 30}

            };
            foreach (var i in membershipTypes)
                context.MembershipTypes.AddOrUpdate(i);

        }
    }
}
