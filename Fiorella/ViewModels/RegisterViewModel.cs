using System.ComponentModel.DataAnnotations;

namespace Fiorella.ViewModels
{
    public class RegisterViewModel
    {
        [Required, MaxLength(100)]
        public string FullName { get; set; }


        [Required, MaxLength(100), MinLength(6)]
        public string UserName { get; set; }


        [Required, EmailAddress, DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required, DataType(DataType.Password)]
        public string Password { get; set; }


        [Required, DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
