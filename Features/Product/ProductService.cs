using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bagel_sales_control.Core;
using bagel_sales_control.DataContext;
using bagel_sales_control.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace bagel_sales_control.Features.Product
{
    public class ProductService
    {
        private readonly BagelSalesControlContext _bagelSalesControlContext;

        public ProductService(BagelSalesControlContext bagelSalesControlContext)
        {
            if (bagelSalesControlContext == null) throw new ArgumentNullException(nameof(bagelSalesControlContext));

            _bagelSalesControlContext = bagelSalesControlContext;
        }

        public async Task<Response> GetProducts()
        {
            List<ProductAgg> products = await _bagelSalesControlContext.Product.ToListAsync();

            if (products.Any())
            {
                _bagelSalesControlContext.Dispose();

                return new Response { Data = products };
            }
            _bagelSalesControlContext.Dispose();

            return new Response { Message = "There isn't Products" };
        }

        public Task<Response> GetProductByName(ProductRequest productRequest)
        {
            if (productRequest == null) throw new ArgumentNullException(nameof(productRequest));

            List<ProductAgg> products = _bagelSalesControlContext.Product.ToList().FindAll(p => p.NameProduct.Contains(productRequest.NameProduct));

            if (products.Any())
            {
                _bagelSalesControlContext.Dispose();

                return Task.FromResult(new Response { Data = products });
            }

            _bagelSalesControlContext.Dispose();

            return Task.FromResult(new Response { Message = "There isn't Products whit that name" });
        }

        public async Task<Response> DeleteProduct(ProductRequest productRequest)
        {
            if (productRequest == null) throw new ArgumentNullException(nameof(productRequest));

            ProductAgg product = null;

            if (productRequest.ProductId != 0)
            {
                product = _bagelSalesControlContext.Product.ToList().Find(p => p.ProductId == productRequest.ProductId);

                if (product != null)
                {
                    IDbContextTransaction transaction = _bagelSalesControlContext.Database.BeginTransaction();

                    _bagelSalesControlContext.Remove<ProductAgg>(product);
                    await _bagelSalesControlContext.SaveChangesAsync();
                    transaction.Commit();
                    _bagelSalesControlContext.Dispose();

                    return new Response { Message = "Product removed successfully" };
                }
            }

            _bagelSalesControlContext.Dispose();
            return new Response { Notification = "ProductId is requerid" };
        }

        public async Task<Response> SaveProduct(ProductRequest productRequest)
        {
            if (productRequest == null) throw new ArgumentNullException(nameof(productRequest));

            ProductAgg product = null;
            ControlTransactionFields transactionInfo = TransactionInfo.GetTransactionData(productRequest.UserName);
            IDbContextTransaction transaction = _bagelSalesControlContext.Database.BeginTransaction();

            if (productRequest.ProductId != 0)
            {
                product = _bagelSalesControlContext.Product.ToList().Find(p => p.ProductId == productRequest.ProductId);
            }

            if (product == null)
            {
                product = MaterializeProduct(productRequest, transactionInfo);

                await _bagelSalesControlContext.AddAsync<ProductAgg>(product);
            }
            else
            {
                product.NameProduct = productRequest.NameProduct;
                product.InitialExistence = productRequest.InitialExistence;
                product.Existence = productRequest.Existence;
                product.PurchasePrice = productRequest.PurchasePrice;
                product.SalePrice = productRequest.SalePrice;
                product.Wholesaleprice = productRequest.Wholesaleprice;
                product.TransactionModificationDate = DateTime.Now;
            }

            await _bagelSalesControlContext.SaveChangesAsync();
            transaction.Commit();

            _bagelSalesControlContext.Dispose();

            return new Response { Data = product, Message = "Product Saved Successfully" };

        }

        private ProductAgg MaterializeProduct(ProductRequest productRequest, ControlTransactionFields transactionInfo)
        {
            return new ProductAgg
            {
                NameProduct = productRequest.NameProduct,
                InitialExistence = productRequest.InitialExistence,
                Existence = productRequest.InitialExistence,
                PurchasePrice = productRequest.PurchasePrice,
                SalePrice = productRequest.SalePrice,
                Wholesaleprice = productRequest.Wholesaleprice,
                CreatedBy = transactionInfo.CreatedBy,
                TransactionDate = transactionInfo.TransactionDate,
                TransactionModificationDate = DateTime.Now
            };
        }
    }
}