
using eShopSolution.Application.System.Languages;
using eShopSolution.Data.EF;
using eShopSolution.Data.Entities;
using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Languages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationTest
{
    public class LanguageServiceTests
    {
        private EShopDbContext _context;
        private ILanguageService _languageService;
        private readonly IConfiguration _config;
        public LanguageServiceTests()
        {
            // Initialize a new context with the in-memory database for each test
            var options = new DbContextOptionsBuilder<EShopDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Use a unique name for each test
                .Options;

            _context = new EShopDbContext(options);
            SeedInMemoryDatabase(); // Populate the database with test data
            _languageService = new LanguageService(_context, _config);
        }

        private void SeedInMemoryDatabase()
        {
            var languages = new List<Language>
        {
            new Language
            {
                Id = "en",
                Name = "English",
                IsDefault = true
            },
            new Language
            {
                Id = "fr",
                Name = "French",
                IsDefault = false
            },
            // Add more test languages as needed
        };

            _context.Languages.AddRange(languages);
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetAll_ReturnsAllLanguages()
        {
            // Act
            var result = await _languageService.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ApiSuccessResult<List<LanguageVm>>>(result);
            var data = result.ResultObj.Count();
            Assert.NotNull(data);
            Assert.Equal(2, data);
           
        }

        public void Dispose()
        {
            // Clean up and dispose of the context when the tests are done
            _context.Dispose();
        }
    }
}
