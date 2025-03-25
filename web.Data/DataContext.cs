using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.Core.models;
using web.Core.interfaces;


namespace web.Data
{
    public class DataContext:DbContext,IDataContext
    {
        public DbSet<User> UserList { get; set; }
        public DbSet<Creation> CreationList { get; set; }
        public DbSet<Challenge> ChallengeList { get; set; }
        public DbSet<Vote> VotesList { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=my_db");
        }
    }
}
