using BusinessEntities;
using Common;
using Core.Services.Order;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace WebApi.Controllers
{
    [RoutePrefix("Orders")]
    public class OrderController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private APIResponse _response;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
            _response = new APIResponse();
        }

        [Route("Create")]
        [HttpPost]
        public APIResponse Create([FromBody] Order model)
        {
            try
            {
                var order = _orderService.Create(model);
                _response.Result = order;
                _response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [Route("update")]
        [HttpPost]
        public APIResponse Update([FromBody] Order model)
        {
            try
            {
                var order = _orderService.GetOrderByID(model.Id);
                if (order == null)
                {
                    _response.Message = "Order details does not exist in the system";
                    _response.StatusCode = HttpStatusCode.NotFound;
                }
                else
                {
                    var Order = _orderService.Update(model);
                    _response.Result = Order;
                    _response.StatusCode = HttpStatusCode.OK;
                }

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [Route("Delete/{orderID:int}")]
        [HttpDelete]
        public APIResponse Delete(int orderID)
        {
            try
            {
                var Order = _orderService.GetOrderByID(orderID);
                if (Order == null)
                {
                    _response.Message = "Order details does not exist in the system";
                    _response.StatusCode = HttpStatusCode.NotFound;
                }
                else
                {
                    _orderService.Delete(orderID);
                    _response.Message = "Order deleted from the system";
                    _response.StatusCode = HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [Route("GetOrderByID/{orderID:int}")]
        [HttpGet]
        public APIResponse Get(int orderID)
        {

            try
            {
                var order = _orderService.GetOrderByID(orderID);
                _response.Result = order;
                _response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [Route("GetOrders")]
        [HttpGet]
        public APIResponse GetAll(int skip, int take, string Status = null)
        {
            try
            {
                var lstOrder = _orderService.GetAllOrders();
                if (lstOrder.Count() > 0 && !string.IsNullOrEmpty(Status))
                {
                    lstOrder = lstOrder.Where(s => s.Status.ToLower() == Status.ToLower());
                }
                lstOrder = lstOrder.Skip(skip).Take(take).ToList();
                _response.Result = lstOrder;
                _response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}