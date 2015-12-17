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
using NPOI.SS.UserModel;
using RedCloudWork.Domian;

namespace RedCloudWork.Controllers
{
    public class BillsController : Controller
    {

        protected List<Bills> GetBillsByExecl(string fileName,string name)
        {
            var table = ComMethod.GetDataTableByExecl(fileName);

            //var list = table.GetModelByDataTable<Bills>();
            var list = GeListByDt(table, name);

            return list;
        }
       
        // GET: Bills
        public ActionResult AddBills()
        {
            var myContext = new MyDbcontext();
            var list = (from item in myContext.Saleswoman
                select new SelectListItem()
                {
                    Text = item.Name,
                    Value = item.Name
                }).ToList();
            ViewBag.Salses = list ;
            
            return View();
        }
        [HttpPost]
        public JsonResult AddBills(string name, HttpPostedFileBase file)
        {
            if (Request.Files[0] !=null)
            {
                var fileName = Path.Combine(Request.MapPath("~/Upload"),DateTime.Now.ToString("yyyyMMddHHmmss") +Path.GetFileName(Request.Files[0].FileName));
                try
                {
                    file.SaveAs(fileName);
                    var list = GetBillsByExecl(fileName, name);

                }
                catch (Exception ex)
                {
                    return Json(new { state = "no" });
                }
            }
            
            var result = new { state = "ok" };
            return Json(result);
        }

        public  List<Bills> GeListByDt(DataTable dt, string name)
        {
            var list = new List<Bills>();

            try
            {
                var productRepository = ComMethod.GetRepository<Products>();
                var saleManRepository = ComMethod.GetRepository<Salesman>();
                var merchantRepository = ComMethod.GetRepository<Merchants>();
                var billsRepository = ComMethod.GetRepository<Bills>();

                foreach (DataRow row in dt.Rows)
                {
                    var productName = row["产品"].ToString();
                    var merchantNo = row["商户编号"].ToString();
                    var product = productRepository.Table.FirstOrDefault(p => p.Name==productName);
                    var saleMan = saleManRepository.Table.FirstOrDefault(p => p.Name == name);
                    var merchant =merchantRepository.Table.FirstOrDefault(p => p.Name == merchantNo);

                    if (product == null)
                    {
                        product = new Products() { Name = productName };
                        productRepository.Insert(product);
                        product = productRepository.Table.FirstOrDefault(p => p.Name == productName);
                    }
                    if (saleMan == null)
                    {
                        saleMan = new Salesman() { Name = name };
                        saleManRepository.Insert(saleMan);
                        saleMan = saleManRepository.Table.FirstOrDefault(p => p.Name == name);
                    }
                    if (merchant == null)
                    {
                        merchant = new Merchants()
                        {
                            MerchantNo = merchantNo,
                            Name = row["商户"].ToString()
                        };
                        merchantRepository.Insert(merchant);
                        merchant = merchantRepository.Table.FirstOrDefault(p => p.MerchantNo == merchantNo);
                    }
                 

                    var model = new Bills
                    {
                        Product = product,
                        ProductId = product.Id,
                        ChargeSource = row["计费来源"].ToString(),
                        Salesman = saleMan,
                        SalesmanId = saleMan.Id,
                        ServiceRequestNo = row["业务请求编号"].ToString(),
                        Merchant = merchant,
                        MerchantId = merchant.Id,
                        Amount = decimal.Parse(row["计费金额"].ToString()),
                        TradingTime = DateTime.Parse(row["交易时间"].ToString().ComTime()),
                        CompletionTime = DateTime.Parse(row["完成时间"].ToString().ComTime()),
                        ProductExpense = decimal.Parse(row["产品费用"].ToString()),
                        CompleteState = row["状态"].ToString() == "已完成"
                    };

                    billsRepository.Insert(model);
                }
            }
            catch (Exception ex)
            {
                //写入日志
            }
            return list;

        }
    }
}