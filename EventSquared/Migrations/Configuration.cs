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
                UserName = "rcaba002@gmail.com",
                Email = "rcaba002@gmail.com",
                PhoneNumber = "314-365-3234",
                EmailConfirmed = true,
                Address = new Address { 
                    Street = "414 N. 23rd Street", 
                    City = "St. Louis", 
                    State = "MO", 
                    ZipCode = "63103" }
            };

            manager.Create(user, "Password1");

            context.Events.Add(new Event
            {
                Title = "32nd Birthday Party",
                StartDate = new DateTime(2015, 9, 26),
                Description = "Join me in celebrating my 32nd Birthday!",
                ApplicationUserId = user.Id,
                Address = new Address {
                    Street = "101 Nansemond Ave",
                    City = "St. Louis",
                    State = "MO",
                    ZipCode = "63101" }
            });

            context.Squares.Add(new Square
            {
                Title = "Dinner",
                StartTime = new DateTime(2015, 9, 26, 6, 0, 0),
                EndTime = new DateTime(2015, 9, 26, 8, 0, 0),
                Location = "Schlafly Taproom",
                Description = "Serving a chicken entree and a vegetarian entree. $15 at the door."
            });

            context.Squares.Add(new Square
            {
                Title = "Drinks",
                StartTime = new DateTime(2015, 9, 26, 8, 30, 0),
                EndTime = new DateTime(2015, 9, 26, 11, 0, 0),
                Location = "Bobby's Place",
                Description = "Contact me for address or to arrange carpool."
            });

            context.Squares.Add(new Square
            {
                Title = "After Hours House Party",
                StartTime = new DateTime(2015, 9, 26, 11, 30, 0),
                EndTime = new DateTime(2015, 9, 26, 3, 0, 0),
                Location = "My House",
                Description = "Beer and wine provided. BYOL."
            });
        }
    }
}
