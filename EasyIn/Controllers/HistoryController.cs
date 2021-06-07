using EasyIn.Models;
using EasyIn.Repositories.Interfaces;
using EasyIn.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Threading.Tasks;

namespace EasyIn.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/history")]
    public class HistoryController : BaseController
    {
        private IQrCodeLoginRepository _qrCodeLoginRepository;
        public HistoryController(IQrCodeLoginRepository qrCodeLoginRepository,
                                 IUserRepository userRepository)
                                 : base(userRepository)
        {
            _qrCodeLoginRepository = qrCodeLoginRepository;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var histories = await _qrCodeLoginRepository.GetAllByUser(User.GetUserId().Value);

            if (!histories.Any())
                return NoContent();

            var result = histories.Select(h => new HistoryModel(h)).ToList();

            return Ok(result);
        }
    }
}
