#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using targetchatserver.Models;

namespace targetchatserver.Data
{
    public class targetchatserverContext : DbContext
    {
        public targetchatserverContext (DbContextOptions<targetchatserverContext> options)
            : base(options)
        {
        }

        public DbSet<targetchatserver.Models.UserModel> UserModel { get; set; }

        public DbSet<targetchatserver.Models.Contact> Contact { get; set; }

        public DbSet<targetchatserver.Models.Message> Message { get; set; }
    }
}
