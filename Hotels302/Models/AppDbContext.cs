using Hotels302.Migrations;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Hotels302.Models
{
    public class AppDbContext: IdentityDbContext<ApplicationUser>
    {
        public AppDbContext() : base("Hotel302")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AppDbContext, Configuration>("Hotel302"));
        }

        //public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<City> Cities  { get; set; }
        public DbSet<Contact> Contacts  { get; set; }
        public DbSet<Hotel> Hotels  { get; set; }
        public DbSet<Reservation> Reservations  { get; set; }
        public DbSet<Room> Rooms  { get; set; }
        public DbSet<RoomType> RoomTypes  { get; set; }
        public DbSet<Town> Towns  { get; set; }
        public static AppDbContext Create()
        {
            return new AppDbContext();
        }

    }
}