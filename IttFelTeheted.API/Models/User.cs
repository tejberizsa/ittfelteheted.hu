using System;
using System.Collections.Generic;

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
        public int Age { get; set; }
        public string Introduction { get; set; }
        public ICollection<UserPhoto> Photos { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastActive { get; set; }
    }
}