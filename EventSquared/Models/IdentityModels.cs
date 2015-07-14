using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;
using System;

namespace EventSquared.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? AddressId { get; set; }
        public virtual Address Address { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        public virtual ICollection<Square> Squares { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            return userIdentity;
        }
    }

    public class allViewModel
    {
        public IEnumerable<Event> yourEvents { get; set; }
        public IEnumerable<Event> subscribedEvents { get; set; }
        public IEnumerable<Square> allSquares { get; set; }
        public IEnumerable<Square> yourSquares { get; set; }
    }

    public class Event
    {
        public int Id { get; set; }

        public string Title { get; set; }

        [DisplayFormat(DataFormatString = "{0:dddd, MMMM d, yyyy}",
            ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        public string Description { get; set; }

        public int? AddressId { get; set; }
        public virtual Address Address { get; set; }

        public virtual ICollection<Square> Squares { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }

    public class Address
    {
        public int Id { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }
    }

    public class Square
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:M/d/yyyy HH:mm tt}", ApplyFormatInEditMode = true)]   
        public DateTime CurrentTime { get; set; }

        public string Title { get; set; }

        [DisplayFormat(DataFormatString = "{0:HH:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }

        [DisplayFormat(DataFormatString = "{0:HH:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime EndTime { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public int EventId { get; set; }
        public virtual Event Event { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Event> Events { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Square> Squares { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<EventSquared.Models.newEventViewModel> newEventViewModels { get; set; }

        public System.Data.Entity.DbSet<EventSquared.Models.detailsEventViewModel> detailsEventViewModels { get; set; }
    }
}