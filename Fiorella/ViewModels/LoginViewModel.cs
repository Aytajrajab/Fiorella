using System.ComponentModel.DataAnnotations;

namespace Fiorella.ViewModels
{
    public class LoginViewModel
    {
        [Required, EmailAddress, DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required,DataType(DataType.Password)]
        public string Password { get; set; }

        public bool Persistent { get; set; } = true;
    }
}
