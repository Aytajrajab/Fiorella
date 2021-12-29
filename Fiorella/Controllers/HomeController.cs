using Fiorella.DAL;
using Fiorella.Models;
using Fiorella.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Fiorella.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Category> categories = _context.Categories.ToList();
            List<Product> products = _context.Products.Take(4).ToList();
            List<SliderImage> sliderImages = _context.SliderImages.ToList();
            List<Slider> sliders = _context.Slider.ToList();

            HomeIndexViewModel model = new HomeIndexViewModel()
            {
                categories = categories,
                products = products,
                sliderImages = sliderImages,
                slider = sliders,
            };
            return View(model);
        }
    }
}
