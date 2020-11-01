namespace bagel_sales_control.Features.Product
{
    public class ProductRequest
    {
        public int ProductId { get; set; }
        public string NameProduct { get; set; }
        public int InitialExistence { get; set; }
        public int Existence { get; set; }
        public int PurchasePrice { get; set; }
        public int SalePrice { get; set; }
        public int Wholesaleprice { get; set; }
        public string UserName { get; set; }
    }
}