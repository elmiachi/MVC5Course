using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using System.Data.Entity.Validation;
using System.Data.Entity;

namespace MVC5Course.Controllers
{
    public class EFController : Controller
    {
        FabricsEntities db = new FabricsEntities();
        // GET: EF
        public ActionResult Index(bool? IsActive ,string keyword)
        {
            var db = new FabricsEntities();
            var product = new Product()
            {
                ProductName = "BMW",
                Price = 2,
                Stock = 1,
                Active = true

            };
            //db.Product.Add(product);
            SaveChanges();
            
           
            var pkey = product.ProductId;
            //var data = db.Product.ToList();
            var data = db.Product.OrderByDescending(p => p.ProductId).AsQueryable();
            if (IsActive.HasValue)
            {
                data = data.Where(p=>p.Active.HasValue?p.Active.Value==IsActive.Value:false);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.Where(p=>p.ProductName.Contains(keyword));
            }
            foreach (var item in data)
            {
                item.Price = item.Price + 1;

            }
            //db.Database.ExecuteSqlCommand("update dbo....")
            SaveChanges();
            return View(data);
        }

      

        public ActionResult Detail(int id)
        {
            //var data = db.Product.Find(id);
            //var data = db.Product.Where(p => p.ProductId == id).FirstOrDefault();
            var data = db.Product.FirstOrDefault(p => p.ProductId == id);
            return View(data);
        }
        public ActionResult Delete(int id)
        {
            var product = db.Product.Find(id);
            //foreach (var ol in product.OrderLine.ToList())
            //{
            //    db.OrderLine.Remove(ol);
            //}
            RemoveProduct(product);
            SaveChanges();

            return RedirectToAction("Index");
        }

        private void RemoveProduct(Product product)
        {
            db.OrderLine.RemoveRange(product.OrderLine);
            db.Product.Remove(product);
        }
        public ActionResult QueryPlan(int num = 10)
        {
            var data = db.Product
                .Include(t => t.OrderLine)
                .OrderBy(p => p.ProductId)
                .AsQueryable();

            //var data = db.Database.SqlQuery<Product>(@"
            //select * 
            //FROM dbo.Product p 
            //WHERE
            //    p.ProductId < @p0
            //        ", num);
            db.usp_Fabrics(10, 10); //呼叫StoreProcedure
            return View(data);
        }
        private void SaveChanges()
        {
            try
            {
                db.SaveChanges();

            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                {
                    string entityname = item.Entry.Entity.GetType().Name;
                    foreach (DbValidationError err in item.ValidationErrors)
                    {
                        throw new Exception(entityname + "類型驗證失敗:" + err.ErrorMessage);
                    };
                };
                throw;
            }
        }
    }
    
}
