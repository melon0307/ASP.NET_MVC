using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMVCDemo.Controllers
{
    public class 暫存練習Controller : Controller
    {
        // GET: 暫存練習
        static int count;// 使用此方式會異地共用一個變數，若希望異地不共用則需使用session(key,value) sessionId,sessionDate(20mins)
        public ActionResult showCount()
        {
            count++;
            ViewBag.kk = count;
            return View();
        }

        public ActionResult showCountBySession()
        {
            int count = 0;
            if (Session["kk"] != null)
                count = (int)Session["kk"];
            count++;
            Session["kk"] = count;
            ViewBag.kk = count;
            return View();
        }

        public ActionResult showCountByCookie()
        {
            int count = 0;
            HttpCookie x = Request.Cookies["KK"];// KK=key
            if (x != null)
                count = Convert.ToInt32(x.Value);
            count++;
            x = new HttpCookie("KK");
            x.Value = count.ToString();
            x.Expires = DateTime.Now.AddSeconds(20);
            Response.Cookies.Add(x);

            ViewBag.kk = count;
            return View();
        }
    }
}