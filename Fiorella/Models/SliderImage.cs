using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fiorella.Models
{
    public class SliderImage : BaseEntity
    {
        public string Image { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }
        public int SliderId { get; set; }
        public Slider slider { get; set; }
    }
}
