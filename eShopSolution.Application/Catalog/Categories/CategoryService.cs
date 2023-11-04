using eShopSolution.Data.EF;
using eShopSolution.ViewModels.Catalog.Categories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using eShopSolution.Data.Entities;
using eShopSolution.Utilities.Constants;
using eShopSolution.Data.Enums;
using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.Utilities.Exceptions;
using eShopSolution.Application.Common;

namespace eShopSolution.Application.Catalog.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly EShopDbContext _context;
        private readonly IStorageService _storageService;

        public CategoryService(EShopDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;   
        }

        public async Task<int> Create(CategoryCreateRequest request)
        {
            var languages = _context.Languages;
            var translations = new List<CategoryTranslation>();
            foreach (var language in languages)
            {
                if (language.Id == request.LanguageId)
                {
                    translations.Add(new CategoryTranslation()
                    {
                        Name = request.Name,
                        SeoDescription = request.SeoDescription,
                        SeoTitle = request.SeoTitle,
                        SeoAlias = request.SeoAlias,
                        LanguageId = request.LanguageId
                    });
                }
              /*  else
                {
                    translations.Add(new CategoryTranslation()
                    {
                        Name = SystemConstants.ProductConstants.NA,
                        SeoDescription = SystemConstants.ProductConstants.NA,
                        SeoAlias = SystemConstants.ProductConstants.NA,
                        SeoTitle= SystemConstants.ProductConstants.NA,
                        LanguageId = language.Id
                    });
                }*/
            }
            var category = new Category()
            {
                SortOrder = request.SortOrder,
                IsShowOnHome = request.IsShowOnHome,
                Status = request.Status,
                CategoryTranslations = translations

            };
           
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category.Id;
        }

        public async Task<int> Update(CategoryUpdateRequest request)
        {
            {
                var category = await _context.Categories.FindAsync(request.Id);
                var categoryTranslations = await _context.CategoryTranslations.FirstOrDefaultAsync(x => x.CategoryId == request.Id
                && x.LanguageId == request.LanguageId);
                


                if (category == null || categoryTranslations == null) throw new EShopException($"Cannot find a category with id: {request.Id}");

                categoryTranslations.Name = request.Name;
                categoryTranslations.SeoAlias = request.SeoAlias;
                categoryTranslations.SeoDescription = request.SeoDescription;
                categoryTranslations.SeoTitle = request.SeoTitle;
                categoryTranslations.LanguageId = request.LanguageId;
                category.SortOrder = request.SortOrder;
                category.Status = request.Status;
                category.IsShowOnHome = request.IsShowOnHome;
                return await _context.SaveChangesAsync();
            }
        }

        public async Task<int> Delete(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null) throw new EShopException($"Cannot find a product: {categoryId}");

            _context.Categories.Remove(category);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<CategoryVm>> GetAll(string languageId)
        {
            var query = from c in _context.Categories
                        join ct in _context.CategoryTranslations on c.Id equals ct.CategoryId
                        where ct.LanguageId == languageId
                        select new { c, ct };
            return await query.Select(x => new CategoryVm()
            {
                Id = x.c.Id,
                Name = x.ct.Name,
                ParentId = x.c.ParentId
            }).ToListAsync();
        }

        public async Task<CategoryVm> GetById(string languageId, int id)
        {
            var query = from c in _context.Categories
                        join ct in _context.CategoryTranslations on c.Id equals ct.CategoryId
                        where ct.LanguageId == languageId && c.Id == id
                        select new { c, ct };
            return await query.Select(x => new CategoryVm()
            {
                Id = x.c.Id,
                Name = x.ct.Name,
                ParentId = x.c.ParentId
            }).FirstOrDefaultAsync();
        }

        public async Task<PagedResult<CategoryVm>> GetAllPaging(GetCategoryPagingRequest request)
        {

            //1. Select join
            var query = from c in _context.Categories
                        join ct in _context.CategoryTranslations on c.Id equals ct.CategoryId
                        where ct.LanguageId == request.LanguageId
                        select new CategoryVm()
                        {
                            Id = c.Id,
                            Name = ct.Name,
                            ParentId = c.ParentId
                        };

            //2. filter
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.Name.Contains(request.Keyword));

            // Create a list to store distinct products
            List<CategoryVm> distinctCategory = new List<CategoryVm>();

            foreach (var categoryVm in query)
            {
                // Check if the product with the same ID is already in the distinctProducts list
                if (!distinctCategory.Any(p => p.Id == categoryVm.Id) && !(categoryVm.Name == "N/A"))
                {
                    distinctCategory.Add(categoryVm);
                }
            }
            int totalRow = distinctCategory.Count();

            var data = distinctCategory
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var pagedResult = new PagedResult<CategoryVm>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };
            return pagedResult;
        }
    }
}