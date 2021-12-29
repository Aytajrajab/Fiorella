using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fiorella.Models
{
    public class Slider : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Sign { get; set; }
        [NotMapped]
        public IFormFile signFile { get; set; }
        public List<SliderImage> sliderImages { get; set; }
    }
}
