using System;
using System.ComponentModel.DataAnnotations;

namespace IttFelTeheted.API.Dtos
{
    public class AnswerForAddDto
    {
        [Required(ErrorMessage = "Válasz kötelező")]
        [StringLength(1000, ErrorMessage = "Válasz nem lehet ilyen hosszú")]
        public string AnswerBody { get; set; }
        public DateTime DateAdded { get; set; }
        public int Like { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int TopicId { get; set; }
        public string PhotoUrl { get; set; }
        [Required]
        public int PostId { get; set; }
        public AnswerForAddDto()
        {
            Like = 0;
            DateAdded = DateTime.Now;
        }
    }
}