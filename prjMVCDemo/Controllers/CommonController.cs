using prjMVCDemo.Models;
using prjMVCDemo.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMVCDemo.Controllers
{
    public class CommonController : Controller
    {
        // GET: Common
        public ActionResult Home()
        {
            CCustomer user = Session[CDictionary.SK_LogIned_User] as CCustomer;
            if (user == null)
                return RedirectToAction("LogIn");
            
            return View(user);
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(CLogInViewModel vModel)
        {
            if (!string.IsNullOrEmpty(vModel.txtAccount))
            {
                CCustomer cust = (new CCustomerFactory()).queryByEmail(vModel.txtAccount);
                if (cust.fEmail.Equals(vModel.txtAccount))
                {
                    Session[CDictionary.SK_LogIned_User] = cust;
                    return RedirectToAction("Home");
                }                
            }
            return View();
        }
    }
}