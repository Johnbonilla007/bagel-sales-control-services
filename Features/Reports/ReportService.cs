using System.Threading.Tasks;
using System;
using bagel_sales_control.DataContext;
using bagel_sales_control.Core;
using System.Linq;
using System.Collections.Generic;

namespace bagel_sales_control.Features.Reports
{
    public class ReportService
    {
        private readonly BagelSalesControlContext _bagelSalesControlContext;

        public ReportService(BagelSalesControlContext bagelSalesControlContext)
        {
            if (bagelSalesControlContext == null) throw new ArgumentNullException(nameof(bagelSalesControlContext));

            _bagelSalesControlContext = bagelSalesControlContext;
        }


        public async Task<Response> GetGeneralReport()
        {
            var products = _bagelSalesControlContext.Product.ToList();
            var sales = _bagelSalesControlContext.Sales.ToList();
            GeneralReport generalReport = new GeneralReport();

            generalReport.TotalReverse = GetTotalReverse(products);
            generalReport.TotalSoldNormal = GetTotalSold(sales, "Normal");
            generalReport.TotalSoldWholesale = GetTotalSold(sales, "Mayoreo");
            generalReport.TotalSold = generalReport.TotalSoldNormal + generalReport.TotalSoldWholesale;


            return new Response { Data = generalReport };
        }

        private int GetTotalSold(List<Sales.Sales> sales, String typeSale)
        {
            List<int> totalsSold = new List<int>();
            if (sales.Any())
            {
                sales.ForEach(s =>
                {
                    if (s.TypeSale == typeSale)
                    {
                        totalsSold.Add(s.Total);
                    }
                });
            }

            return totalsSold.Sum(t => t);
        }

        private int GetTotalSoldNormal(List<Sales.Sales> sales)
        {
            throw new NotImplementedException();
        }

        private int GetTotalReverse(List<ProductAgg> products)
        {
            List<int> totalsInverted = new List<int>();
            if (products.Any())
            {
                products.ForEach(item =>
             {
                 int quantityProduct = item.InitialExistence;
                 int reverse = item.PurchasePrice;

                 totalsInverted.Add(quantityProduct * reverse);
             });

            }


            return totalsInverted.Sum(t => t);
        }
    }
}