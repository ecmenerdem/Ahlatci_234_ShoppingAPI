using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shopping.Business.Abstract;
using ShoppingAPI.Api.Validation.FluentValidation;
using ShoppingAPI.Entity.DTO.Category;
using ShoppingAPI.Entity.DTO.User;
using ShoppingAPI.Entity.Poco;
using ShoppingAPI.Entity.Result;
using ShoppingAPI.Helper.CustomException;
using System.Net;

namespace ShoppingAPI.Api.Controllers
{
    [ApiController]
    [Route("ShoppingAPI/[action]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("/AddUser")]
        [ProducesResponseType(typeof(Sonuc<UserDTOResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddUser(UserDTORequest userDTORequest)
        {
            
                UserRegisterValidator userValidator = new();

                if (userValidator.Validate(userDTORequest).IsValid)
                {
                    User user = _mapper.Map<User>(userDTORequest);

                    await _userService.AddAsync(user);

                    UserDTOResponse userDTOResponse = _mapper.Map<UserDTOResponse>(user);
                    return Ok(Sonuc<UserDTOResponse>.SuccessWithData(userDTOResponse));
                }
                else
                {
                    List<string> validationMessages = new();
                    for (int i = 0; i < userValidator.Validate(userDTORequest).Errors.Count; i++)
                    {
                        validationMessages.Add(userValidator.Validate(userDTORequest).Errors[i].ErrorMessage);
                    }

                throw new FieldValidationException(validationMessages);
            }
          

            
         
        }

        [HttpGet("/User/{guid}")]
        [ProducesResponseType(typeof(Sonuc<UserDTOResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUser(Guid guid)
        {
            var user = await _userService.GetAsync(q => q.GUID == guid);
            if (user != null)
            {
                UserDTOResponse userDTOResponse = _mapper.Map<UserDTOResponse>(user);


                return Ok(Sonuc<UserDTOResponse>.SuccessWithData(userDTOResponse));

            }
            else
            {
                return NotFound(Sonuc<UserDTOResponse>.SuccessNoDataFound());
                    
            }

        }

        [HttpGet("/Users")]
        [ProducesResponseType(typeof(Sonuc<List<UserDTOResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllAsync();

            List<UserDTOResponse> usersDTOResponseList = new List<UserDTOResponse>();

            foreach (var user in users)
            {
                var userDTO = _mapper.Map<UserDTOResponse>(user);
                usersDTOResponseList.Add(userDTO);
            }
            return Ok(Sonuc<List<UserDTOResponse>>.SuccessWithData(usersDTOResponseList));
        }

    }
}
