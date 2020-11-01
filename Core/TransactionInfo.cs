using System;
using bagel_sales_control.Helpers;

namespace bagel_sales_control.Core
{
    public static class TransactionInfo
    {
        public static ControlTransactionFields GetTransactionData(string userName)
        {
            return new ControlTransactionFields
            {
                TransactionDate = DateTime.Now,
                CreatedBy = userName
            };
        }
    }
}