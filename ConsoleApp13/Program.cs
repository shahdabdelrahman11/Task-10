using ConsoleApp13.Data;
using ConsoleApp13.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp13
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ApplicationDbContext _context = new ApplicationDbContext();

            // 1- List all customers' first and last names along with their email addresses.
           
            var customers = _context.Customers
                .Select(c => new
                {
                    c.FirstName,
                    c.LastName,
                    c.Email
                });
              

            foreach (var item in customers)
                Console.WriteLine($"{item.FirstName} , {item.LastName} , {item.Email}");


            // 2- Retrieve all orders processed by a specific staff member (staff_id = 3).

            var ordersStaff = _context.Orders
                .Where(o => o.StaffId == 3);
                

            foreach (var item in ordersStaff)
                Console.WriteLine($"{item.StaffId} , {item.OrderId}");


            // 3- Get all products that belong to a category named "Mountain Bikes".

            var BikeProducts = _context.Products
                .Where(p => p.Category.CategoryName == "Mountain Bikes");
                

            foreach (var item in BikeProducts)
                Console.WriteLine($"{item.ProductId} , {item.ProductName}");


            // 4- Count the total number of orders per store.

            var orderCounts = _context.Orders
                .GroupBy(o => o.StoreId)
                .Select(g => new
                {
                    StoreId = g.Key,
                    Count = g.Count()
                });
               

            foreach (var item in orderCounts)
                Console.WriteLine($"{item.StoreId} , {item.Count}");


            // 5- List all orders that have not been shipped yet (shipped_date is null).
            var pendOrders = _context.Orders
                .Where(o => o.ShippedDate == null);
                

            foreach (var item in pendOrders)
                Console.WriteLine($"{item.OrderId} , {item.CustomerId}");


            // 6- Display each customer's full name and the number of orders they have placed.

            var customerOrders = _context.Customers
                .Select(c => new
                {
                    FullName = c.FirstName + " " + c.LastName,
                    OrderCount = c.Orders.Count()
                });
               

            foreach (var item in customerOrders)
                Console.WriteLine($"{item.FullName} : {item.OrderCount}");


            // 7- List all products that have never been ordered (not found in order_items).

            var NOrderProducts = _context.Products
                .Where(p => !p.OrderItems.Any());
            

            foreach (var item in NOrderProducts)
                Console.WriteLine($"{item.ProductId} , {item.ProductName}");


            // 8- Display products that have a quantity of less than 5 in any store stock.

            var lowProducts = _context.Products
                .Where(p => p.Stocks.Any(s => s.Quantity < 5));
                

            foreach (var item in lowProducts)
                Console.WriteLine($"{item.ProductId} : {item.ProductName}");


            // 9- Retrieve the first product from the products table.
          
            var firstProduct = _context.Products.FirstOrDefault();
            if (firstProduct != null)
                Console.WriteLine($"{firstProduct.ProductId} , {firstProduct.ProductName}");


            // 10- Retrieve all products from the products table with a certain model year.

            var productsYear = _context.Products
                .Where(p => p.ModelYear == 2018);
                

            foreach (var item in productsYear)
                Console.WriteLine($"{item.ProductId} , {item.ProductName} , {item.ModelYear}");


            // 11- Display each product with the number of times it was ordered.
            var productOrderCount = _context.Products
                .Select(p => new
                {
                    ProductName = p.ProductName,
                    OrderCount = p.OrderItems.Count()
                });
              

            foreach (var item in productOrderCount)
                Console.WriteLine($"{item.ProductName} , {item.OrderCount}");


            // 12- Count the number of products in a specific category.
         
            int categoryProductCount = _context.Products
                .Where(p => p.CategoryId == 1)
                .Count();

            Console.WriteLine(categoryProductCount);


            // 13- Calculate the average list price of products.
          
            var averagePrice = _context.Products.Average(p => p.ListPrice);
            Console.WriteLine(averagePrice);


            // 14- Retrieve a specific product from the products table by ID.
          
            var productId = _context.Products.Find(5);
            if (productId != null)
                Console.WriteLine($"{productId.ProductId} , {productId.ProductName}");


            // 15- List all products that were ordered with a quantity greater than 3 in any order.

            var highQuantity = _context.OrderItems
                .Where(oi => oi.Quantity > 3)
                .Select(oi => oi.Product)
                .Distinct();
                

            foreach (var item in highQuantity)
                Console.WriteLine($"{item.ProductId} , {item.ProductName}");


            // 16- Display each staff member's name and how many orders they processed.

            var staffOrders = _context.Staffs
                .Select(s => new
                {
                    StaffName = s.FirstName + " " + s.LastName,
                    OrdersCount = s.Orders.Count()
                });
              

            foreach (var item in staffOrders)
                Console.WriteLine($"{item.StaffName} , {item.OrdersCount}");


            // 17- List active staff members only (active = 1) along with their phone numbers.

            var activeStaff = _context.Staffs
                .Where(s => s.Active == 1);
                

            foreach (var item in activeStaff)
                Console.WriteLine($"{item.FirstName} {item.LastName} , {item.Phone}");


            // 18- List all products with their brand name and category name.

            var productsWithBrandCategory = _context.Products
                .Select(p => new
                {
                    ProductName = p.ProductName,
                    BrandName = p.Brand.BrandName,
                    CategoryName = p.Category.CategoryName
                });
                

            foreach (var item in productsWithBrandCategory)
                Console.WriteLine($"{item.ProductName} , {item.BrandName} , {item.CategoryName}");


            // 19- Retrieve orders that are completed.

            var completedOrders = _context.Orders
                .Where(o => o.OrderStatus == 3);
               
            foreach (var item in completedOrders)
                Console.WriteLine($"{item.OrderId} , {item.OrderStatus} , {item.OrderDate}");


            // 20- List each product with the total quantity sold (sum of quantity from order_items).

            var totalQuantity = _context.Products
                .Select(p => new
                {
                    ProductName = p.ProductName,
                    TotalQuantitySold = p.OrderItems.Sum(oi => oi.Quantity)
                });
                

            foreach (var item in totalQuantity)
                Console.WriteLine($"{item.ProductName} , {item.TotalQuantitySold}");


            
        }
    }
}