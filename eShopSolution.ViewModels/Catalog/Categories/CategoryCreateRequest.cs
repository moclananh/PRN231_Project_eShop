using eShopSolution.Data.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.ViewModels.Catalog.Categories
{
    public class CategoryCreateRequest
    {
        public int SortOrder { set; get; }
        public bool IsShowOnHome { set; get; }
        public Status Status { set; get; }
        [Required(ErrorMessage = "Bạn phải nhập tên category")]
        public string Name { set; get; }
        public string SeoDescription { set; get; }
        public string SeoTitle { set; get; }
        public string SeoAlias { set; get; }
        public string LanguageId { set; get; }

        public bool? IsFeatured { get; set; }

    }
}
