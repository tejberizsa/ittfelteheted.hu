using System.Collections.Generic;
using System.Threading.Tasks;
using IttFelTeheted.API.Helpers;
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
         Task<IEnumerable<Topic>> GetTopics();
         Task<Topic> GetTopic(int Id);
         Task<Message> GetMessage(int id);
         Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams);
         Task<IEnumerable<Message>> GetMessageThread(int userId, int recipientId);
         Task<PostedPhoto> GetPostedPhoto(int id);
         Task<UserPhoto> GetUserPhoto(int id);
         Task<UserPhoto> GetMainPhotoForUser(int userId);
    }
}