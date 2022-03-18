using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QonaqWebApp.AppCode.Infrastructure;
using QonaqWebApp.Models.Entity;
using QonaqWebApp.Models.ViewModel;
using System.Linq;

namespace QonaqWebApp.Controllers
{
    public class MenuController : Controller
    {
        public readonly IRepository<AppDetail> appDetailRepo;
        public readonly IRepository<Category> categoryRepo;
        public readonly IRepository<Brand> brandRepo;

        public MenuController(IRepository<AppDetail> appDetailRepo,
                                IRepository<Category> categoryRepo,
                                IRepository<Brand> brandRepo)
        {
            this.appDetailRepo = appDetailRepo;
            this.categoryRepo = categoryRepo;
            this.brandRepo = brandRepo;
        }

        public IActionResult Index()
        {
            ProductVM menuVM = new ProductVM(appDetailRepo.GetAll(x => x.Id == 10).ToList()
                                            , categoryRepo.GetAll().Include(x => x.Products).Where(x => x.Products.Any()).ToList()
                                            , brandRepo.GetAll().Include(x => x.Products).Where(x => x.Products.Count > 10).OrderByDescending(x => x.Products.Count).ToList());
            return View(menuVM);
        }

        [HttpPost]
        public IActionResult Index(ProductVM productVM)
        {
            var asda = brandRepo.GetAll().Where(x => productVM.BrandIds.Contains(x.BrandId)).ToList();

            ProductVM menuVM = new ProductVM(appDetailRepo.GetAll(x => x.Id == 10).ToList()
                                            , categoryRepo.GetAll().Include(x => x.Products.Where(p => productVM.BrandIds.Contains(p.BrandId))).Where(x => x.Products.Any()).ToList()
                                            , brandRepo.GetAll().Include(x => x.Products).Where(x => x.Products.Count > 10).OrderByDescending(x => x.Products.Count).ToList());
            return View(menuVM);
        }
    }
}
