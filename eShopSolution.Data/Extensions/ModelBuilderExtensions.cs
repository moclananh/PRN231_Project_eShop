using eShopSolution.Data.Entities;
using eShopSolution.Data.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.Extensions
{
    // mockdata
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppConfig>().HasData(
               new AppConfig() { Key = "HomeTitle", Value = "This is home page of eShopSolution" },
               new AppConfig() { Key = "HomeKeyword", Value = "This is keyword of eShopSolution" },
               new AppConfig() { Key = "HomeDescription", Value = "This is description of eShopSolution" }
               );
            modelBuilder.Entity<Language>().HasData(
                new Language() { Id = "vi", Name = "Tiếng Việt", IsDefault = true },
                new Language() { Id = "en", Name = "English", IsDefault = false });

            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    Id = 1,
                    IsShowOnHome = true,
                    ParentId = null,
                    SortOrder = 1,
                    Status = Status.Active,
                },
                 new Category()
                 {
                     Id = 2,
                     IsShowOnHome = true,
                     ParentId = null,
                     SortOrder = 2,
                     Status = Status.Active
                 },
                 new Category()
                 {
                     Id = 3,
                     IsShowOnHome = true,
                     ParentId = null,
                     SortOrder = 3,
                     Status = Status.Active
                 }
                 , new Category()
                {
                    Id = 4,
                    IsShowOnHome = true,
                    ParentId = null,
                    SortOrder = 4,
                    Status = Status.Active
                });

            modelBuilder.Entity<CategoryTranslation>().HasData(
                  new CategoryTranslation() { Id = 1, CategoryId = 1, Name = "Love Flowers", LanguageId = "en", SeoAlias = "love-flowers", SeoDescription = "The Love Flower, also known by its scientific name Amorisia caritatis, is a mesmerizing botanical wonder that symbolizes the essence of love and affection.", SeoTitle = "Hoa Tinh Yeu" },
                  new CategoryTranslation() { Id = 2, CategoryId = 2, Name = "Birthday Flowers", LanguageId = "en", SeoAlias = "birthday-flowers", SeoDescription = "The Birthday Flower, known scientifically as Festivus celebratus, is a delightful and vibrant botanical treasure that embodies the joy and celebration of a special day.", SeoTitle = "Hoa Sinh Nhat" },
                  new CategoryTranslation() { Id = 3, CategoryId = 3, Name = "Congratulation Flowers", LanguageId = "en", SeoAlias = "congratulation-flowers", SeoDescription = "The Congratulation Flower, scientifically known as Florens triumphi, is a magnificent and awe-inspiring bloom that embodies the spirit of celebration, achievement, and triumph.", SeoTitle = "Hoa Chuc Mung" },
                  new CategoryTranslation() { Id = 4, CategoryId = 4, Name = "Condolence Flowers", LanguageId = "en", SeoAlias = "condolence-flowers", SeoDescription = "Condolence flowers hold a special place in expressing sympathy and offering solace during times of grief and loss. ", SeoTitle = "Hoa chia buon" }
                    );

            modelBuilder.Entity<Product>().HasData(
           new Product()
           {
               Id = 1,
               DateCreated = DateTime.Now,
               OriginalPrice = 100000,
               Price = 200000,
               Stock = 0,
               ViewCount = 0,
           });
            modelBuilder.Entity<ProductTranslation>().HasData(
                 new ProductTranslation()
                 {
                     Id = 1,
                     ProductId = 1,
                     Name = "Hoa Hong Do",
                     LanguageId = "vi",
                     SeoAlias = "hoa-hong-do",
                     SeoDescription = "Hoa hong do",
                     SeoTitle = "Hoa hong do",
                     Details = "Hoa hong do detail",
                     Description = "Giữa cuộc sống với bao nhiêu khó khăn, căng thẳng và tấp nập, một chút nhẹ nhàng, hồn nhiên của đóa hồng đỏ chắc chắn sẽ mang đến niềm vui khó tả cho một ai đó. Sống chậm lại và yêu thương nhiều hơn, chia sẻ nhiều hơn và quan tâm nhiều hơn, hãy để bó hồng này giúp bạn gửi đi thông điệp đó nhé."
                 },
                    new ProductTranslation()
                    {
                        Id = 2,
                        ProductId = 1,
                        Name = "Sunflower",
                        LanguageId = "en",
                        SeoAlias = "sun-flower",
                        SeoDescription = "sun-flower",
                        SeoTitle = "sun-flower",
                        Details = "sun-flower",
                        Description = "Made from the freshest sunflowers and adorned with branches of white calimero, the flower basket shines brightly and full of life, as lucky as the sun on a summer day. This will be a very suitable gift if you want to make an impression on your partners, colleagues, friends, or the people you love and care about the most."
                    });
            modelBuilder.Entity<ProductInCategory>().HasData(
                new ProductInCategory() { ProductId = 1, CategoryId = 1 }
                );

            // any guid
            var roleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC");
            var adminId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");
            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = roleId,
                Name = "admin",
                NormalizedName = "admin",
                Description = "Administrator role"
            });

            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "admin@admin.com",
                NormalizedEmail = "admin@admin.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "admin"),
                SecurityStamp = string.Empty,
                FirstName = "Admin",
                LastName = "minator",
                Dob = new DateTime(2023, 01, 01)
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = adminId
            });

            modelBuilder.Entity<Slide>().HasData(
              new Slide() { Id = 1, Name = "Second Thumbnail label", Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", SortOrder = 1, Url = "#", Image = "/themes/images/carousel/1.png", Status = Status.Active },
              new Slide() { Id = 2, Name = "Second Thumbnail label", Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", SortOrder = 2, Url = "#", Image = "/themes/images/carousel/2.png", Status = Status.Active },
              new Slide() { Id = 3, Name = "Second Thumbnail label", Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", SortOrder = 3, Url = "#", Image = "/themes/images/carousel/3.png", Status = Status.Active },
              new Slide() { Id = 4, Name = "Second Thumbnail label", Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", SortOrder = 4, Url = "#", Image = "/themes/images/carousel/4.png", Status = Status.Active },
              new Slide() { Id = 5, Name = "Second Thumbnail label", Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", SortOrder = 5, Url = "#", Image = "/themes/images/carousel/5.png", Status = Status.Active },
              new Slide() { Id = 6, Name = "Second Thumbnail label", Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", SortOrder = 6, Url = "#", Image = "/themes/images/carousel/6.png", Status = Status.Active }
              );
        }
    }
}