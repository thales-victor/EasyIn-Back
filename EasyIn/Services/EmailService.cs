using EasyIn.Domain;
using EasyIn.Repositories.Interfaces;
using EasyIn.Services.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using MimeKit.Text;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EasyIn.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailOptions _appSettings;

        public EmailService(IOptions<EmailOptions> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public void Send(string to, string subject, string html)
        {
            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_appSettings.HostUsername));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) 
            {
                Text = html 
            };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect(_appSettings.HostAddress, _appSettings.HostPort);
            smtp.AuthenticationMechanisms.Remove("XOAUTH2");
            smtp.Authenticate(_appSettings.HostUsername, _appSettings.HostPassword);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
