using Microsoft.AspNetCore.Mvc.Rendering;
using QonaqWebApp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QonaqWebApp.Models.ViewModel
{
    public class ProductVM
    {
        public ProductVM()
        {
        }

        public ProductVM(List<AppDetail> appDetails)
        {
            this.AppDetails = appDetails;
        }

        public ProductVM(List<AppDetail> appDetails, List<Category> categories)
            : this(appDetails)
        {
            this.Categories = categories;
        }

        public ProductVM(List<AppDetail> appDetails, List<Category> categories, List<Brand> brands)
            : this(appDetails, categories)
        {
            this.Brands = brands;
        }

        public List<AppDetail> AppDetails { get; set; }
        public List<Category> Categories { get; set; }
        public List<Brand> Brands { get; set; }
        public string[] BrandIds { get; set; }
        public decimal MaxValue { get; set; }
        public decimal MinValue { get; set; }
    }
}