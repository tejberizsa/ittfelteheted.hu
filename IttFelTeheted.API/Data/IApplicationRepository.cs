using System.Collections.Generic;
using System.Threading.Tasks;
using IttFelTeheted.API.Models;

namespace IttFelTeheted.API.Data
{
    public interface IApplicationRepository
    {
         void Add<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         Task<bool> SaveAll();
         Task<IEnumerable<User>> GetUsers();
         Task<User> GetUser(int id);
         Task<IEnumerable<Post>> GetPosts();
         Task<Post> GetPostByID(int Id);
         Task<Topic> GetTopic(int Id);
    }
}