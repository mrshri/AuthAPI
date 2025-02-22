using CouponWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace CouponWeb.DATA
{
    public class CouponContext : DbContext
    {

        public CouponContext(DbContextOptions<CouponContext> options) : base(options)
        {
        }

        public DbSet<Coupon> Coupons { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(
                new Coupon
                {
                    CouponId = 1,
                    CouponCode = "10OFF",
                    MinAmount = 20,
                    DiscountAmount = 10
                },
                new Coupon
                {
                    CouponId = 2,
                    CouponCode = "20OFF",
                    MinAmount = 30,
                    DiscountAmount = 20
                }
            );

        }
    }

    }
