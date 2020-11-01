using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using bagel_sales_control.Helpers;

namespace bagel_sales_control.Features.Sales
{
    public class Sales : ControlTransactionFields
    {
        [Key]
        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public int SoldQuantity { get; set; }
        public string TypeSale { get; set; }
        public int Total { get; set; }
        public string Commentary { get; set; }

        [NotMapped]
        public string NameProduct { get; set; }

    }
}