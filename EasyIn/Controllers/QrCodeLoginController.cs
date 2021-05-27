using EasyIn.Models;
using EasyIn.Repositories.Interfaces;
using EasyIn.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EasyIn.Controllers
{
    [ApiController]
    [Route("api/QrCode")]
    public class QrCodeLoginController : BaseController
    {
        private readonly IQrCodeLoginRepository _qrCodeLoginRepository;

        public QrCodeLoginController(IUserRepository userRepository,
                                     IQrCodeLoginRepository qrCodeLoginRepository)
                                     : base(userRepository)
        {
            _qrCodeLoginRepository = qrCodeLoginRepository;
        }

        [HttpGet("Login")]
        public async Task<ActionResult> GetQrCodeLogin([FromQuery] QrCodeLoginModel model)
        {
            if (!IsValidQrCodeLoginModel(model))
                return BadRequest(new ResponseError("Parâmetros inválidos"));

            var login = await _qrCodeLoginRepository.Get(model.PlatformId, model.BrowserToken);

            if (login == null)
                return BadRequest(new ResponseError("Tentativa de login não encontrada ou já expirou"));

            var result = new QrCodeLoginResultModel(login.Credential.Username, login.Credential.Password);

            return Ok(result);
        }

        [HttpPost("Login")]
        [Authorize]
        public async Task<ActionResult> QrCodeLogin(QrCodeLoginModel model)
        {
            if (!IsValidQrCodeLoginModel(model))
                return BadRequest(new ResponseError("Parâmetros inválidos"));

            var user = await User.ToEntityUser();
            var credentials = user.GetCredentials(model.PlatformId, model.CredentialId);

            if (!credentials.Any())
                return BadRequest(new ResponseError("Nenhuma credencial encontrada para esta plataforma"));
            else if (credentials.Count > 1)
                return Accepted("", new MoreThenOneCredentialError(credentials));

            var credentialToLogin = credentials.First();

            var login = new QrCodeLogin(credentialToLogin, model.BrowserToken);

            await _qrCodeLoginRepository.Add(login);

            return Created("", null);
        }

        private bool IsValidQrCodeLoginModel(QrCodeLoginModel model)
        {
            if (model.PlatformId == 0)
                return false;
            if (string.IsNullOrWhiteSpace(model.BrowserToken))
                return false;

            return true;
        }
    }
}
