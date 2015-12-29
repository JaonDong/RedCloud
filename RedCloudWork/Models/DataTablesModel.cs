using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace RedCloudWork.Models
{
    public class Search
    {
        private const string _value = "[search][value]";
        private const string _FirtsValue = "search[value]";
        private const string _regex = "[search][regex]";
        private const string _FirtsRegex = "search[regex]";
        public string Value { get; set; }
        public string Regex { get; set; }

        public Search(IEnumerable<KeyValuePair<string, string>> keyValues)
        {
            foreach (var keyValue in keyValues)
            {
                switch (keyValue.Key)
                {
                    case _value:
                        this.Value = keyValue.Value;
                        break;
                    case _regex:
                        this.Regex = keyValue.Value;
                        break;
                    case _FirtsValue:
                        this.Value = keyValue.Value;
                        break;
                    case _FirtsRegex:
                        this.Regex = keyValue.Value;
                        break;

                }
            }
        }
    }

    public class Column
    {
        private const string _data = "[data]";
        private const string _name = "[name]";
        private const string _searchable = "[searchable]";
        private const string _orderable = "[orderable]";
        private const string _search = "[search]";
        public string Data { get;  set; }
        public string Name { get; set; }
        public bool Searchable { get; set; }

        public bool Orderable { get; set; }

        public Search Search { get; set; }


        public Column(IEnumerable<KeyValuePair<string, string>> keyValues)
        {
            var keyValuePairs = keyValues as KeyValuePair<string, string>[] ?? keyValues.ToArray();
            foreach (var keyValue in keyValuePairs)
            {
                switch (keyValue.Key)
                {
                    case _data:
                        this.Data = keyValue.Value;
                        break;
                    case _name:
                        this.Name = keyValue.Value;
                        break;
                    case _searchable:
                        this.Orderable = DataTablesModel.TryParseBool(keyValue.Value);
                        break;
                    case _orderable:
                        this.Searchable = DataTablesModel.TryParseBool(keyValue.Value);
                        break;
                }
            }
            this.Search=new Search(keyValuePairs.Where(x=>x.Key.Contains(_search)).ToList());
        }

    }
    public class DataTablesModel
    {
        private readonly HttpRequestBase _request;
        private const string StartPar = "start";
        private const string LengthPar = "length";
        private const string DrawPar = "draw";
        private const string ColumnPar = "columns";
        private const string SearchPar = "search[";
        public DataTablesModel(HttpRequestBase request)
        {
            _request = request;
            
            //
            Columns=new List<Column>();

            var listValues = GetColumnKeyValues(request.Form);
            foreach (var keyValues in listValues)
            {
                Columns.Add(new Column(keyValues));
            }

            //
            Start =TryParseInt(_request.Form[StartPar]);
            //
            Length = TryParseInt(_request.Form[LengthPar]);
            //
            Draw = TryParseInt(_request.Form[DrawPar]);

            this.Search = new Search(request.Form.AllKeys.Where(x => x.Contains(SearchPar)).Select(x=>new KeyValuePair<string, string>(x,request.Form[x])).ToList());
        }

        #region 属性
        public int Draw { get; private set; }
        /// <summary>
        /// 列
        /// </summary>
        public  List<Column> Columns { get; private set; }
        /// <summary>
        /// 跳过的数据数目
        /// </summary>
        public int Start { get; private set; }
        /// <summary>
        /// 查询的数据
        /// </summary>
        public int Length { get; private set; }
        /// <summary>
        /// 是否可搜索
        /// </summary>
        public Search Search { get; private set; } 
        #endregion

        protected List<List<KeyValuePair<string, string>>> GetColumnKeyValues(NameValueCollection collection)
        {
            var keyValues = new List<List<KeyValuePair<string, string>>>();
            var conunt = collection.AllKeys.Count(x => x.Contains(ColumnPar))/6;

            for (var i = 0; i < conunt; i++)
            {
                var key = string.Format("{0}[{1}]", ColumnPar, i);

                var list = collection.AllKeys.Where(x => x.Contains(key)).Select(x => new KeyValuePair<string,string>(x.Replace(key, ""), collection[x])).ToList();

                keyValues.Add(list);
            }
            return keyValues;
        }

        public static int TryParseInt(string str)
        {
            var num = 0;

            int.TryParse(str, out num);

            return  num;
        }

        public static bool TryParseBool(string str)
        {
            var b = false;

            bool.TryParse(str, out b);

            return b;
        }
    }

    
}