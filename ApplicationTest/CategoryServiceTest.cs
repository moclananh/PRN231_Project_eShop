using eShopSolution.Application.Catalog.Categories;
using eShopSolution.Application.Common;
using eShopSolution.Data.EF;
using eShopSolution.Data.Entities;
using eShopSolution.ViewModels.Catalog.Categories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationTest
{
    public class CategoryServiceTest
    {
        private EShopDbContext _context;
        private ICategoryService _categoryService;
        private IStorageService _storageService;

        public CategoryServiceTest()
        {
            // Initialize a new context with the in-memory database for each test
            var options = new DbContextOptionsBuilder<EShopDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Use a unique name for each test
                .Options;

            _context = new EShopDbContext(options);
            SeedInMemoryDatabase(); // Populate the database with test data
            _categoryService = new CategoryService(_context, _storageService);
        }

        private void SeedInMemoryDatabase()
        {
            // Add seed data for your Categories and any other required data
            var languages = new List<Language>
        {
            new Language
            {
                Id = "en",
                Name = "English",
                // Add other language properties
            },
            new Language
            {
                Id = "fr",
                Name = "French",// Use a different language ID
                // Add other language properties
            },
            // Add more languages as needed
        };

            _context.Languages.AddRange(languages);
            _context.SaveChanges();
        }

        [Fact]
        public async Task Create_ValidCategory_ReturnsCategoryId()
        {
            // Arrange
            var request = new CategoryCreateRequest
            {
                Name = "TestCategory",
                SeoDescription = "Test SEO Description",
                SeoTitle = "Test SEO Title",
                SeoAlias = "test-category",
                LanguageId = "en", // Replace with a valid language ID in your test data
                SortOrder = 1,
                IsShowOnHome = true,
                Status = eShopSolution.Data.Enums.Status.Active
            };

            // Act
            var categoryId = await _categoryService.Create(request);

            // Assert
            Assert.True(categoryId > 0); // Check if a valid category ID is returned
        }

        [Fact]
        public async Task Create_CategoryWithNonExistentLanguage_ReturnsCategoryId()
        {
            // Arrange
            var request = new CategoryCreateRequest
            {
                Name = "TestCategory",
                SeoDescription = "Test SEO Description",
                SeoTitle = "Test SEO Title",
                SeoAlias = "test-category",
                LanguageId = "de", // Use a different language ID that does not exist in your test data
                SortOrder = 1,
                IsShowOnHome = true,
                Status = eShopSolution.Data.Enums.Status.Active
            };

            // Act
            var categoryId = await _categoryService.Create(request);

            // Assert
            Assert.True(categoryId > 0); // Check if a valid category ID is returned
        }

        public void Dispose()
        {
            // Clean up and dispose of the context when the tests are done
            _context.Dispose();
        }
    }
}

