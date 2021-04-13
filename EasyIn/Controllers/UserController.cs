using EasyIn.Models;
using EasyIn.Repositories.Interfaces;
using EasyIn.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EasyIn.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/user")]
    public class UserController : BaseController
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        public UserController(IUserRepository userRepository, IEmailService emailService): base(userRepository)
        {
            _userRepository = userRepository;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var user = await _userRepository.GetById(User.GetId());

            if (user == null)
                return NoContent();

            var result = new UserModel(user);

            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Post(UserCreateModel model)
        {
            if (await _userRepository.AlreadyExists(model.Email))
                return BadRequest(new ResponseError("Usuário já cadastrado"));

            if (model.Password != model.ConfirmPassword)
                return BadRequest(new ResponseError("Senha e confirmação de senha não coincidem"));

            var user = await _userRepository.Add(new User(model.Email, model.Username, model.Password));

            var result = new UserModel(user);

            return Ok(result);
        }

        [HttpPost("ForgotPassword")]
        [AllowAnonymous]
        public async Task<ActionResult> ForgotPassword(UserUpdateModel model)
        {
            if (!await _userRepository.AlreadyExists(model.Email))
                return BadRequest(new ResponseError("Usuário não encontrado"));
            try
            {
                var newPassword = RandomPasswordGenerator.Generate(10);

                if (!Debugger.IsAttached)
                {
                    _emailService.Send(
                    to: model.Email,
                    subject: "Recuperação de senha",
                    html: GetForgotPassworEmail(newPassword)
                    );
                }

                var user = await _userRepository.GetByEmail(model.Email);
                user.SetTemporaryPassword(newPassword);

                await _userRepository.Update(user);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseError(ex.Message));
            }
        }


        [HttpPut]
        public async Task<ActionResult> Put(UserUpdateModel model)
        {
            var user = await _userRepository.GetById(User.GetId());

            if (user == null)
                return NoContent();

            if (user.IsChangingPassword(model.NewPassword))
            {
                if (user.Password != model.OldPassword)
                    return BadRequest(new ResponseError("Senha antiga incorreta"));

                if (model.NewPassword != model.ConfirmNewPassword)
                    return BadRequest(new ResponseError("Senha e confirmação de senha não coincidem"));
            }

            user.Update(model.Email, model.Username, model.NewPassword);

            await _userRepository.Update(user);

            var result = new UserModel(user);

            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete()
        {
            var user = await _userRepository.GetById(User.GetId());

            if (user == null)
                return NoContent();

            user.Remove();

            await _userRepository.Update(user);

            return Ok();
        }


        private string GetForgotPassworEmail(string substitution)
        {
            try
            {
                string path = GetPathFileName();
                string html = System.IO.File.ReadAllText(path);

                string pattern = @"#NewPassword";

                var regex = new Regex(pattern, RegexOptions.Multiline);

                html = regex.Replace(html, substitution);

                return html;
            }
            catch
            {
                return string.Empty;
            }
        }

        private static string GetPathFileName()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            for (int i = 0; i < 4; i++)
            {
                path = Directory.GetParent(path).FullName;
            }
            path += "\\Services\\Models\\ForgotPasswordEmail.html";

            return path;
        }
    }
}
