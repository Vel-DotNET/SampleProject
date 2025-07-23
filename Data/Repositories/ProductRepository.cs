using BusinessEntities;
using Common;
using Microsoft.Extensions.Caching.Memory;
using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories
{
    [AutoRegister]
    public class ProductRepository : IProductRepository
    {
        // private readonly ApplicationDBContext _context;
        private IList<Product> _lstProduct = new List<Product>();

        public ProductRepository()
        {

         //Considering  in Memory storage and aswell with the current Target version 2.0,
         //I used simple List with two Collections in it for the purpose CRUD operations
            _lstProduct.Add(new Product
            {
                Id = 120,
                Name = "Elantra",
                Description = "Hyundai Elantra",
                Price = 1000,
                Model = "Basic",
                Color = "White",
                Category = "Sedan",
                Imageurl = ""
            });
            _lstProduct.Add(new Product
            {
                Id = 127,
                Name = "innova",
                Description = "Toyota innova",
                Price = 4500,
                Model = "TopEnd",
                Color = "White",
                Category = "SUV",
                Imageurl = ""
            });


        }

        public Product Save(Product entity)
        {
            _lstProduct.Add(entity);
            return Get(entity.Id);
        }

        public Product Update(Product entity)
        {
            return _lstProduct.Where(S => S.Id == entity.Id)
              .Select(S =>
              {
                  S.Name = entity.Name; S.Description = entity.Description; S.Price = entity.Price;
                  S.Model = entity.Model; S.Color = entity.Color; S.Category = entity.Category; return S;
              }).FirstOrDefault();

        }


        public void Delete(int id)
        {
            _lstProduct.Remove(Get(id));
        }

        public Product Get(int id)
        {
            return _lstProduct.Where(S => S.Id == id).FirstOrDefault();
        }

        public IEnumerable<Product> GetAll()
        {
            return _lstProduct;
        }
    }
}
