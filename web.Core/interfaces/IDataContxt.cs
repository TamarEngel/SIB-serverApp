using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using web.Core.models;
using Microsoft.EntityFrameworkCore;


namespace web.Core.interfaces
{
    public interface IDataContext
    {
        public DbSet<User> UserList { get; set; }
        public DbSet<Creation> CreationList { get; set; }
        public DbSet<Challenge> ChallengeList { get; set; }

    }
}
