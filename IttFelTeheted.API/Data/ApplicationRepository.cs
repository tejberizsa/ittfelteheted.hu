using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IttFelTeheted.API.Helpers;
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
            var user = await _context.Users.Include(u => u.Photos).FirstOrDefaultAsync(u => u.Id == id);
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

        public async Task<Message> GetMessage(int id)
        {
            return await _context.Messages.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams)
        {
            var messages = _context.Messages
                .Include(u => u.Sender).ThenInclude(p => p.Photos)
                .Include(u => u.Recipient).ThenInclude(p => p.Photos)
                .AsQueryable();

            switch (messageParams.MessageContainer)
            {
                case "Inbox":
                    messages = messages.Where(u => u.RecipientId == messageParams.UserId);
                    break;
                case "Outbox":
                    messages = messages.Where(u => u.SenderId == messageParams.UserId);
                    break;
                default:
                    messages = messages.Where(u => u.RecipientId == messageParams.UserId && u.IsRead == false);
                    break;
            }
            
            messages = messages.OrderByDescending(d => d.MessageSent);
            return await PagedList<Message>.CreateAsync(messages, messageParams.PageNumber, messageParams.PageSize);
        }

        public async Task<IEnumerable<Message>> GetMessageThread(int userId, int recipientId)
        {
            var messages = await _context.Messages
                .Include(u => u.Sender).ThenInclude(p => p.Photos)
                .Include(u => u.Recipient).ThenInclude(p => p.Photos)
                .Where(m => m.RecipientId == userId && m.SenderId == recipientId || m.RecipientId == recipientId && m.SenderId == userId)
                .OrderByDescending(d => d.MessageSent)
                .ToListAsync();

            return messages;
        }
    }
}