using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TargetChatServer11.Models;

namespace TargetChatServer11.Data
{
    public class TargetChatServer11Context : DbContext
    {
        public TargetChatServer11Context(DbContextOptions<TargetChatServer11Context> options)
            : base(options)
        {
        }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        //  {
        //      modelBuilder.Entity<Contact>()
        //                .HasKey("id", "User");
        //   }

        public DbSet<TargetChatServer11.Models.UserModel> UserModel { get; set; }

        public DbSet<TargetChatServer11.Models.Contact> Contact { get; set; }

        public DbSet<TargetChatServer11.Models.Message> Message { get; set; }

        public DbSet<TargetChatServer11.Models.AndroidDeviceIDModel> AndroidDeviceIDModel { get; set; }
    }
}
