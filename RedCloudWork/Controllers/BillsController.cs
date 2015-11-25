using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using RedCloudWork.Domian;

namespace RedCloudWork.Controllers
{
    public class BillsController : Controller
    {
       
        // GET: Bills
        public ActionResult AddBills()
        {
            var myContext = new MyDbcontext();
            var list = (from item in myContext.Saleswoman
                select new SelectListItem()
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                }).ToList();
            ViewBag.Salses = list ;
            
            return View();
        }
        [HttpPost]
        public JsonResult AddBills(string name, HttpPostedFileBase file)
        {

            var result = new {state="ok"};
            return  Json(result);
        }
    }
}