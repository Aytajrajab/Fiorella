using Fiorella.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Fiorella.Areas.Admin.ViewModels
{
    public class MultipleViewModel
    {
        public IFormFile[] files { get; set; }
        public int Id { get; set; }
        public Slider Slider { get; set; }
        public List<SliderImage> SliderImgs { get; set; }
    }
}
