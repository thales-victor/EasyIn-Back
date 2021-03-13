using EasyIn.Models;
using EasyIn.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EasyIn.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get(int id)
        {
            var user = await _userRepository.GetById(id);

            if (user == null)
                return NoContent();

            var result = new UserModel(user);

            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Post(UserUpdateModel model)
        {
            if (await _userRepository.AlreadyExists(model.Email))
                return BadRequest("Usuário já cadastrado");

            if (model.Password != model.ConfirmPassword)
                return BadRequest("Senha e confirmação de senha não coincidem");

            var user = await _userRepository.Add(new User(model.Email, model.Username, model.Password));

            var result = new UserModel(user);

            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult> Put(UserUpdateModel model)
        {
            var user = await _userRepository.GetById(model.Id);

            if (user == null)
                return NoContent();

            user.Update(model.Email, model.Username, model.Password);

            await _userRepository.Update(user);

            var result = new UserModel(user);

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var user = await _userRepository.GetById(id);

            if (user == null)
                return NoContent();

            user.Remove();

            await _userRepository.Update(user);

            return Ok();
        }
    }
}
