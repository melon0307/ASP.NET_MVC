using prjMVCDemo.Models;
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
        public ActionResult CartView()
        {
            List<CShoppingCartItem> cart = Session[CDictionary.SK_已加入購物車的_商品們_列表] as List<CShoppingCartItem>;
            if (cart == null)
                return RedirectToAction("List");
            return View(cart);
        }

        public ActionResult List()
        {
            var db = (new dbDemoEntities()).tProducts;

            return View(db);
        }

        public ActionResult Delete(int id)
        {
            List<CShoppingCartItem> cart = Session[CDictionary.SK_已加入購物車的_商品們_列表] as List<CShoppingCartItem>;
            cart.RemoveAt(id);

            return RedirectToAction("CartView");
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

        [HttpGet]
        public ActionResult AddToSession(int? id)
        {
            dbDemoEntities db = new dbDemoEntities();
            tProducts prod = db.tProducts.FirstOrDefault(x => x.fId == id);
            if (prod == null)
                return RedirectToAction("List");
            return View(prod);
        }

        [HttpPost]
        public ActionResult AddToSession(CAddToCartViewModel mvModel)
        {
            dbDemoEntities db = new dbDemoEntities();
            tProducts prod = db.tProducts.FirstOrDefault(x => x.fId == mvModel.txtFId);
            if (prod == null)
                return RedirectToAction("List");

            List<CShoppingCartItem> list = Session[CDictionary.SK_已加入購物車的_商品們_列表] as List<CShoppingCartItem>;
            if(list == null)
            {
                list = new List<CShoppingCartItem>();
                Session[CDictionary.SK_已加入購物車的_商品們_列表] = list;
            }

            CShoppingCartItem cartItem = new CShoppingCartItem()
            {
                productId = mvModel.txtFId,
                price = (decimal)prod.fPrice,
                count = mvModel.txtCount,
                product = prod
            };

            list.Add(cartItem);
            return RedirectToAction("List");
        }
    }
}