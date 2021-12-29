using Fiorella.Areas.Admin.Controllers.Constants;
using Fiorella.Areas.Admin.Utils;
using Fiorella.Areas.Admin.ViewModels;
using Fiorella.DAL;
using Fiorella.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;

namespace Fiorella.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderImageController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public SliderImageController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            var sliderImgs = await _context.SliderImages.ToListAsync();
            return View(sliderImgs);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public async Task<IActionResult> CreateSliderImage(SliderImage sliderImage)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!sliderImage.File.IsContains())
            {
                ModelState.AddModelError(nameof(SliderImage), "File is not supported");
                return View();
            }

            if (sliderImage.File.IsRightSize(1000))
            {
                ModelState.AddModelError(nameof(File), "Size can not be greater than 1mb.");
            }

            sliderImage.Image = FileUtils.FileCreate(sliderImage.File);

            await _context.SliderImages.AddAsync(sliderImage);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Update(int id)
        {
            
            SliderImage slider = await _context.SliderImages.FindAsync(id);
            if (slider == null)
            {
                return BadRequest();
            }
            return View(slider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Update")]
        public async Task<IActionResult> UpdateSlider(int id, SliderImage sliderImage)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id != sliderImage.Id)
            {
                return NotFound();
            }

            if (!sliderImage.File.IsContains())
            {
                ModelState.AddModelError(nameof(SliderImage), "File is not supported.");
                return View();
            }
            if (sliderImage.File.IsRightSize(1000))
            {
                ModelState.AddModelError(nameof(SliderImage), "Size can not be greater than 1mb.");

            }

            var slider = await _context.SliderImages.FindAsync(id);
            string path = Path.Combine(FileConstants.ImagePath, slider.Image);

            slider.Image = FileUtils.FileCreate(sliderImage.File);

            _context.SliderImages.Update(slider);
            await _context.SaveChangesAsync();



            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int id)
        {
            SliderImage sliderImage = await _context.SliderImages.FindAsync(id);
            if (sliderImage == null)
            {
                return NotFound();
            }
            return View(sliderImage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteSliderImage(int id)
        {
            SliderImage sliderImage = await _context.SliderImages.FindAsync(id);
            if (sliderImage == null)
            {
                return NotFound();
            }

            string path = Path.Combine(FileConstants.ImagePath, sliderImage.Image);

            _context.SliderImages.Remove(sliderImage);
            await _context.SaveChangesAsync();
            FileUtils.FileDelete(path);
            return RedirectToAction(nameof(Index));
        }

        //public IActionResult AddSliderToSliderImages()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> AddSlider(MultipleViewModel viewModel)
        //{

        //    if (!ModelState.IsValid)
        //    {
        //        return View();
        //    }

        //    var slider = new Slider { Title = "Title", Description = "Description", Sign = "Sign" };
        //    await _context.Slider.AddAsync(slider);
        //    await _context.SaveChangesAsync();

        //    foreach (var item in viewModel.files)
        //    {
        //        if (!item.IsContains())
        //        {
        //            ModelState.AddModelError(nameof(SliderImage), "File is not supported.");
        //            return View();
        //        }

        //        if (item.IsRightSize(1000))
        //        {
        //            ModelState.AddModelError(nameof(SliderImage), "Size can not be greater than 1mb.");

        //        }

        //        viewModel.Slider.Sign = FileUtils.FileCreate(viewModel.Slider.signFile);

        //        //await _context.SliderImages.AddAsync();
                
        //    }
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction();

        //}

    }
}
