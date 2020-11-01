using System;
namespace bagel_sales_control.Helpers
{
    public class ControlTransactionFields
    {
        public DateTime TransactionDate { get; set; }
        public DateTime TransactionModificationDate { get; set; }
        public string CreatedBy { get; set; }
    }
}