using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.Product
{
    public interface IProductService
    {

        BusinessEntities.Product Create(BusinessEntities.Product product);

        BusinessEntities.Product Update(BusinessEntities.Product product);

        void Delete(int id);
        IEnumerable<BusinessEntities.Product> GetProducts();

        BusinessEntities.Product GetProductByID(int id);

    }
}
