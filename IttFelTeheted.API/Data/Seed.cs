using System.Collections.Generic;
using IttFelTeheted.API.Models;
using Newtonsoft.Json;
using System.Linq;
using Newtonsoft.Json.Serialization;
using System;

namespace IttFelTeheted.API.Data
{
    public class Seed
    {
        private readonly DataContext _context;
        public Seed(DataContext context)
        {
            _context = context;
        }

        public void SeedUser()
        {
            if(_context.Users.Count() < 1)
            {
                var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
                var users = JsonConvert.DeserializeObject<List<User>>(userData);
                foreach (var user in users)
                {
                    byte[] passwordHash, passwordSalt;
                    CreatePasswordHash("Password", out passwordHash, out passwordSalt);

                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                    user.Username = user.Username.ToLower();

                    _context.Users.Add(user);
                }
                _context.SaveChanges();
            }
            if (_context.Topics.Count() < 1)
            {
                var topicData = System.IO.File.ReadAllText("Data/TopicSeedData.json");
                var topics = JsonConvert.DeserializeObject<List<Topic>>(topicData);
                foreach (var topic in topics)
                {
                    _context.Topics.Add(topic);
                }
                _context.SaveChanges();
            }

            // if(_context.Posts.Count() < 1)
            // {
            //     var postData = System.IO.File.ReadAllText("Data/PostSeedData.json");
            //     var settings = new JsonSerializerSettings
            //             {
            //                 NullValueHandling = NullValueHandling.Ignore,
            //                 MissingMemberHandling = MissingMemberHandling.Ignore
            //                 // Error = HandleDeserializationError
            //             };
            //     var posts = JsonConvert.DeserializeObject<List<Post>>(postData, settings);
            //     Random random = new Random();
            //     int randomUser = random.Next(1, 20);
            //     int randomTopic = random.Next(1, 30);
            //     foreach (var post in posts)
            //     {
            //         post.User = _context.Users.FirstOrDefault(x => x.Id == randomUser);
            //         post.Topic = _context.Topics.FirstOrDefault(x => x.Id == randomTopic);
            //         foreach (var answer in post.Answers)
            //         {
            //             answer.User = _context.Users.FirstOrDefault(x => x.Id == randomUser);
            //         }
            //         _context.Posts.Add(post);
            //     }
            //     _context.SaveChanges();
            // }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public static void HandleDeserializationError(object sender, ErrorEventArgs errorArgs)
        {
            var currentError = errorArgs.ErrorContext.Error.Message;
            errorArgs.ErrorContext.Handled = true;
        }
    }
}