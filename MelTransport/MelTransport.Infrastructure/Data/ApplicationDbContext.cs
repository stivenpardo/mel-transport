using MelTransport.Domain.Entities;
using MelTransport.Domain.UserManage;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MelTransport.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Bill> Bill { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Package> Package { get; set; }
        public DbSet<Transporter> Transporter { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Vehicle> Vehicle { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the sequence
            modelBuilder.HasSequence<int>("OrderNumberSequence")
                .StartsAt(1)
                .IncrementsBy(1);

            modelBuilder.HasSequence<int>("BillNumberSequence")
                .StartsAt(1)
                .IncrementsBy(1);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
