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

        public MenuController(IRepository<AppDetail> appDetailRepo,
                                IRepository<Category> categoryRepo)
        {
            this.appDetailRepo = appDetailRepo;
            this.categoryRepo = categoryRepo;
        }

        public IActionResult Index()
        {
            ProductVM menuVM = new ProductVM(appDetailRepo.GetAll(x => x.Id == 10).ToList()
                                            , categoryRepo.GetAll().Include(x => x.Products).Where(x => x.Products.Any()).ToList());
            return View(menuVM);
        }

        [HttpPost]
        public IActionResult Index(ProductVM productVM)
        {

            ProductVM menuVM = new ProductVM(appDetailRepo.GetAll(x => x.Id == 10).ToList()
                                            , categoryRepo.GetAll().Include(x => x.Products).Where(x => x.Products.Any()).ToList());
            return View(menuVM);
        }
    }
}