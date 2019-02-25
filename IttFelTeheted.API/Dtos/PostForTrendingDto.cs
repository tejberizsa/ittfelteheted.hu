using System;

namespace IttFelTeheted.API.Dtos
{
    public class PostForTrendingDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Username { get; set; }
        public DateTime DateAdded { get; set; }
        public int Views { get; set; }
        public string PhotoUrl { get; set; }
        public int AnswerCount { get; set; }
    }
}