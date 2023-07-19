using Microsoft.AspNetCore.Mvc;
using Shopping.Business.Abstract;
using ShoppingAPI.Entity.DTO.User;
using ShoppingAPI.Entity.Poco;
using ShoppingAPI.Entity.Result;
using System.Net;

namespace ShoppingAPI.Api.Controllers
{
    [ApiController]
    [Route("ShoppingAPI/[action]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("/AddUser")]
        [ProducesResponseType(typeof(Sonuc<UserDTOResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddUser(UserDTORequest userDTORequest)
        {
            User user = new()
            {
                FirstName = userDTORequest.FirstName,
                LastName = userDTORequest.LastName,
                Username = userDTORequest.Username,
                Password = userDTORequest.Password,
                Email = userDTORequest.Email,
                PhoneNumber = userDTORequest.PhoneNumber,
                Adress = userDTORequest.Adress,
            };

            await _userService.AddAsync(user);

            UserDTOResponse userDTOResponse = new()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Password = user.Password,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Adress = user.Adress,
                Guid = user.GUID
            };
            return Ok(Sonuc<UserDTOResponse>.SuccessWithData(userDTOResponse));
        }

        [HttpGet("/User/{guid}")]
        [ProducesResponseType(typeof(Sonuc<UserDTOResponse>), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> GetUser(Guid guid)
        {
            var user = await _userService.GetAsync(q => q.GUID == guid);
            if (user != null)
            {
                UserDTOResponse userDTOResponse = new()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username,
                    Password = user.Password,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Adress = user.Adress,
                    Guid = user.GUID
                };

                return Ok(Sonuc<UserDTOResponse>.SuccessWithData(userDTOResponse));

            }
            else
            {
                return NotFound(Sonuc<UserDTOResponse>.SuccessNoDataFound());
                    
            }

        }
    }
}
