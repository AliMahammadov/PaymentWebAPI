using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PaymentWebEntity.Entities;

namespace PaymentWebData.DAL
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Balance> Balances { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasOne(u => u.Balance)
                .WithMany(b => b.Users)
                .HasForeignKey(u => u.BalanceId); 


            builder.Entity<Payment>()
                .HasOne(p => p.User)  
                .WithMany(u => u.Payments) 
                .HasForeignKey(p => p.UserId);
        }




    }





}

