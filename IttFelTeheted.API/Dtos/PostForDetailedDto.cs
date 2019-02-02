using System;
using System.Collections.Generic;
using IttFelTeheted.API.Models;

namespace IttFelTeheted.API.Dtos
{
    public class PostForDetailedDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PostBody { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public Topic Topic { get; set; }
        public DateTime DateAdded { get; set; }
        public int Views { get; set; }
        public string PhotoUrl { get; set; }
        public ICollection<AnswerForDetailedDto> Answers { get; set; }
    }
}