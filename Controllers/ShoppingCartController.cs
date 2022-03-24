using Microsoft.AspNetCore.Mvc;
using QonaqWebApp.AppCode.Helpers;
using QonaqWebApp.AppCode.Infrastructure;
using QonaqWebApp.Models.Entity;
using QonaqWebApp.Models.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace QonaqWebApp.Controllers
{
    public class ShoppingCartController : Controller
    {
        public readonly IRepository<AppDetail> appDetailRepo;
        public readonly IRepository<Product> productRepo;
        public readonly IRepository<Order> orderRepo;
        public readonly IRepository<Customer> customerRepo;

        public ShoppingCartController(IRepository<AppDetail> appDetailRepo,
                                      IRepository<Product> productRepo,
                                      IRepository<Customer> customerRepo,
                                      IRepository<Order> orderRepo)
        {
            this.appDetailRepo = appDetailRepo;
            this.productRepo = productRepo;
            this.orderRepo = orderRepo;
            this.customerRepo = customerRepo;
        }

        public IActionResult Index()
        {
            List<Order> cart = SessionHelper.GetObjectFromJson<List<Order>>(HttpContext.Session, "cart");
            if (cart != null)
                ViewBag.total = cart.Sum(item => item.Product.Price * item.Quantity);

            ShoppingVM shoppingVM = new ShoppingVM(null, cart);

            //TempData.Keep("Success");
            //TempData.Keep("Error");

            return View(shoppingVM);
        }

        [TempData]
        public string Success { get; set; }
        [TempData]
        public string Error { get; set; }

        public IActionResult Buy(string id)
        {
            if (SessionHelper.GetObjectFromJson<List<Order>>(HttpContext.Session, "cart") == null)
            {
                List<Order> cart = new List<Order>();
                cart.Add(new Order { Product = productRepo.GetByCode(id), Quantity = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<Order> cart = SessionHelper.GetObjectFromJson<List<Order>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (isExist(id) != -1)
                    cart[index].Quantity++;
                else
                    cart.Add(new Order { Product = productRepo.GetByCode(id), Quantity = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return NoContent();
        }

        public IActionResult Remove(string id)
        {
            List<Order> cart = SessionHelper.GetObjectFromJson<List<Order>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }

        private int isExist(string id)
        {
            List<Order> cart = SessionHelper.GetObjectFromJson<List<Order>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Product.ProductId.Equals(id))
                    return i;
            return -1;
        }

        public IActionResult ConfirmCart(ShoppingVM shoppingVM)
        {
            List<Order> cart = SessionHelper.GetObjectFromJson<List<Order>>(HttpContext.Session, "cart");
            shoppingVM.Orders = cart;

            if (shoppingVM.Customer != null)
            {
                customerRepo.Add(shoppingVM.Customer);
                customerRepo.SaveChanges();

                if (shoppingVM.Orders != null)
                {
                    for (int i = 0; i < shoppingVM.Orders.Count; i++)
                    {
                        shoppingVM.Orders[i].CustomerId = shoppingVM.Customer.Id;
                        shoppingVM.Orders[i].ProductId = shoppingVM.Orders[i].Product.ProductId;
                        shoppingVM.Orders[i].Product = null;
                    }
                    orderRepo.AddRange(shoppingVM.Orders);
                    int rowAffected = orderRepo.SaveChanges();
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", null);

                    if (rowAffected > 0)
                        TempData["Success"] = "Sifariş Qeydə alındı";
                    else
                        TempData["Error"] = "Sifariş Qeydə alınmadı";  
                }
                else
                    TempData["Error"] = "Sifariş üçün heç bir məhsul seçilməyib";
            }               
            return RedirectToAction("Index");
        }

    }
}
