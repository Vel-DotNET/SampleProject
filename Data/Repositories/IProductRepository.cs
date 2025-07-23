using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public interface IProductRepository
    {

        Product Save(Product entity);
        Product Update(Product entity);
        void Delete(int entity);
        Product Get(int id);
        IEnumerable<Product> GetAll();

    }
}
