using Microsoft.AspNetCore.Mvc;
using TrybeHotel.Models;
using TrybeHotel.Repository;
using TrybeHotel.Dto;
using TrybeHotel.Services;

namespace TrybeHotel.Controllers
{
    [ApiController]
    [Route("login")]

    public class LoginController : Controller
    {

        private readonly IUserRepository _repository;
        public LoginController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginDto login){
           var successLogin = _repository.Login(login);
           if (successLogin == null) 
           {
            return Unauthorized(new { Message = "Incorrect e-mail or password" });  
           }
            TokenGenerator tokenGenerator = new();
            var token = tokenGenerator.Generate(successLogin);
            return Ok(new { token });
        }
    }
}