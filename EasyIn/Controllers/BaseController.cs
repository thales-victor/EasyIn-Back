using EasyIn.Repositories;
using EasyIn.Repositories.Interfaces;
using EasyIn.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EasyIn.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        private IUserRepository _userRepository;
        public User AuthenticatedUser;

        public BaseController(IUserRepository userRepository = null)
        {
            _userRepository = userRepository;
            AuthenticatedUser = GetAuthenticationUser();
        }

        public bool IsOwner(User user)
        {
            if (user == null || AuthenticatedUser == null)
                return false;

            return user.Id == AuthenticatedUser.Id;
        }

        public User GetAuthenticationUser()
        {
            int? id = 0;
            var a = User.GetUserId();

            if (!id.HasValue || _userRepository == null)
                return null;

            return _userRepository.GetById(id.Value).Result;
        }
    }
}
