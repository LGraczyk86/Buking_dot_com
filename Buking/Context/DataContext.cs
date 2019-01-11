using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using Buking.Models.Database;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Buking.Context
{
    public class DataContext : IdentityDbContext
    {
        public DbSet<GuestHouse> GuestHouses { get; set; }

        public DbSet<Booked> Bookeds { get; set; }

        public DataContext() : base("DatabaseContext")
        {
        }
    }
}