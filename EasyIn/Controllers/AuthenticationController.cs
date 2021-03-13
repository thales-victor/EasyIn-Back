using EasyIn.Models;
using EasyIn.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace EasyIn.Controllers
{
    [ApiController]
    [Route("api/user/{userId:int}/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticationRepository _authenticationRepository;
        private IUserRepository _userRepository;
        private IPlatformRepository _platformRepository;

        public AuthenticationController(IAuthenticationRepository authenticationRepository,
                                        IUserRepository userRepository,
                                        IPlatformRepository platformRepository)
        {
            _authenticationRepository = authenticationRepository;
            _userRepository = userRepository;
            _platformRepository = platformRepository;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get(int userId, int id)
        {
            var authentication = await _authenticationRepository.GetById(id);

            if (authentication?.User?.Id != userId)
                return NoContent();

            var result = new AuthenticationModel(authentication);

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetAll(int userId)
        {
            var authentications = await _authenticationRepository.GetByUserId(userId);

            if (authentications == null)
                return NoContent();

            var result = authentications.Select(a => new AuthenticationListModel(a)).ToList();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Post(AuthenticationUpdateModel model)
        {
            var user = await _userRepository.GetById(model.UserId);
            var platform = await _platformRepository.GetById(model.PlatformId);

            var authentication = await _authenticationRepository.Add(new Authentication(user, platform, model.Password));

            var result = new AuthenticationModel(authentication);

            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult> Put(AuthenticationUpdateModel model)
        {
            var authentication = await _authenticationRepository.GetById(model.Id);

            if (authentication?.User?.Id != model.UserId)
                return NoContent();

            authentication.Update(model.Password);

            await _authenticationRepository.Update(authentication);

            var result = new AuthenticationModel(authentication);

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int userId, int id)
        {
            var authentication = await _authenticationRepository.GetById(id);

            if (authentication?.User?.Id != userId)
                return NoContent();

            authentication.Remove();

            await _authenticationRepository.Update(authentication);

            return Ok();
        }
    }
}
