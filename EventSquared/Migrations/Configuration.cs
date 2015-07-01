namespace EventSquared.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using EventSquared.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<EventSquared.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(EventSquared.Models.ApplicationDbContext context)
        {
            var manager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(
                    new ApplicationDbContext()));

            for (int i = 0; i < 4; i++)
            {
                var user = new ApplicationUser()
                {
                    FirstName = string.Format("FirstName{0}", i.ToString()),
                    LastName = string.Format("LastName{0}", i.ToString()),
                    UserName = string.Format("Email{0}@Example.com", i.ToString()),
                    Email = string.Format("Email{0}@Example.com", i.ToString()),

                };
                manager.Create(user, string.Format("Password{0}", i.ToString()));
            }
        }
    }
}
