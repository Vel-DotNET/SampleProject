using BusinessEntities;
using Common;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.Order
{

    [AutoRegister]
    public class OrderService : IOrderService
    {

        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public BusinessEntities.Order Create(BusinessEntities.Order order)
        {
            return _orderRepository.Save(order);
        }
        public BusinessEntities.Order Update(BusinessEntities.Order order)
        {
            return _orderRepository.Update(order);
        }

        public void Delete(int id)
        {
            _orderRepository.Delete(id);
        }

        public IEnumerable<BusinessEntities.Order> GetAllOrders()
        {
            return _orderRepository.GetAll();
        }

        public BusinessEntities.Order GetOrderByID(int id)
        {
            return _orderRepository.Get(id);
        }

        
    }
}
