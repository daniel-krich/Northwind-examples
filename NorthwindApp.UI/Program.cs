using Microsoft.EntityFrameworkCore;
using NorthwindApp.Data;
using NorthwindApp.Data.Context;
using NorthwindApp.Data.Entities;

using (var dbContext = new NorthwindContext())
{
    var productsCategory4 = (from prod in dbContext.Products
                             where prod.CategoryId == 4
                             select prod.ProductName).ToList();

    var productsCategoryAbove4 = (from prod in dbContext.Products
                                  where prod.CategoryId > 4
                                  select prod.ProductName).ToList();

    var productNamesWithCategoryNames = (from prod in dbContext.Products.Include(x => x.Category)
                                         //join cat in dbContext.Categories on prod.CategoryId equals cat.CategoryId
                                         select new { prod.ProductName, prod.Category!.CategoryName }).ToList();

    foreach (var productWithCategory in productNamesWithCategoryNames)
    {
        Console.WriteLine($"product: {productWithCategory.ProductName} | Category: {productWithCategory.CategoryName}");
    }

    Territory newTerritory = new Territory
    {
        RegionId = 3,
        TerritoryDescription = "Israel",
        TerritoryId = "Israel"
    };

    dbContext.Territories.Add(newTerritory);

    dbContext.SaveChanges();

    newTerritory.TerritoryDescription = "Changed Israel";
    dbContext.Territories.Update(newTerritory);

    dbContext.SaveChanges();

    foreach (var territory in dbContext.Territories.Where(x => x.RegionId == 3).Select(x => x.TerritoryDescription))
    {
        Console.WriteLine(territory);
    }

    dbContext.Territories.Remove(newTerritory);

    dbContext.SaveChanges();


    /*foreach (var prod in productsCategory4)
    {
        Console.WriteLine(prod);
    }

    Console.WriteLine();

    foreach (var prod in productsCategoryAbove4)
    {
        Console.WriteLine(prod);
    }*/

    /*var regions = dbContext.Regions.Include(x => x.Territories).ToList();

    regions.ForEach(region =>
    {
        var territories = region.Territories;

        Console.WriteLine(region.RegionDescription.Trim() + " -->");

        foreach(var territory in territories)
        {
            Console.WriteLine("\t" + territory.TerritoryDescription);
        }
        Console.WriteLine();
    });

    var order = new Order
    {
        CustomerId = "FRANK",
        EmployeeId = 6,
        OrderDate = DateTime.Now,
        ShipAddress = "7 Piccadilly Rd.",
        ShipCity = "New York",
        ShipCountry = "New York"
    };

    dbContext.Orders.Add(order);

    dbContext.SaveChanges();

    Console.WriteLine("order id: " + order.OrderId);

    dbContext.OrderDetails.AddRange(new[]
    {
        new OrderDetail
        {
            ProductId = 11,
            UnitPrice = 95,
            Quantity = 3,
            Order = order
        },
        new OrderDetail
        {
            ProductId = 56,
            UnitPrice = 47,
            Quantity = 6,
            Order = order
        },
        new OrderDetail
        {
            ProductId = 74,
            UnitPrice = 120,
            Quantity = 5,
            Order = order
        },
    });


    dbContext.SaveChanges();


    order.EmployeeId = 5;

    dbContext.SaveChanges();

    var orderDetailWithProductId56 = dbContext.OrderDetails.FirstOrDefault(x => x.ProductId == 56);
    if (orderDetailWithProductId56 != null)
    {
        dbContext.OrderDetails.Remove(orderDetailWithProductId56);
        dbContext.SaveChanges();
    }*/
}