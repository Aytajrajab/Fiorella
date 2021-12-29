using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Fiorella.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string FullName { get; set; }
    }
}
