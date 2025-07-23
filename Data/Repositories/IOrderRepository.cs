using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public interface IOrderRepository
    {
        Order Save(Order entity);
        Order Update(Order entity);
        void Delete(int entity);
        Order Get(int id);
        IEnumerable<Order> GetAll();
    }
}
