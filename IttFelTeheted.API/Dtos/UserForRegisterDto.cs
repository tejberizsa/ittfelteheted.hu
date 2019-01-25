using System.ComponentModel.DataAnnotations;

namespace IttFelTeheted.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required(ErrorMessage = "Felhasználó üres!")]
        public string Username { get; set; }
        
        [Required(ErrorMessage = "Jelszó üres!")]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "Jelszó hossza 4 és 8 karakter között kell legyen.")]
        public string Password { get; set; }
    }
}