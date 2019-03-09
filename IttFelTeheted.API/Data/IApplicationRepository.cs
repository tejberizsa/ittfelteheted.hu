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
         Task<PagedList<Post>> GetPosts(PageParams postParams);
         Task<Post> GetPostByID(int id, bool viewIt = false);
         Task<Post> GetPostByRandom();
         Task<Answer> GetAnswerByID(int id);
         Task<IEnumerable<Topic>> GetTopics();
         Task<Topic> GetTopic(int id);
         Task<Message> GetMessage(int id);
         Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams);
         Task<IEnumerable<Message>> GetMessageThread(int userId, int recipientId);
         Task<PostedPhoto> GetPostedPhoto(int id);
         Task<UserPhoto> GetUserPhoto(int id);
         Task<UserPhoto> GetMainPhotoForUser(int userId);
         Task<IEnumerable<Post>> GetTrendingPosts(int topicId, int userId);
         Task<Vote> GetVote(int userId, int answerId);
         Task<UserFollow> GetUserFollow(int userId, int followedId);
         Task<PostFollow> GetPostFollow(int userId, int followedId);
    }
}