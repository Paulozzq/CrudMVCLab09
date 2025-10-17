using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CrudMVC.Models; 

namespace CrudMVC.Controllers
{
    public class ProductsController : Controller
    {
        private List<ProductModel> GetProductsFromSession()
        {
            if (Session["Products"] == null)
            {
                var products = new List<ProductModel>
                {
                    new ProductModel { Id = 1, Name = "Producto A", Price = 100, Description = "Descripcion A", DateExpiration = DateTime.Today.AddMonths(1), Active = true },
                    new ProductModel { Id = 2, Name = "Producto B", Price = 200, Description = "Descripcion B", DateExpiration = DateTime.Today.AddMonths(2), Active = true },

                };
                Session["Products"] = products;
            }
            return (List<ProductModel>)Session["Products"];
        }

        // GET: Products
        public ActionResult Index()
        {
            var products = GetProductsFromSession().Where(p => p.Active).ToList();
            return View(products);
        }

        public ActionResult Details(int id)
        {
            var products = GetProductsFromSession();
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null) return HttpNotFound();
            return View(product);
        }


        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        public ActionResult Create(ProductModel model)
        {
            try
            {
                var products = GetProductsFromSession();
                model.Id = products.Count > 0 ? products.Max(p => p.Id) + 1 : 1;
                products.Add(model);
                Session["Products"] = products;
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int id)
        {
            var products = GetProductsFromSession();
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null) return HttpNotFound();
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ProductModel model)
        {
            try
            {
                var products = GetProductsFromSession();
                var product = products.FirstOrDefault(p => p.Id == id);
                if (product == null) return HttpNotFound();

                product.Name = model.Name;
                product.Price = model.Price;
                product.Description = model.Description;
                product.DateExpiration = model.DateExpiration;

                Session["Products"] = products;
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int id)
        {
            var products = GetProductsFromSession();
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null) return HttpNotFound();
            return View(product);
        }


        private void PrintProductsToDebug(List<ProductModel> products)
        {
            foreach (var p in products)
            {
                System.Diagnostics.Debug.WriteLine($"Id: {p.Id}, Name: {p.Name}, Active: {p.Active}, Price: {p.Price}, Expiration: {p.DateExpiration}");
            }
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var products = GetProductsFromSession();
                var product = products.FirstOrDefault(p => p.Id == id);
                if (product == null) return HttpNotFound();

                product.Active = false;

                Session["Products"] = products;

                PrintProductsToDebug(products);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
