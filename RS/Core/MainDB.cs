using RS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RS.Core
{
    public class MainDB : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<UsersRoles> UsersRoles { get; set; }
        public DbSet<Trainings> Trainings { get; set; }
        public DbSet<TrainingUsers> TrainingUsers { get; set; }
        public DbSet<Permanents> Permanents { get; set; }

        public MainDB() 
            : base("MainDB")
        {
            
        }
    }
}