using System;

namespace RedCloudWork.Domian
{
    public class Bills:BaseEntity
    {
        /// <summary>
        /// 业务员
        /// </summary>
        public Salesman Salesman { get; set; }
        /// <summary>
        /// 账单使用的产品
        /// </summary>
         public Products Product { get; set; }
        /// <summary>
        /// 计费来源，当前只有“结算”
        /// </summary>
         public string ChargeSource { get; set; }
        /// <summary>
        /// 业务请求编号
        /// </summary>
        public string ServiceRequestNo { get; set; }
        /// <summary>
        /// 商户
        /// </summary>
        public Merchants Merchant { get; set; }
        /// <summary>
        /// 计费金额
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 交易时间
        /// </summary>
        public DateTime TradingTime { get; set; }
        /// <summary>
        /// 完成时间,未完成不具有完成时间
        /// </summary>
        public DateTime? CompletionTime { get; set; }
        /// <summary>
        /// 产品费用
        /// </summary>
        public decimal ProductExpense { get; set; }
        /// <summary>
        /// 是否完成
        /// </summary>
        public bool CompleteState { get; set; }
    }
}