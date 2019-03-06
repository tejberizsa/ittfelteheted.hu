using System;
using System.ComponentModel.DataAnnotations;

namespace IttFelTeheted.API.Dtos
{
    public class PostForAddDto
    {
        [Required(ErrorMessage = "Poszt vagy kérdés kötelező")]
        [StringLength(250, ErrorMessage = "Poszt vagy kérdés nem lehet ilyen hosszú")]
        public string Title { get; set; }
        [StringLength(2000, ErrorMessage = "Kiegészítő rész nem lehet ilyen hosszú")]
        public string PostBody { get; set; }
        public int UserId { get; set; }
        [Required(ErrorMessage = "Kategória kötelező")]
        public int TopicId { get; set; }
        public DateTime DateAdded { get; set; }
        public int Views { get; set; }
        public PostForAddDto()
        {
            Views = 0;
            DateAdded = DateTime.Now;
        }
    }
}