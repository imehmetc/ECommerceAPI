using ECommerceAPI.Domain.Entities.Concrete;
using ECommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Context
{
    public class ECommerceDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        // DbSets..
        // DbSet user eklenmeyecek.
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<HelpCenter> HelpCenters { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<UserComment> UserComments { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Cart> Carts { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<HelpCenter>().HasData(
                  new HelpCenter
                  {
                      Id = Guid.NewGuid(),
                      PopularQuestion = "How can I return my order?",
                      PopularAnswer = "If you’re not happy with your order, you have 30 days to send us back any items.<br>\r\n To make a return, simply create a return label (check ''How do I get a return label?'' for more details) for each delivery you received and pack the items you are returning. Make sure to cover up or remove the original delivery bar code on the box and add the right labels for the respective items and package.<br>\r\nDone! You can drop off your return at a parcel shop. Don’t forget to keep the return receipt from the courier until your refund has been processed.<br>\r\n We will process your return and the total amount will be reimbursed to you through the same payment method that you used for the purchase. Please keep in mind that the time to refund may vary according to your payment method."
                  });

            builder.Entity<Category>()
                .HasMany(c => c.ChildCategories)
                .WithOne() // ParentCategory'yı kaldırdığınız için ilişkiyi burada belirtmiyoruz
                .HasForeignKey(c => c.ParentCategoryId);

            builder.Entity<Contact>().HasData(
                  new Contact { Id = Guid.NewGuid(), Address = "Ankara/Çankaya Türkiye", Title = "Title of ECommerce Site", Phone = "(+90) 530 000 00 00", Email = "boostAnk17@ecommerce.com" });
            builder.Entity<AboutUs>().HasData(
                  new AboutUs { Id = Guid.NewGuid(), BusinessInfo = "Our mission is to have a positive impact by offering a seamless e-commerce experience to customers and sellers.\r\n\r\n\r\nThe trust of around 30 million customers and 250,000 sellers propelled us to become the first decacorn in Türkiye and one of the top e-commerce platforms in the world.", WhatWeDo = "Our goal is to make fashion accessible for everyone, where trends meet affordability." });


            builder.Entity<Product>().HasMany(x => x.CartItems).WithOne(x => x.Product).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Product>().HasMany(x => x.UserComments).WithOne(x => x.Product).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<AppUser>().HasMany(x => x.UserComments).WithOne(x => x.AppUser).HasForeignKey(x => x.AppUserId).OnDelete(DeleteBehavior.Restrict);

            // SellerReply ve AppUser arasındaki ilişkiler
            builder.Entity<SellerReply>()
                .HasOne(sr => sr.AppUser)
                .WithMany(u => u.RepliesAsUser)
                .HasForeignKey(sr => sr.AppUserId)
                .OnDelete(DeleteBehavior.Restrict); // veya Cascade, SetNull vb.

            builder.Entity<SellerReply>()
                .HasOne(sr => sr.Seller)
                .WithMany(u => u.RepliesAsSeller)
                .HasForeignKey(sr => sr.SellerId)
                .OnDelete(DeleteBehavior.Restrict); // veya Cascade, SetNull vb.

            builder.Entity<CartItem>()
                    .HasOne(ci => ci.Cart)
                    .WithMany(c => c.CartItems)
                    .HasForeignKey(ci => ci.CartId)
                    .OnDelete(DeleteBehavior.Restrict); // Veya DeleteBehavior.SetNull

            builder.Entity<Cart>()
                    .HasOne(c => c.Customer)
                    .WithMany(c => c.Carts)
                    .HasForeignKey(c => c.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict); // Veya DeleteBehavior.SetNull
            builder.Entity<CartItem>()
                    .HasOne(c => c.Customer)
                    .WithMany(c => c.CartItems)
                    .HasForeignKey(c => c.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Cart>()
                    .HasOne(c => c.Order)
                    .WithOne(c => c.Cart)
                    .HasForeignKey<Order>(c => c.CartId)
                    .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Address>()
                    .HasOne(c => c.AppUser)
                    .WithMany(c => c.Addresses)
                    .HasForeignKey(c => c.AppUserId)
                    .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Order>()
                    .HasOne(c => c.Customer)
                    .WithMany(c => c.Orders)
                    .HasForeignKey(c => c.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict); // Veya DeleteBehavior.SetNull
            builder.Entity<Order>()
                    .HasOne(c => c.Address)
                    .WithMany(c => c.Orders)
                    .HasForeignKey(c => c.AddressId)
                    .OnDelete(DeleteBehavior.Restrict); // Veya DeleteBehavior.SetNull

        }
    }
}
