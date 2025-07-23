using BusinessEntities;
using Common;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.Product
{
    [AutoRegister]
    public class ProductService : IProductService
    {

        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public BusinessEntities.Product Create(BusinessEntities.Product product)
        {
           return  _productRepository.Save(product);
        }

        public BusinessEntities.Product Update(BusinessEntities.Product product)
        {
            return _productRepository.Update(product);
        }

        public void Delete(int id)
        {
            _productRepository.Delete(id);
        }

        public IEnumerable<BusinessEntities.Product> GetProducts()
        {
            return _productRepository.GetAll();
        }

        public BusinessEntities.Product GetProductByID(int id)
        {
            return _productRepository.Get(id);
        }
    }
}
