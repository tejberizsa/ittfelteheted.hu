using System;
using System.Collections.Generic;

namespace IttFelTeheted.API.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PostBody { get; set; }
        public User User { get; set; }
        public virtual Topic Topic { get; set; }
        public DateTime DateAdded { get; set; }
        public int Views { get; set; }
        public string PhotoUrl { get; set; }
        public ICollection<Answer> Answers { get; set; }

    }
}