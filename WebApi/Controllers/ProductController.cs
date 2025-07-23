using BusinessEntities;
using Common;
using Core.Services.Product;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace WebApi.Controllers
{
    [RoutePrefix("Product")]
    public class ProductController : BaseApiController
    {
        private readonly IProductService _productService;
        private APIResponse _response;
        public ProductController(IProductService productService)
        {
            _productService = productService;
            _response = new APIResponse();
        }

        [Route("Create")]
        [HttpPost]
        public APIResponse Create([FromBody] Product model)
        {
            try
            {
                var product = _productService.Create(model);
                _response.Result = product;
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
        public APIResponse Update([FromBody] Product model)
        {
            try
            {
                var product = _productService.GetProductByID(model.Id);
                if (product == null)
                {
                    _response.Message = "Product does not exist in the system";
                    _response.StatusCode = HttpStatusCode.NotFound;
                }
                else
                {
                    _productService.Update(model);
                    _response.Result = product;
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

        [Route("Delete/{prouctID:int}")]
        [HttpDelete]
        public APIResponse Delete(int prouctID)
        {
            try
            {
                var product = _productService.GetProductByID(prouctID);
                if (product == null)
                {
                    _response.Message = "Product does not exist in the system";
                    _response.StatusCode = HttpStatusCode.NotFound;
                }
                else
                {
                    _productService.Delete(prouctID);
                    _response.Message = "Product deleted from the system";
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

        [Route("Get/{productID:int}")]
        [HttpGet]
        public APIResponse GetProduct(int productID)
        {

            try
            {
                var product = _productService.GetProductByID(productID);
                _response.Result = product;
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

        [Route("GetAll")]
        ///{skip:int}/{take:int}
        [HttpGet]
        public APIResponse GetProducts(int skip, int take, string category = null, string color = null)
        {
            try
            {

                var lstOrder = _productService.GetProducts();
                if (lstOrder.Count() > 0)
                {
                    if (!string.IsNullOrEmpty(category))
                    {
                        lstOrder = lstOrder.Where(s => s.Category.ToLower() == category.ToLower());
                    }
                    if (!string.IsNullOrEmpty(color))
                    {
                        lstOrder = lstOrder.Where(s => s.Color.ToLower() == color.ToLower());
                    }
                }
                var product = lstOrder.Skip(skip).Take(take).ToList();

                _response.Result = product;
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