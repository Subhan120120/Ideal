using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QonaqWebApp.AppCode.Infrastructure;
using QonaqWebApp.Models.Entity;
using QonaqWebApp.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace QonaqWebApp.Controllers
{
    public class MenuController : Controller
    {
        public readonly IRepository<AppDetail> appDetailRepo;
        public readonly IRepository<Category> categoryRepo;
        public readonly IRepository<Product> productRepo;
        public readonly IRepository<Brand> brandRepo;

        public MenuController(IRepository<AppDetail> appDetailRepo,
                                IRepository<Category> categoryRepo,
                                IRepository<Product> productRepo,
                                IRepository<Brand> brandRepo)
        {
            this.appDetailRepo = appDetailRepo;
            this.categoryRepo = categoryRepo;
            this.productRepo = productRepo;
            this.brandRepo = brandRepo;
        }

        public IActionResult Index()
        {
            ProductVM menuVM = new ProductVM(appDetailRepo.GetAll(x => x.Id == 10).ToList()
                                            , categoryRepo.GetAll().Include(x => x.Products).Where(x => x.Products.Any()).ToList()
                                            , brandRepo.GetAll().Where(x => x.Products.Count > 10).OrderByDescending(x => x.Products.Count).ToList());

            menuVM.MaxValue = productRepo.GetAll().Max(x => x.Price);

            return View(menuVM);
        }

        [HttpPost]
        public IActionResult Index(ProductVM productVM)
        {
            var filteredProducts = categoryRepo.GetAll()
                                           .Include(x => x.Products.Where(p => productVM.BrandIds == null ? true : productVM.BrandIds.Contains(p.BrandId) && p.Price >= productVM.MinValue && p.Price <= productVM.MaxValue))
                                           .Where(x => x.Products.Any()).ToList();


            ProductVM menuVM = new ProductVM(appDetailRepo.GetAll(x => x.Id == 10).ToList()
                                            , filteredProducts
                                            , brandRepo.GetAll().Where(x => x.Products.Count > 10).OrderByDescending(x => x.Products.Count).ToList());
            return View(menuVM);
        }

        [HttpGet]
        public IActionResult Detail(string code)
        {
            ViewBag.Category = new SelectList(categoryRepo.GetAll(), "CategoryId", "CategoryName");

            if (code == "")
            {
                return View(new Product());
            }
            else
            {
                var product = productRepo.GetByCode(code);
                string brandName = brandRepo.GetByCode(product.BrandId).BrandName;
                product.Brand.BrandName = brandName;
                if (product == null)
                {
                    return NotFound();
                }
                return View(product);
            }
        }
    }
}