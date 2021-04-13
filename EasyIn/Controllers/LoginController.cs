using EasyIn.Models;
using EasyIn.Repositories.Interfaces;
using EasyIn.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EasyIn.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        private ILoginRepository _loginRepository;

        public LoginController(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        [HttpPost]
        public async Task<ActionResult<dynamic>> Login(UserLoginModel model)
        {
            var user = await _loginRepository.Get(model.Username, model.Password);

            if (user == null)
                return BadRequest(new ResponseError("Usuário ou senha inválidos"));

            var token = TokenService.GenerateToken(user);

            return Ok(new
            {
                user = new UserModel(user),
                token = token
            });
        }
    }
}
