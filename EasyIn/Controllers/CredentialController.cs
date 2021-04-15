using EasyIn.Models;
using EasyIn.Repositories.Interfaces;
using EasyIn.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EasyIn.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/credential")]
    public class CredentialController : BaseController
    {
        private readonly ICredentialRepository _credentialRepository;
        private readonly IPlatformRepository _platformRepository;

        public CredentialController(ICredentialRepository credentialRepository,
                                    IPlatformRepository platformRepository,
                                    IUserRepository userRepository) : base(userRepository)
        {
            _credentialRepository = credentialRepository;
            _platformRepository = platformRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var credentials = await _credentialRepository.GetByUserId(User.GetId());

            if (credentials == null)
                return NoContent();

            var result = credentials.Select(a => new CredentialListModel(a)).ToList();

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get(int id)
        {
            var credential = await _credentialRepository.GetById(id);

            if (!User.IsOwner(credential?.User))
                return NoContent();

            var result = new CredentialModel(credential);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Post(CredentialUpdateModel model)
        {
            if (!await IsValidCredential(model))
                return BadRequest(new ResponseError("Parâmetros inválidos"));

            var platform = await _platformRepository.GetById(model.PlatformId);
            var authUser = await User.ToEntityUser();

            var credential = await _credentialRepository.Add(new Credential(authUser, platform, model.Username, model.Password));

            var result = new CredentialModel(credential);

            return Created("", result);
        }

        private async Task<bool> IsValidCredential(CredentialUpdateModel model, bool ignoreConfirmPassword = false)
        {
            if (string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.Password))
                return false;

            if (!ignoreConfirmPassword && string.IsNullOrWhiteSpace(model.ConfirmPassword))
                return false;

            if (model.Id != 0)
            {
                var credential = await _credentialRepository.GetById(model.Id);
                if (!User.IsOwner(credential?.User))
                    return false;
            }

            return true;
        }

        [HttpPut]
        public async Task<ActionResult> Put(CredentialUpdateModel model)
        {
            if (!await IsValidCredential(model, true))
                return BadRequest(new ResponseError("Parâmetros inválidos"));

            var credential = await _credentialRepository.GetById(model.Id);

            credential.Update(model.Username, model.Password);

            await _credentialRepository.Update(credential);

            var result = new CredentialModel(credential);

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var credential = await _credentialRepository.GetById(id);

            if (!User.IsOwner(credential?.User))
                return NoContent();

            credential.Remove();

            await _credentialRepository.Update(credential);

            return Ok();
        }
    }
}
