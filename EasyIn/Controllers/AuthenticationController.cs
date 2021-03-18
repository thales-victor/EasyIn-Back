using EasyIn.Models;
using EasyIn.Repositories.Interfaces;
using EasyIn.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EasyIn.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/user/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticationRepository _authenticationRepository;
        private IPlatformRepository _platformRepository;
            
        public AuthenticationController(IAuthenticationRepository authenticationRepository,
                                        IUserRepository userRepository,
                                        IPlatformRepository platformRepository)
        {
            _authenticationRepository = authenticationRepository;
            _platformRepository = platformRepository;
            OAuthUser.InitAuthUser(userRepository);
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var authentications = await _authenticationRepository.GetByUserId(User.GetId());

            if (authentications == null)
                return NoContent();

            var result = authentications.Select(a => new AuthenticationListModel(a)).ToList();

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get(int id)
        {
            var authentication = await _authenticationRepository.GetById(id);

            if (!User.IsOwner(authentication?.User))
                return NoContent();

            var result = new AuthenticationModel(authentication);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Post(AuthenticationUpdateModel model)
        {
            var platform = await _platformRepository.GetById(model.PlatformId);
            var authUser = await User.ToEntityUser();

            var authentication = await _authenticationRepository.Add(new Authentication(authUser, platform, model.Password));

            var result = new AuthenticationModel(authentication);

            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult> Put(AuthenticationUpdateModel model)
        {
            var authentication = await _authenticationRepository.GetById(model.Id);

            if (!User.IsOwner(authentication?.User))
                return NoContent();

            authentication.Update(model.Password);

            await _authenticationRepository.Update(authentication);

            var result = new AuthenticationModel(authentication);

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var authentication = await _authenticationRepository.GetById(id);

            if (!User.IsOwner(authentication?.User))
                return NoContent();

            authentication.Remove();

            await _authenticationRepository.Update(authentication);

            return Ok();
        }
    }
}
