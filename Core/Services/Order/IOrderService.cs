using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.Order
{
    public interface IOrderService
    {
        BusinessEntities.Order Create(BusinessEntities.Order order);
        BusinessEntities.Order Update(BusinessEntities.Order order);
        void Delete(int id);
        IEnumerable<BusinessEntities.Order> GetAllOrders();
        BusinessEntities.Order GetOrderByID(int id);

    }
}
