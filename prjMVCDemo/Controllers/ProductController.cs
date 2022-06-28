using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMVCDemo.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult List()
        {
            string keyword = Request.Form["txtKeyword"];
            var db = (new dbDemoEntities()).tProducts;
            IEnumerable<tProducts> datas = null;

            if (string.IsNullOrEmpty(keyword))
                datas = db;
            else
                datas = db.Where(t => t.fName.Contains(keyword));

            return View(datas);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(tProducts p)
        {
            dbDemoEntities db = new dbDemoEntities();
            db.tProducts.Add(p);
            db.SaveChanges();
            return RedirectToAction("List");
        }

        public ActionResult Delete(int? id)
        {
            dbDemoEntities db = new dbDemoEntities();
            tProducts p = db.tProducts.FirstOrDefault(x => x.fId == id);
            db.tProducts.Remove(p);
            db.SaveChanges();
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            dbDemoEntities db = new dbDemoEntities();
            tProducts prod = db.tProducts.FirstOrDefault(x => x.fId == id);
            if (prod == null)
                return RedirectToAction("List");
            return View(prod);
        }

        [HttpPost]
        public ActionResult Edit(tProducts p)
        {
            dbDemoEntities db = new dbDemoEntities();
            tProducts prod = db.tProducts.FirstOrDefault(x => x.fId == p.fId);

            if (prod != null)
            {
                if (p.photo != null)
                {
                    string pName = Guid.NewGuid().ToString() + ".png";
                    p.photo.SaveAs(Server.MapPath("~/Images/" + pName));
                    prod.fImagePath = pName;
                }
                prod.fName = p.fName;
                prod.fPrice = p.fPrice;
                prod.fQty = p.fQty;
                prod.fCost = p.fCost;
            }
            db.SaveChanges();
            return RedirectToAction("List");
        }
    }
}