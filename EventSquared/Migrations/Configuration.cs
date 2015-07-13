namespace EventSquared.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using EventSquared.Models;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var manager = new UserManager<ApplicationUser>(
                            new UserStore<ApplicationUser>(
                                new ApplicationDbContext()));

            var user = new ApplicationUser()
            {
                FirstName = "Rob",
                LastName = "Cabardo",
                UserName = "email@Example.com",
                Email = "email@Example.com",
                PhoneNumber = "314-555-1234",
                EmailConfirmed = true,
                Address = new Address { 
                    Street = "321 Main Street", 
                    City = "St. Louis", 
                    State = "MO", 
                    ZipCode = "63108" }
            };

            manager.Create(user, "Password1");

            context.Events.Add(new Event
            {
                Title = "Jane's Birthday Party",
                StartDate = new DateTime(2015, 7, 17),
                Description = "Join us in celebrating Jane's Birthday!",
                ApplicationUserId = user.Id,
                Address = new Address {
                    Street = "101 Forest Ave",
                    City = "St. Louis",
                    State = "MO",
                    ZipCode = "63103" }
            });

            context.Squares.Add(new Square
            {
                Title = "Dinner",
                CurrentTime = new DateTime(2015, 7, 10, 16, 44, 12),
                StartTime = new DateTime(2015, 7, 17, 5, 0, 0),
                EndTime = new DateTime(2015, 7, 17, 7, 0, 0),
                ApplicationUserId = user.Id,
                Location = "Schlafly Taproom",
                Description = "Chicken and vegetarian options available."
            });

            context.Squares.Add(new Square
            {
                Title = "Gifts",
                CurrentTime = new DateTime(2015, 7, 13, 11, 31, 32),
                StartTime = new DateTime(2015, 7, 17, 7, 0, 0),
                EndTime = new DateTime(2015, 7, 17, 7, 30, 0),
                ApplicationUserId = user.Id,
                Location = "Rob's House",
                Description = "Contact Rob for address and directions."
            });

            context.Squares.Add(new Square
            {
                Title = "Dancing",
                CurrentTime = new DateTime(2015, 7, 14, 13, 11, 30),
                StartTime = new DateTime(2015, 7, 17, 9, 0, 0),
                EndTime = new DateTime(2015, 7, 17, 11, 0, 0),
                ApplicationUserId = user.Id,
                Location = "The Club",
                Description = "Look for our VIP private area."
            });
        }
    }
}
