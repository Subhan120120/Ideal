﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QonaqWebApp.AppCode.Infrastructure;
using QonaqWebApp.Models.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace QonaqWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenuController : Controller
    {
        public readonly IRepository<Product> productRepo;
        public readonly IRepository<Category> categoryRepo;

        public MenuController(IRepository<Product> productRepo,
                                IRepository<Category> categoryRepo)
        {
            this.productRepo = productRepo;
            this.categoryRepo = categoryRepo;
        }


        public IActionResult Menu()
        {
            IList<Product> melumat = productRepo.GetAll().Include(x => x.Category).ToList();
            return View(melumat);
        }

        [HttpGet]
        public IActionResult AddOrEdit(string id = "")
        {
            ViewBag.Category = new SelectList(categoryRepo.GetAll(), "CategoryId", "CategoryName");
            if (id == "")
            {
                return View(new Product());
            }
            else
            {
                var product = productRepo.GetByCode(id);
                if (product == null)
                {
                    return NotFound();
                }
                return View(product);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddOrEdit(string id, Product product, IFormFile ImagePath)
        {
            ViewBag.Category = new SelectList(categoryRepo.GetAll(), "Id", "CategoryName");

            if (ModelState.IsValid)
            {

                if (ImagePath != null)
                {
                    string randomFileName = Path.Combine("wwwroot", "Uploads", "Images", $"{Guid.NewGuid().ToString()}{Path.GetExtension(ImagePath.FileName)}");
                    using (var stream = new FileStream(randomFileName, FileMode.Create))
                        ImagePath.CopyTo(stream);
                    product.ImagePath = Path.GetFileName(randomFileName);
                }


                if (string.IsNullOrEmpty(id))
                {
                    productRepo.Add(product);
                    productRepo.SaveChanges();
                }
                else
                {
                    try
                    {
                        if (ImagePath != null)
                            productRepo.GetByCode(id).ImagePath = product.ImagePath;
                        productRepo.GetByCode(id).ProductDesc = product.ProductDesc;
                        productRepo.GetByCode(id).CategoryId = product.CategoryId;
                        productRepo.GetByCode(id).ProductName = product.ProductName;
                        productRepo.GetByCode(id).Price = product.Price;
                        productRepo.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                    }
                }

                Product AddedRow = productRepo.GetAll(x => x.ProductId == product.ProductId).Include(x => x.Category).FirstOrDefault();

                //string jsonResult = JsonConvert.SerializeObject(melumat, Formatting.Indented, options);


                return Json(new { isValid = true, product = AddedRow }, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            }
            return Json(new { isValid = false });
        }

        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string[] selectRowsArr)
        {
            foreach (string item in selectRowsArr)
            {
                Product MenuRow = productRepo.GetByCode(item);
                productRepo.Delete(MenuRow);
                var pathImage = Path.Combine("wwwroot", "Uploads", "Images", MenuRow.ImagePath);
                if (System.IO.File.Exists(pathImage))
                    System.IO.File.Delete(pathImage);
            }
            int affectedRow = productRepo.SaveChanges();

            if (affectedRow != 0)
                return Json(new { isValid = true });
            else
                return Json(new { isValid = false });
        }

        public IActionResult CategoryList()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CategoryList(Category category)
        {
            categoryRepo.Add(category);
            categoryRepo.SaveChanges();
            return RedirectToAction("Menu");
        }

    }
}