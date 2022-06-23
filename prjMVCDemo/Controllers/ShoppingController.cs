using prjMVCDemo.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMVCDemo.Controllers
{
    public class ShoppingController : Controller
    {
        // GET: Shopping
        public ActionResult List()
        {
            var db = (new dbDemoEntities()).tProducts;

            return View(db);
        }

        [HttpGet]
        public ActionResult AddToCart(int? id)
        {
            dbDemoEntities db = new dbDemoEntities();
            tProducts prod = db.tProducts.FirstOrDefault(x => x.fId == id);
            if (prod == null)
                return RedirectToAction("List");
            return View(prod);
        }

        [HttpPost]
        public ActionResult AddToCart(CAddToCartViewModel mvModel)
        {
            dbDemoEntities db = new dbDemoEntities();
            tProducts prod = db.tProducts.FirstOrDefault(x => x.fId == mvModel.txtFId);
            if (prod == null)
                return RedirectToAction("List");

            tShoppingCart cart = new tShoppingCart();
            cart.fProductId = mvModel.txtFId;
            cart.fPrice = prod.fPrice;
            cart.fDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            cart.fCount = mvModel.txtCount;
            cart.fCustomerId = 1;

            db.tShoppingCart.Add(cart);
            db.SaveChanges();

            return RedirectToAction("List");
        }
    }
}