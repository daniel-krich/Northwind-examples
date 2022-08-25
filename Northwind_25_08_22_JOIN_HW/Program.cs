using Microsoft.EntityFrameworkCore;
using NorthwindApp.Data;
using NorthwindApp.Data.Context;
using NorthwindApp.Data.Entities;

using (var dbContext = new NorthwindContext())
{
    // 1.

    var productNameAndQuantityList = (from product in dbContext.Products
                                      select new { product.ProductName, product.QuantityPerUnit }).ToList();

    // 2.

    var productIdAndNameList = (from product in dbContext.Products
                                where !product.Discontinued
                                select new { product.ProductId, product.ProductName }).ToList();

    // 3.

    var productIdAndNameDiscontinuedList = (from product in dbContext.Products
                                            where product.Discontinued
                                            select new { product.ProductId, product.ProductName }).ToList();

    // 4.

    var expensiveFirstProductList = (from product in dbContext.Products
                                     where !product.Discontinued
                                     orderby product.UnitPrice descending
                                     select new { product.ProductName, product.UnitPrice }).ToList();

    var cheapFirstProductList = (from product in dbContext.Products
                                 where !product.Discontinued
                                 orderby product.UnitPrice ascending
                                 select new { product.ProductName, product.UnitPrice }).ToList();

    // 5.

    var productListLessThen20Bucks = (from product in dbContext.Products
                                      where !product.Discontinued && product.UnitPrice < 20
                                      select new { product.ProductId, product.ProductName, product.UnitPrice }).ToList();

    // 6.

    var productListBetween15To25Bucks = (from product in dbContext.Products
                                         where !product.Discontinued && product.UnitPrice >= 15 && product.UnitPrice <= 25
                                         select new { product.ProductId, product.ProductName, product.UnitPrice }).ToList();

    // 7.

    var productListOfAboveAvgPrice = (from product in dbContext.Products
                                      where !product.Discontinued && product.UnitPrice > dbContext.Products.Average(x => x.UnitPrice)
                                      select new { product.ProductName, product.UnitPrice }).ToList();

    // 8.

    var tenMostExpensiveProductList = (from product in dbContext.Products
                                       where !product.Discontinued
                                       orderby product.UnitPrice descending
                                       select new { product.ProductName, product.UnitPrice }).Take(10).ToList();

    // 9.

    var countCurrentProducts = dbContext.Products.Where(x => !x.Discontinued).Count();
    var countDiscontinuedProducts = dbContext.Products.Where(x => x.Discontinued).Count();

    // 10.

    var productListOfLessThanQuantityOrder = (from product in dbContext.Products
                                              where product.UnitsInStock < product.UnitsOnOrder
                                              select new { product.ProductName, product.UnitsOnOrder, product.UnitsInStock }).ToList();
}