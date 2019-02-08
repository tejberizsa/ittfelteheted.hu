using System;

namespace IttFelTeheted.API.Models
{
    public class PostedPhoto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}