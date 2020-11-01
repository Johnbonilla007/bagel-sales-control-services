namespace bagel_sales_control.Features.Sales
{
    public class SalesRequest
    {
        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public int SoldQuantity { get; set; }
        public string TypeSale { get; set; }
        public int Total { get; set; }
        public string Commentary { get; set; }
        public string UserName { get; set; }
    }
}