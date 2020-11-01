using System.Threading.Tasks;
using System;
using bagel_sales_control.DataContext;
using bagel_sales_control.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage;
using bagel_sales_control.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace bagel_sales_control.Features.Sales
{
    public class SalesService
    {
        private readonly BagelSalesControlContext _bagelSalesControlContext;

        public SalesService(BagelSalesControlContext bagelSalesControlContext)
        {
            if (bagelSalesControlContext == null) throw new ArgumentNullException(nameof(bagelSalesControlContext));

            _bagelSalesControlContext = bagelSalesControlContext;
        }

        public async Task<Response> SaveSale(SalesRequest salesRequest)
        {
            if (salesRequest == null) throw new ArgumentNullException(nameof(salesRequest));
            string notification = "";
            Sales sale = null;
            IDbContextTransaction transaction = _bagelSalesControlContext.Database.BeginTransaction();
            ControlTransactionFields transactionInfo = TransactionInfo.GetTransactionData(salesRequest.UserName);

            if (salesRequest.SaleId != 0)
            {
                sale = _bagelSalesControlContext.Sales.ToList().Find(s => s.SaleId == salesRequest.SaleId);
            }

            if (sale == null)
            {
                sale = MaterializeSale(salesRequest, transactionInfo);

                await _bagelSalesControlContext.AddAsync<Sales>(sale);
            }
            else
            {
                sale.SoldQuantity = salesRequest.SoldQuantity;
                sale.TypeSale = salesRequest.TypeSale;
                sale.Total = salesRequest.Total;
                sale.Commentary = salesRequest.Commentary;
                sale.TransactionModificationDate = DateTime.Now;
            }

            ProductAgg product = _bagelSalesControlContext.Product.ToList().Find(p => p.ProductId == salesRequest.ProductId);

            if (product != null)
            {
                if (product.Existence < salesRequest.SoldQuantity)
                {
                    notification = "No hay suficientes " + product.NameProduct + " disponibles";

                    return new Response { Notification = notification };
                }

                product.Existence = product.Existence - salesRequest.SoldQuantity;
                if (product.Existence <= 5)
                {
                    notification = "El Producto " + product.NameProduct + " está a punto de acabarse";
                }
            }


            await _bagelSalesControlContext.SaveChangesAsync();
            transaction.Commit();

            _bagelSalesControlContext.Dispose();
            transaction.Dispose();


            return new Response { Message = "The sale was registered successfully", Data = sale, Notification = notification };
        }

        public async Task<Response> GetSales()
        {
            List<Sales> sales = await _bagelSalesControlContext.Sales.ToListAsync();
            List<ProductAgg> products = await _bagelSalesControlContext.Product.ToListAsync();

            if (sales.Any())
            {
                sales.ForEach(sale =>
                {
                    sale.NameProduct = GetNameProduct(sale.ProductId, products);
                });


                _bagelSalesControlContext.Dispose();
                return new Response { Data = sales };
            }

            _bagelSalesControlContext.Dispose();
            return new Response { Message = "There isn't sales registered" };

        }

        private string GetNameProduct(int productId, List<ProductAgg> products)
        {
            string nameProduct = "";

            if (products.Any())
            {
                nameProduct = products.FirstOrDefault(p => p.ProductId == productId)?.NameProduct;
            }

            return nameProduct;
        }

        public Task<Response> GetSaleByProductId(SalesRequest salesRequest)
        {
            if (salesRequest == null) throw new ArgumentNullException(nameof(salesRequest));

            if (salesRequest.ProductId == 0)
            {
                return Task.FromResult(new Response { Notification = "ProductId is requerid" });
            }

            List<Sales> sales = _bagelSalesControlContext.Sales.ToList().FindAll(s => s.ProductId == salesRequest.ProductId);

            if (sales.Any())
            {
                _bagelSalesControlContext.Dispose();
                return Task.FromResult(new Response { Data = sales });
            }

            return Task.FromResult(new Response { Message = "There isn't sales registered with that product" });
        }

        public async Task<Response> DeleteSale(SalesRequest salesRequest)
        {
            if (salesRequest == null) throw new ArgumentNullException(nameof(salesRequest));

            Sales sale = null;

            if (salesRequest.SaleId != 0)
            {
                sale = _bagelSalesControlContext.Sales.ToList().Find(s => s.SaleId == salesRequest.SaleId);

                if (sale != null)
                {
                    IDbContextTransaction transaction = _bagelSalesControlContext.Database.BeginTransaction();

                    _bagelSalesControlContext.Remove<Sales>(sale);
                    await _bagelSalesControlContext.SaveChangesAsync();
                    transaction.Commit();

                    _bagelSalesControlContext.Dispose();
                    transaction.Dispose();

                    return new Response { Message = "Sale removed successfully" };
                }
            }

            _bagelSalesControlContext.Dispose();

            return new Response { Notification = "SaleId is requerid" };

        }

        private Sales MaterializeSale(SalesRequest salesRequest, ControlTransactionFields transactionInfo)
        {
            return new Sales
            {
                ProductId = salesRequest.ProductId,
                SoldQuantity = salesRequest.SoldQuantity,
                TypeSale = salesRequest.TypeSale,
                Total = salesRequest.Total,
                Commentary = salesRequest.Commentary,
                CreatedBy = transactionInfo.CreatedBy,
                TransactionDate = transactionInfo.TransactionDate,
            };
        }
    }
}