using System.Threading.Tasks;
using IttFelTeheted.API.Models;

namespace IttFelTeheted.API.Data
{
    public interface IAuthRepository
    {
         Task<User> Register(User user, string password);
         Task<User> Login(string username, string password);
         Task<bool> UserExists(string username);
    }
}