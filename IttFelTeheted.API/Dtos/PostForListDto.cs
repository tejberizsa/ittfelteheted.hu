using System;
using IttFelTeheted.API.Models;

namespace IttFelTeheted.API.Dtos
{
    public class PostForListDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PostBody { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public Topic Topic { get; set; }
        public DateTime DateAdded { get; set; }
        public int Views { get; set; }
        public string PhotoUrl { get; set; }
        public AnswerForDetailedDto Answer { get; set; }
    }
}