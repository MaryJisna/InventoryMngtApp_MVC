using InventoryMngtApp_MVC.Models.Repository;
using InventoryMngtApp_MVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryMngtApp_MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductRepository repository = new ProductRepository();

        // Action to show all products
        public ActionResult Index()
        {
            var products = repository.GetAllProducts();
            return View(products);
        }

        // Action to create a new product
        public ActionResult Create()
        {
            return View();
        }

        // POST: Action to save the new product
        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                string fileName = Path.GetFileName(file.FileName);
                string path = Path.Combine(Server.MapPath("~/Images"), fileName);
                file.SaveAs(path);
                product.ProductImage = fileName;
            }

            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }
    }

}