using EasyIn.Models;
using EasyIn.Repositories.Interfaces;
using EasyIn.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Threading.Tasks;

namespace EasyIn.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        private ILoginRepository _loginRepository;
        private IHttpClientFactory _clientFactory;

        public LoginController(ILoginRepository loginRepository, IHttpClientFactory clientFactory)
        {
            _loginRepository = loginRepository;
            _clientFactory = clientFactory;
        }

        [HttpGet]
        public ActionResult Teste()
        {
            return Ok(new { message = "Parabéns" });
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginRequestModel model)
        {
            var user = await _loginRepository.Get(model.Username, model.Password);

            if (user == null)
                return BadRequest(new ResponseError("Usuário ou senha inválidos"));

            var token = TokenService.GenerateToken(user);

            return Ok(new LoginResultModel(user, token));
        }

        [HttpPost("qrcode")]
        public async Task<ActionResult> QrCodeLogin(QrCodeLoginModel model)
        {
            var result = await GetCredentials(model.PlatformId, model.BrowserToken);
            if (result != null)
            {
                return await Login(result);
            }

            return BadRequest(new ResponseError("Não foi possível conectar. Tente novamente"));
        }

        private async Task<LoginRequestModel> GetCredentials(int platformId, string browserToken)
        {
            var uri = $"https://192.168.0.21:44347/api/qrcode/login?platformId={platformId}&browserToken={browserToken}";

            using var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true;

            using var httpClient = new HttpClient(httpClientHandler);
            var response = await httpClient.GetAsync(uri);

            if (!response.IsSuccessStatusCode)
                return null;

            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<LoginRequestModel>(stringResponse);
            return result;
        }
    }
}
