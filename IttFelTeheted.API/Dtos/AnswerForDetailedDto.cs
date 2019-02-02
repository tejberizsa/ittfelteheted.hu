using System;

namespace IttFelTeheted.API.Dtos
{
    public class AnswerForDetailedDto
    {
        public int Id { get; set; }
        public string AnswerBody { get; set; }
        public DateTime DateAdded { get; set; }
        public int Like { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PhotoUrl { get; set; }
    }
}