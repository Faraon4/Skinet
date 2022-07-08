using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static  async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try 
            {
                if(!context.ProductBrands.Any())
                {
                    // Read the data from a json file
                    var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                    // Serialize the data into a list
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    // Each item from the list we add to the database
                    foreach (var item in brands)
                    {
                        context.ProductBrands.Add(item);
                    }
                    // Save the database 
                    await context.SaveChangesAsync();

                }

                 if(!context.ProductTypes.Any())
                {
                    // Read the data from a json file
                    var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                    // Serialize the data into a list
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    // Each item from the list we add to the database
                    foreach (var item in types)
                    {
                        context.ProductTypes.Add(item);
                    }
                    // Save the database 
                    await context.SaveChangesAsync();

                }

                 if(!context.Products.Any())
                {
                    // Read the data from a json file
                    var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                    // Serialize the data into a list
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    // Each item from the list we add to the database
                    foreach (var item in products)
                    {
                        context.Products.Add(item);
                    }
                    // Save the database 
                    await context.SaveChangesAsync();

                }
            }
            catch(Exception ex)
            {
                    var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                    logger.LogError(ex.Message);
            }
        }
    }
}