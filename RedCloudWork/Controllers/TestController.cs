using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Results;
using Newtonsoft.Json;
using RedCloudWork.Models;

namespace RedCloudWork.Controllers
{
    public class TestController : ApiController
    {

        public HttpResponseMessage Post(string name, string disType, int start = 0, int length = 20)
        {
            var list=new List<TestModel>()
            {
                new TestModel(){Name = "Jack",Age = 18},
                new TestModel(){Name = "董陌陌",Age = 20},
                new TestModel(){Name = "Dong",Age = 36},
                new TestModel(){Name = "Jack",Age = 18},
                new TestModel(){Name = "董陌陌",Age = 20},
                new TestModel(){Name = "Dong",Age = 36},
                new TestModel(){Name = "Jack",Age = 18},
                new TestModel(){Name = "董陌陌",Age = 20},
                new TestModel(){Name = "Dong",Age = 36},
                new TestModel(){Name = "Jack",Age = 18},
                new TestModel(){Name = "董陌陌",Age = 20},
                new TestModel(){Name = "Dong",Age = 36},
            };
            var pageData = list.Skip(start).Take(length);
            var restult = new {data = pageData, recordsTotal = list.Count, recordsFiltered = list.Count};

            return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(restult), Encoding.GetEncoding("UTF-8"), "application/json") };
        }
    }
}
