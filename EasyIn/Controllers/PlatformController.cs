using EasyIn.Models;
using EasyIn.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace EasyIn.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/platform")]
    public class PlatformController : ControllerBase
    {
        private IPlatformRepository _platformRepository;

        public PlatformController(IPlatformRepository platformRepository)
        {
            _platformRepository = platformRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var platforms = await _platformRepository.GetAll();

            if (platforms == null)
                return NoContent();

            var result = platforms
                            .OrderByDescending(p => p.AllowIntegratedLogin)
                            .ThenBy(p => p.Name)
                            .Select(p => new PlatformModel(p))
                            .ToList();

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get(int id)
        {
            var platform = await _platformRepository.GetById(id);

            if (platform == null)
                return NoContent();

            var result = new PlatformModel(platform);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Post(PlatformModel model)
        {
            var platform = await _platformRepository.Add(new Platform(model.Name));

            var result = new PlatformModel(platform);

            return Created("", result);
        }

        [HttpPut]
        public async Task<ActionResult> Put(PlatformModel model)
        {
            var platform = await _platformRepository.GetById(model.Id);

            if (platform == null)
                return NoContent();

            platform.Update(model.Name);

            await _platformRepository.Update(platform);

            var result = new PlatformModel(platform);

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var platform = await _platformRepository.GetById(id);

            if (platform == null)
                return NoContent();

            platform.Remove();

            await _platformRepository.Update(platform);

            return Ok();
        }
    }
}
