using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using bagel_sales_control.Helpers;

namespace bagel_sales_control.Features
{
    public class ProductAgg : ControlTransactionFields
    {
        [Key]
        public int ProductId { get; set; }
        public string NameProduct { get; set; }
        public int InitialExistence { get; set; }
        public int Existence { get; set; }
        public int PurchasePrice { get; set; }
        public int SalePrice { get; set; }
        public int Wholesaleprice { get; set; }
    }
}