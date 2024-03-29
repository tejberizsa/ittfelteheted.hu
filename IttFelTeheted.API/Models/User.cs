using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace IttFelTeheted.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string EMail { get; set; }
        public string Gender { get; set; }
        [NotMapped]
        public int Age
        {
            get 
            {
                var age = DateTime.Today.Year - Birth.Year;
                if (Birth.Date > DateTime.Today.AddYears(-age))
                    age--;
                return age;
            }
        }
        public DateTime Birth { get; set; }
        public string Introduction { get; set; }
        public ICollection<UserPhoto> Photos { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastActive { get; set; }
        public DateTime? ConfirmationEmailSent { get; set; }
        public bool EMailConfirmed { get; set; }
        public string ConfirmKey { get; set; }
        public bool Deleted { get; set; }
        public ICollection<Message> MessagesSent { get; set; }
        public ICollection<Message> MessagesReceived { get; set; }
        public ICollection<Vote> Voted { get; set; }
        public ICollection<UserFollow> Follower { get; set; }
        public ICollection<UserFollow> Followed { get; set; }
        public ICollection<PostFollow> PostFollowed { get; set; }
    }
}