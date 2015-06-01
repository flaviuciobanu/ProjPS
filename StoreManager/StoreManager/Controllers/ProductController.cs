using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreManager.Models;

namespace StoreManager.Controllers
{
    public class ProductController : Controller
    {
        private ProductDBContext db = new ProductDBContext();
        private SaleDBContext saleDb = new SaleDBContext();
    
        //
        // GET: /Product/

        public ActionResult Index()
        {
            return View(db.produse.ToList());
        }

        //
        // GET: /Product/Details/5

        public ActionResult Details(int id = 0)
        {
            Product product = db.produse.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // GET: /Product/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Product/Create

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                db.produse.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        //
        // GET: /Product/Edit/5

        public ActionResult Edit(int id = 0)
        {
            
                Product product = db.produse.Find(id);
                if (product == null)
                {
                    return HttpNotFound();
                }
                return View(product);
            
        }

        //
        // POST: /Product/Edit/5

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(product).State = EntityState.Modified;
                 db.SaveChanges();
                 return RedirectToAction("Index");
                 }
            return View(product);
            }
             else return RedirectToAction("Index");
        }

        //
        // GET: /Product/Delete/5

        public ActionResult Delete(int id = 0)
        {
            if (User.Identity.IsAuthenticated)
            {
            Product product = db.produse.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
            }
            else return RedirectToAction("Index");
        }

        //
        // POST: /Product/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.produse.Find(id);
            db.produse.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        [ActionName("addForSale")]
        public ActionResult addForSale(int id)
        {
            Product prod = db.produse.Find(id);
            prod.Stoc -= 1;
            db.Entry(prod).State = EntityState.Modified;
            db.SaveChanges();
            saleDb.vanzare.Add(prod);
            saleDb.SaveChanges();
            saleDb.PretTotal += prod.Pret;
            return RedirectToAction("Index");

        }
        [ActionName("finalizeSale")]
        public ActionResult finalizeSale()
        {
            decimal pretTotal = 0;
            foreach (var entity in saleDb.vanzare)
               pretTotal += entity.Pret;
            var sellList = from m in saleDb.vanzare
                       select m;
            ViewData["pretTotal"] =pretTotal.ToString();
            return View(sellList);
        }
        public ActionResult clearProducts()
        {
            foreach (var entity in saleDb.vanzare)
                saleDb.vanzare.Remove(entity);
            saleDb.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult exportPDF()
        {
            String invoiceString="";
            foreach (var entity in saleDb.vanzare)
                invoiceString += entity.Nume + " \n";
            invoiceString += "pret total: " + saleDb.PretTotal;
            PDFexporter.exportPDF(invoiceString);
            return RedirectToAction("Index");
        }
    }
}