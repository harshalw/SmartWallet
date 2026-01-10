using DockerDeep;
using DockerDeep.Model;
using Microsoft.EntityFrameworkCore;
using SmartWallet.Models;


namespace MyApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        public DbSet<Employee> employee => Set<Employee>();
        public DbSet<User> Users => Set<User>();
        public DbSet<TypeMaster> TypeMasters => Set<TypeMaster>();
        public DbSet<Income> Income => Set<Income>();
        public DbSet<Expense> Expenses => Set<Expense>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(x => x.Username).IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(x => x.Email).IsUnique();
        }
    }
}
