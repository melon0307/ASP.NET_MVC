using prjMVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMVCDemo.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult List()
        {
            List<CCustomer> list = null;
            string keyword = Request.Form["txtKeyword"];
            if (string.IsNullOrEmpty(keyword))
                list = (new CCustomerFactory()).queryAll();
            else
                list = (new CCustomerFactory()).queryByKeyword(keyword);
            return View(list);
        }

        public ActionResult New()
        {
            return View();
        }

        public ActionResult Save()
        {
            CCustomer c = new CCustomer();
            c.fName = Request.Form["txtName"];
            c.fPhone = Request.Form["txtPhone"];
            c.fEmail = Request.Form["txtEmail"];
            c.fAddress = Request.Form["txtAddress"];
            c.fPassword = Request.Form["txtPassword"];

            (new CCustomerFactory()).insert(c);
            return RedirectToAction("List");
        }

        public ActionResult Delete(int? id)
        {
            (new CCustomerFactory()).Delete((int)id);
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            CCustomer c = (new CCustomerFactory()).queryById((int)id);
            return View(c);
        }

        [HttpPost]// 告訴系統假如執行post，才會執行以下方法
        public ActionResult Edit(CCustomer c)
        {
            // 偷雞摸狗法:
            // 1.把物件變成參數(就不用new)
            // 2.屬性名稱必須與UIname相同
            // 因而達到省略以下註解內容效果
            //=====================================================
            //CCustomer c = new CCustomer();
            //c.fId = Convert.ToInt32(Request.Form["txtFid"]);
            //c.fName = Request.Form["txtName"];
            //c.fPhone = Request.Form["txtPhone"];
            //c.fEmail = Request.Form["txtEmail"];
            //c.fAddress = Request.Form["txtAddress"];
            //c.fPassword = Request.Form["txtPassword"];
            //=====================================================
            (new CCustomerFactory()).update(c);
            return RedirectToAction("List");
        }
    }
}