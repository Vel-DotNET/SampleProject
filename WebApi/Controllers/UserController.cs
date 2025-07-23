using BusinessEntities;
using Common;
using Core.Services.Users;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using WebApi.Models.Users;

namespace WebApi.Controllers
{
    [RoutePrefix("users")]
    public class UserController : BaseApiController
    {
        private readonly ICreateUserService _createUserService;
        private readonly IDeleteUserService _deleteUserService;
        private readonly IGetUserService _getUserService;
        private readonly IUpdateUserService _updateUserService;
        private APIResponse _response;
        public UserController(ICreateUserService createUserService, IDeleteUserService deleteUserService, IGetUserService getUserService, IUpdateUserService updateUserService)
        {
            _createUserService = createUserService;
            _deleteUserService = deleteUserService;
            _getUserService = getUserService;
            _updateUserService = updateUserService;
            _response = new APIResponse();
        }

        [Route("CreateUser")]
        [HttpPost]
        public APIResponse CreateUser([FromBody] UserModel model)
        {
            try
            {
                //To check whether the User with Email and Name are already exists in system?
                var userdetails = _getUserService.GetUsers(null, model.Name, model.Email);
                if (userdetails != null && userdetails.Count() > 0)
                {
                    _response.Message = "User already Exists with the Name or EmailID";
                    _response.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    var userId = Guid.NewGuid();
                    var user = _createUserService.Create(userId, model.Name, model.Email, model.Type, model.AnnualSalary, model.Tags);
                    _response.Result = user;
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

        [Route("{userId:guid}/update")]
        [HttpPost]
        public APIResponse UpdateUser(Guid userId, [FromBody] UserModel model)
        {
            try
            {
                var user = _getUserService.GetUser(userId);
                if (user == null)
                {
                    _response.Message = "User does not exist in the system";
                    _response.StatusCode = HttpStatusCode.NotFound;
                }
                else
                {
                    _updateUserService.Update(user, model.Name, model.Email, model.Type, model.AnnualSalary, model.Tags);
                    _response.Result = user;
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

        [Route("{userId:guid}/delete")]
        [HttpDelete]
        public APIResponse DeleteUser(Guid userId)
        {
            try
            {
                var user = _getUserService.GetUser(userId);
                if (user == null)
                {
                    _response.Message = "User does not exist in the system";
                    _response.StatusCode = HttpStatusCode.NotFound;
                }
                else
                {
                    _deleteUserService.Delete(user);
                    _response.Message = "User deleted from the system";
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

        [Route("{userId:guid}")]
        [HttpGet]
        public APIResponse GetUser(Guid userId)
        {

            try
            {
                var user = _getUserService.GetUser(userId);
                _response.Result = user;
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

        [Route("list")]
        [HttpGet]
        public APIResponse GetUsers(int skip, int take, UserTypes? type = null, string name = null, string email = null)
        {
            try
            {
                var users = _getUserService.GetUsers(type, name, email)
                                           .Skip(skip).Take(take)
                                           .Select(q => new UserData(q))
                                           .ToList();
                _response.Result = users;
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

        [Route("clear")]
        [HttpDelete]
        public APIResponse DeleteAllUsers()
        {

            try
            {
                _deleteUserService.DeleteAll();
                _response.StatusCode = HttpStatusCode.OK;
                _response.Message = "All Users are Deleted";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [Route("list/tag")]
        [HttpGet]
        public APIResponse GetUsersByTag(string tag)
        {
            try
            {
                var userdetails = _getUserService.GetUsers(null, null, null);
                var LstUser = userdetails.Where(s => s.Tags.Contains(tag)).ToList();
                _response.Result = LstUser;
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