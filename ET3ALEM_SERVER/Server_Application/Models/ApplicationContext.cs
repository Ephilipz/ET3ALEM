using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server_Application.Models
{
    public class ApplicationContext: IdentityDbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) :base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Test>().ToTable("Test");
            modelBuilder.Entity<Question>().ToTable("Question");
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Question> Questions { get; set; }
    }
}
