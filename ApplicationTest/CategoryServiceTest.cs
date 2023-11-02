using eShopSolution.Application.Catalog.Categories;
using eShopSolution.Application.Common;
using eShopSolution.Data.EF;
using eShopSolution.Data.Entities;
using eShopSolution.Data.Enums;
using eShopSolution.Utilities.Exceptions;
using eShopSolution.ViewModels.Catalog.Categories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationTest
{
    public class CategoryServiceTest : IDisposable
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
            var categories = new List<Category>
    {
        new Category
        {
            Id = 1,
            SortOrder = 1,
            IsShowOnHome = true,
            ParentId = null,
            Status = Status.Active,
            // Add other properties for the first category
        },
        new Category
        {
            Id = 2,
            SortOrder = 2,
            IsShowOnHome = false,
            ParentId = null,
            Status = Status.Active,
            // Add other properties for the second category
        },
        // Add more test categories as needed
    };
            var categoryTranslations = new List<CategoryTranslation>
    {
        new CategoryTranslation
        {
            Id = 1,
            CategoryId = 1, // Match the category ID you want to associate the translation with
            LanguageId = "en",
            Name = "Category 1 in English",
            SeoDescription = "Description 1 in English",
            SeoTitle = "Title 1 in English",
            SeoAlias = "category-1-english"
        },
        new CategoryTranslation
        {
            Id = 2,
            CategoryId = 2, // Match the category ID you want to associate the translation with
            LanguageId = "en",
            Name = "Category 2 in English",
            SeoDescription = "Description 2 in English",
            SeoTitle = "Title 2 in English",
            SeoAlias = "category-2-english"
        },
        // Add more translations as needed
    };

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
            _context.Categories.AddRange(categories);
            _context.CategoryTranslations.AddRange(categoryTranslations);
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


        [Fact]
        public async Task Delete_ExistingCategory_DeletesCategory()
        {
            // Arrange
            var existingCategoryId = 1; // Replace with a valid category ID that exists in your test data
            var categoryToDelete = new Category { Id = existingCategoryId };

            // Act
            _context.Entry(categoryToDelete).State = EntityState.Detached; // Detach the entity from the context
            var result = await _categoryService.Delete(existingCategoryId);

            // Assert
            Assert.Equal(2, result); // Assuming 1 entity was deleted
            var deletedCategory = await _context.Categories.FindAsync(existingCategoryId);
            Assert.Null(deletedCategory); // The category should not exist in the database anymore
        }

        [Fact]
        public async Task Delete_NonExistingCategory_ThrowsException()
        {
            // Act and Assert
            var nonExistingCategoryId = 999; // Replace with a category ID that does not exist in your test data

            var exception = await Assert.ThrowsAsync<EShopException>(() => _categoryService.Delete(nonExistingCategoryId));
            Assert.Equal($"Cannot find a product: {nonExistingCategoryId}", exception.Message);
        }

        [Fact]
        public async Task GetAll_ReturnsCategoryList()
        {
            // Arrange
            var languageId = "en"; // Replace with a valid language ID for your test data

            // Act
            var result = await _categoryService.GetAll(languageId);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.IsType<List<CategoryVm>>(result);
            Assert.True(result.Any(), "No categories were returned.");
        }


        [Fact]
        public async Task GetById_ExistingCategory_ReturnsCategoryVm()
        {
            // Arrange
            var languageId = "en"; // Replace with a valid language ID for your test data
            var categoryId = 1; // Replace with a valid category ID that exists in your test data

            // Act
            var result = await _categoryService.GetById(languageId, categoryId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CategoryVm>(result);
            Assert.Equal(categoryId, result.Id);
            // Add more assertions for other properties as needed
        }

        [Fact]
        public async Task GetById_NonExistingCategory_ReturnsNull()
        {
            // Arrange
            var languageId = "en"; // Replace with a valid language ID for your test data
            var nonExistingCategoryId = 999; // Replace with a category ID that does not exist in your test data

            // Act
            var result = await _categoryService.GetById(languageId, nonExistingCategoryId);

            // Assert
            Assert.Null(result);
        }

        public void Dispose()
        {
            // Clean up and dispose of the context when the tests are done
            _context.Dispose();
        }
    }
}

