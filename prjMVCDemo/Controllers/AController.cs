using prjLottoApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMVCDemo.Controllers
{
    public class AController : Controller // Controller後面一定要加Controller, ex: AController, BController
                                          // 但網址上不顯示後面所加的Controller, local/A(class)/Action(Method)
    {
        //Request => Server => Response
        public string demoResponse()
        {
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.Filter.Close();
            Response.WriteFile(@"C:\note\01.jpg");
            Response.End();
            return "";
        }

        // https://local/A/demoRequest?productId=1
        // XBox 加入購物車成功
        public string demoRequest()
        {
            string id = Request.QueryString["productId"];
            if (id == "1")
                return "XBox 加入購物車成功";
            else if (id == "2")
                return "PS5 加入購物車成功";
            else if (id == "3")
                return "Switch 加入購物車成功";
            return "找不到該產品資料";
        }

        // https://local/A/demoParameter/?productId=1
        // XBox 加入購物車成功
        // 若參數不加"?"，當不傳參數時會出現null錯誤(int 宣告4bytes 若不傳參數系統記憶體不知道要放什麼)

        // 若參數名稱為"id"，則網址上問號與Key都不用輸入
        // ex:https://local/A/demoParameter/2
        public string demoParameter(int? productId)
        {            
            if (productId == 1)
                return "XBox 加入購物車成功";
            else if (productId == 2)
                return "PS5 加入購物車成功";
            else if (productId == 3)
                return "Switch 加入購物車成功";
            return "找不到該產品資料";
        }

        public string queryById(int? id)
        {
            if(id==null)
                return "找不到該客戶資料";

            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
            con.Open();

            SqlCommand cmd = new SqlCommand("Select * from tCustomer where fId = " + id.ToString(), con);
            SqlDataReader reader = cmd.ExecuteReader();

            string result = "找不到該客戶資料";
            if (reader.Read())
                result = reader["fName"].ToString() + " / " + reader["fPhone"].ToString();
            
            con.Close();
            return result;
        }

        public string demoServer()
        {
            return "目前伺服器上的實體位置：" + Server.MapPath(".");
        }

        public string sayHello()
        {
            return "Hello ASP.NET MVC";
        }

        public string lotto()
        {
            return (new CLottoGen()).getLotto();
        }

        // GET: A
        public ActionResult showById()
        {
            return View();
        }
    }
}