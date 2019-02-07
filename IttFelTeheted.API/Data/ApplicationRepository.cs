using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IttFelTeheted.API.Models;
using Microsoft.EntityFrameworkCore;

namespace IttFelTeheted.API.Data
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly DataContext _context;
        public ApplicationRepository(DataContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Post> GetPostByID(int Id)
        {
            var post = await _context.Posts
                .Include(x => x.Topic)
                .FirstOrDefaultAsync(x => x.Id == Id);
            return post;
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            var posts = await _context.Posts
                                .Include(x => x.Topic)
                                .Include(x => x.User)
                                .Include(x => x.Answers)
                                .OrderByDescending(x => x.Views).ToListAsync();
            return posts;
        }

        public async Task<Topic> GetTopic(int Id)
        {
            var topic = await _context.Topics.FirstOrDefaultAsync(t => t.Id == Id);
            return topic;
        }

        public async Task<IEnumerable<Topic>> GetTopics()
        {
            var topics = await _context.Topics.OrderBy(t => t.TopicName).ToListAsync();
            return topics;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}