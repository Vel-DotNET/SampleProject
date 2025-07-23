using BusinessEntities;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories
{
    [AutoRegister]
    public class OrderRepository : IOrderRepository
    {
        private IList<Order> _lstOrder = new List<Order>();

        public OrderRepository()
        {
            _lstOrder.Add(new Order
            {
                Id = 120,
                OrderDate = "22/07/2025",
                OrderTotal = 345,
                Status = "Payment Complete",
                CustomerID = 134,
                ProductID = 78,
            });
            _lstOrder.Add(new Order
            {
                Id = 156,
                OrderDate = "15/07/2025",
                OrderTotal = 690,
                Status = "Pending Delivery",
                CustomerID = 134,
                ProductID = 54,
            });

        }

        public Order Save(Order entity)
        {
            _lstOrder.Add(entity);
            return Get(entity.Id);
        }

        public Order Update(Order entity)
        {
            return _lstOrder.Where(S => S.Id == entity.Id)
             .Select(S =>
             {
                 S.OrderDate = entity.OrderDate; S.OrderTotal = entity.OrderTotal; S.Status = entity.Status;
                 S.ProductID = entity.ProductID; return S;
             }).FirstOrDefault();
        }


        public void Delete(int id)
        {
            _lstOrder.Remove(Get(id));
        }

        public Order Get(int id)
        {
            return _lstOrder.Where(S => S.Id == id).FirstOrDefault();
        }

        public IEnumerable<Order> GetAll()
        {
            return _lstOrder;
        }
    }
}
