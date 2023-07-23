using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Shopping.Business.Abstract;
using ShoppingAPI.Api.Validation.FluentValidation;
using ShoppingAPI.Entity.DTO.Category;
using ShoppingAPI.Entity.DTO.Login;
using ShoppingAPI.Entity.Result;
using ShoppingAPI.Helper.CustomException;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace ShoppingAPI.Api.Controllers
{
    [ApiController]
    [Route("[action]")]
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public LoginController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("/Login")]
        [ProducesResponseType(typeof(Sonuc<LoginResponseDTO>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> LoginAsync(LoginRequestDTO loginRequestDTO)
        {
            LoginValidator loginValidator = new();

            if (loginValidator.Validate(loginRequestDTO).IsValid)
            {
                var user = await _userService.GetAsync(q => q.Username == loginRequestDTO.KullaniciAdi && q.Password == loginRequestDTO.Sifre);

                if (user == null)
                {
                    return NotFound(Sonuc<LoginResponseDTO>.AuthenticationError());
                }
                else
                {
                    var key = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:JWTKey"));

                    var claims = new List<Claim>();
                    claims.Add(new Claim("KullaniciAdi", user.Username));
                    claims.Add(new Claim("KullaniciID", user.ID.ToString()));

                    var jwt = new JwtSecurityToken(
                        expires: DateTime.Now.AddDays(30),
                        claims: claims,
                        issuer: "http://aasfsdagfsd.com",
                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature));

                    var token = new JwtSecurityTokenHandler().WriteToken(jwt);

                    LoginResponseDTO loginResponseDTO = new()
                    {
                        Token = token
                    };

                    return Ok(Sonuc<LoginResponseDTO>.SuccessWithData(loginResponseDTO));

                }

            }
            else
            {
                List<string> validationMessages = new();
                for (int i = 0; i < loginValidator.Validate(loginRequestDTO).Errors.Count; i++)
                {
                    validationMessages.Add(loginValidator.Validate(loginRequestDTO).Errors[i].ErrorMessage);
                }

                //return BadRequest(Sonuc<CategoryDTOResponse>.FieldValidationError(validationMessages));

                throw new FieldValidationException(validationMessages);
            }



        }
    }
}
