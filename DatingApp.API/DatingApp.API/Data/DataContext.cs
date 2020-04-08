using DatingApp.API.Model;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Values> tblValues { get; set; }
        public DbSet<UserModel> tblUser { get; set; }
        public DbSet<Photo> Photos { get; set; }
    }
}
