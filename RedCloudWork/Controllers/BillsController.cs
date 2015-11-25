using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using NPOI.HSSF.UserModel;
using RedCloudWork.Domian;

namespace RedCloudWork.Controllers
{
    public class BillsController : Controller
    {
        protected List<Bills> GetBillsByExecl()
        {
            return new List<Bills>();
        }
       
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
            if (Request.Files[0] !=null)
            {
                var fileName = Path.Combine(Request.MapPath("~/Upload"), Path.GetFileName(Request.Files[0].FileName));
                try
                {
                    file.SaveAs(fileName);
                }
                catch (Exception ex)
                {
                    return Json(new { state = "no" });
                }
            }

            var result = new { state = "ok" };
            return Json(result);
        }
    }
}