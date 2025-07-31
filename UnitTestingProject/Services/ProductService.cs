﻿using UnitTestingProject.Entities;

namespace UnitTestingProject.Services
{
    public class ProductService : IProductService
    {
        private static List<Product> products = new List<Product> { 
            new Product{
                Id=1,
                Name="Acer",
                Price=3200
            },
            new Product{
                Id=2,
                Name="Apple",
                Price=850
            },
        };

        public Product Add(Product product)
        {
            products.Add(product);
            return product;
        }

        public void Delete(int id)
        {
            var item = products.FirstOrDefault(p => p.Id == id);
            if(item != null)
            {
                products.Remove(item);
            }
        }

        public Product GetProductById(int id)
        {
            return products.FirstOrDefault(p => p.Id == id);

        }

        public IEnumerable<Product> GetProducts(int top = 0)
        {
            return top == 0 ? products : (products.Count() > 0 ? products.Take(top) : new List<Product>());
        }

        public Product Update(Product product)
        {
            var item = products.FirstOrDefault(p => p.Id == product.Id);
            if(item != null)
            {
                item.Name = product.Name;
                item.Price = product.Price;
            }
            return product;
        }
    }
}
