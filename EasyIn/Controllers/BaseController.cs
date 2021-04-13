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
    public class BaseController : ControllerBase
    {
        public BaseController(IUserRepository userRepository)
        {
            OAuthUser.InitAuthUser(userRepository);
        }
    }
}
