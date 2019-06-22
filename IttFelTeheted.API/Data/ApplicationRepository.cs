using System;
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

        public async Task<Post> GetPostByID(int id, bool viewIt = false)
        {
            var post = await _context.Posts
                                    .Include(x => x.Topic)
                                    .Include(x => x.User).ThenInclude(u => u.Photos)
                                    .Include(x => x.Answers).ThenInclude(a => a.User).ThenInclude(u => u.Photos)
                                    .Include(x => x.Photos)
                                    .Include(x => x.PostFollower)
                                    .FirstOrDefaultAsync(x => x.Id == id);
            if (viewIt)
            {
                post.Views++;
                await SaveAll();
            }

            return post;
        }

        public async Task<Post> GetPostByRandom()
        {
            Random rand = new Random();
            var postCount = await _context.Posts.CountAsync();
            var skip = rand.Next(1, postCount);
            // var skip = (int)(rand.NextDouble() * _context.Posts.Count());
            var post = await _context.Posts
                                    .Include(x => x.Topic)
                                    .Include(x => x.User).ThenInclude(u => u.Photos)
                                    .Include(x => x.Answers).ThenInclude(a => a.User).ThenInclude(u => u.Photos)
                                    .Include(x => x.Photos)
                                    .Include(x => x.PostFollower)
                                    .Skip(skip).Take(1)
                                    .FirstOrDefaultAsync();
            post.Views++;
            await SaveAll();
            

            return post;
        }

        public async Task<Answer> GetAnswerByID(int id)
        {
            var answer = await _context.Answers.FirstOrDefaultAsync(a => a.Id == id);

            return answer;
        }

        public async Task<PagedList<Post>> GetPosts(PageParams postParams)
        {
            var posts = _context.Posts
                                .Include(x => x.Topic)
                                .Include(x => x.User)
                                .Include(x => x.Answers)
                                .Include(x => x.Photos)
                                .Include(x => x.PostFollower)
                                .OrderByDescending(x => x.Views).AsQueryable();
            
            if (postParams.isFollowQuery != null)
            {
                posts = _context.Posts
                        .FromSql("SELECT Posts.* FROM Posts JOIN PostFollows ON Posts.Id=PostFollows.FollowedId WHERE PostFollows.FollowerId={0}", postParams.UserId)
                                .Include(x => x.Topic)
                                .Include(x => x.User)
                                .Include(x => x.Answers)
                                .Include(x => x.Photos)
                                .Include(x => x.PostFollower)
                                .OrderByDescending(x => x.Views).AsQueryable();
                postParams.PageSize = 8;
            }
            
            if (postParams.TopicId != null)
                posts = posts.Where(p => p.Topic.Id == postParams.TopicId.Value);

            if (postParams.QueryString != null)
                posts = posts.Where(p => p.Title.Contains(postParams.QueryString));

            if (postParams.UserId != null && postParams.isFollowQuery == null)
            {
                posts = posts.Where(p => p.User.Id == postParams.UserId.Value);
                postParams.PageSize = 8;
            }
            
            return await PagedList<Post>.CreateAsync(posts, postParams.PageNumber, postParams.PageSize);
        }

        public async Task<Topic> GetTopic(int id)
        {
            var topic = await _context.Topics.FirstOrDefaultAsync(t => t.Id == id);
            return topic;
        }

        public async Task<IEnumerable<Topic>> GetTopics()
        {
            var topics = await _context.Topics.OrderBy(t => t.TopicName).ToListAsync();
            return topics;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users
                                        .Include(u => u.Photos)
                                        .Include(u => u.Follower)
                                        .FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<PagedList<User>> GetUsers(PageParams userParams)
        {
            var users = _context.Users.AsQueryable();
            if (userParams.UserId != null)
                users = _context.Users
                        .FromSql("SELECT Users.* FROM Users JOIN UserFollows ON Users.Id=UserFollows.FollowedId WHERE UserFollows.FollowerId={0}", userParams.UserId)
                        .Include(u => u.Photos)
                        .OrderByDescending(x => x.Username).AsQueryable();
            userParams.PageSize = 18;
            return await PagedList<User>.CreateAsync(users, userParams.PageNumber, userParams.PageSize);
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
                    messages = messages.Where(u => u.RecipientId == messageParams.UserId && u.RecipientDeleted == false);
                    break;
                case "Outbox":
                    messages = messages.Where(u => u.SenderId == messageParams.UserId && u.SenderDeleted == false);
                    break;
                default:
                    messages = messages.Where(u => u.RecipientId == messageParams.UserId && u.RecipientDeleted == false && u.IsRead == false);
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
                .Where(m => m.RecipientId == userId && m.RecipientDeleted == false && m.SenderId == recipientId 
                        || m.RecipientId == recipientId && m.SenderId == userId && m.SenderDeleted == false)
                .OrderByDescending(d => d.MessageSent)
                .ToListAsync();

            return messages;
        }

        public async Task<PostedPhoto> GetPostedPhoto(int id)
        {
            var postedPhoto = await _context.PostedPhotos.FirstOrDefaultAsync(p => p.Id == id);

            return postedPhoto;
        }

        public async Task<UserPhoto> GetUserPhoto(int id)
        {
            var userPhoto = await _context.UserPhotos.FirstOrDefaultAsync(p => p.Id == id);

            return userPhoto;
        }

        public async Task<UserPhoto> GetMainPhotoForUser(int userId)
        {
            return await _context.UserPhotos.Where(u => u.UserID == userId).FirstOrDefaultAsync(p => p.IsMain);
        }

        public async Task<IEnumerable<Post>> GetTrendingPosts(int topicId, int userId)
        {
            var topPostsByUser = await _context.Posts.Where(p => p.User.Id == userId)
                                                    .OrderByDescending(p => p.Views)
                                                    .Take(4)
                                                    .Include(p => p.Photos)
                                                    .Include(p => p.User)
                                                    .ToListAsync();

            var trendingPosts = await _context.Posts.Where(p => p.Topic.Id == topicId)
                                                    .OrderByDescending(p => p.Views)
                                                    .Take(6)
                                                    .Include(p => p.Photos)
                                                    .Include(p => p.User)
                                                    .ToListAsync();

            var topPosts = await _context.Posts.OrderByDescending(p => p.Views)
                                                    .Take(2)
                                                    .Include(p => p.Photos)
                                                    .Include(p => p.User)
                                                    .ToListAsync();

            trendingPosts.AddRange(topPostsByUser);
            trendingPosts.AddRange(topPosts);

            return trendingPosts;
        }

        public async Task<Vote> GetVote(int userId, int answerId)
        {
            return await _context.Votes.FirstOrDefaultAsync(v => 
                                        v.VoterId == userId && v.VotedId == answerId);
        }

        public async Task<UserFollow> GetUserFollow(int userId, int followedId)
        {
            return await _context.UserFollows.FirstOrDefaultAsync(uf => 
                                        uf.FollowerId == userId && uf.FollowedId == followedId);
        }

        public async Task<PostFollow> GetPostFollow(int userId, int followedId)
        {
            return await _context.PostFollows.FirstOrDefaultAsync(pf => 
                                        pf.FollowerId == userId && pf.FollowedId == followedId);
        }
    }
}