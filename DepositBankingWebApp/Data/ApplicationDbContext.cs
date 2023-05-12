using DepositBankingWebApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DepositBankingWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Deposit> Deposits { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Deposit)
                .WithMany(d => d.Comments)
                .HasForeignKey(d => d.DepositId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Deposit>()
                .HasOne(d => d.ApplicationUser)
                .WithMany(u => u.Deposits)
                .HasForeignKey(d => d.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);               

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.ApplicationUser)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.ApplicationUserId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
            base.OnModelCreating(modelBuilder);//works only for Identity(account system that is maintained by dotnet) classes
        }
    }
}