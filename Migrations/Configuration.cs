namespace EONReality.Migrations
{
    using System;
    using EONReality.Models;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EONReality.Models.EONRealityContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EONReality.Models.EONRealityContext context)
        {
            context.Users.AddOrUpdate(x => x.UserId,
                new User()
                {
                    UserId = 1,
                    Name = "Nadia Nguyen",
                    Email = "Nadia@Nadia.com",
                    Gender = "Female",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    ARequest = "I need 50 onion rings",
                    Dates = "Day 1",
                    DRegister = new DateTime(2019, 5, 1)
                },
                 new User()
                 {
                     UserId = 2,
                     Name = "Kian Chachi",
                     Email = "Kian@Chachi.com",
                     Gender = "Male",
                     CreatedAt = DateTime.Now,
                     UpdatedAt = DateTime.Now,
                     ARequest = "I don't eat meat",
                     Dates = "Day 1, Day 2",
                     DRegister = new DateTime(2019, 3, 5)
                 },
                  new User()
                  {
                      UserId = 3,
                      Name = "Long Nguyen",
                      Email = "Long@Nguyen.com",
                      Gender = "Male",
                      CreatedAt = DateTime.Now,
                      UpdatedAt = DateTime.Now,
                      ARequest = "2 cats",
                      Dates = "Day 3",
                      DRegister = new DateTime(2019, 5, 21)
                  });
        }
    }
}
