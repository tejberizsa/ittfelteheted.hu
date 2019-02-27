using System;
using System.Collections.Generic;

namespace IttFelTeheted.API.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string AnswerBody { get; set; }
        public DateTime DateAdded { get; set; }
        public int Like { get; set; }
        public virtual User User { get; set; }
        public string PhotoUrl { get; set; }
        public Post Post { get; set; }
        public int PostId { get; set; }
        public ICollection<Vote> Voter { get; set; }
    }
}