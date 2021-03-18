using EasyIn.Repositories;
using EasyIn.Repositories.Interfaces;
using EasyIn.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EasyIn.Controllers
{
    public static class OAuthUser
    {
        private static IUserRepository UserRepository { get; set; }

        public static void InitAuthUser(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public static bool IsOwner(this ClaimsPrincipal authUser, User user)
        {
            if (user == null || authUser == null)
                return false;

            return user.Id == authUser.GetId();
        }

        public static async Task<User> ToEntityUser(this ClaimsPrincipal authUser)
        {
            if (UserRepository == null || authUser == null)
                return null;

            var id = authUser.GetId();

            if (id == 0)
                return null;

            return await UserRepository.GetById(id);
        }

        public static int GetId (this ClaimsPrincipal authUser)
        {
            var claim = authUser.FindFirst(ClaimTypes.NameIdentifier);

            if (claim == null)
                return 0;

            var stringId = claim.Value;

            return Convert.ToInt32(stringId);
        }
    }
}
