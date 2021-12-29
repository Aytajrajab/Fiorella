using Fiorella.Areas.Admin.Utils;
using Fiorella.Areas.Admin.ViewModels;
using Fiorella.DAL;
using Fiorella.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiorella.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        public SliderController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var slider = await _context.Slider.FirstAsync();
            var sldrImg = await _context.SliderImages.Where(s=>s.SliderId == slider.Id).ToListAsync();

            MultipleViewModel model = new MultipleViewModel()
            {
                Slider = slider,
                SliderImgs = sldrImg,
            };
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MultipleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            if (!model.Slider.signFile.IsContains())
            {
                ModelState.AddModelError(nameof(model.Slider.signFile), "File is not supported");
                return View();
            }

            if (model.Slider.signFile.IsRightSize(1000))
            {
                ModelState.AddModelError(nameof(model.Slider.signFile), "Size can not be greater than 1mb.");
            }

            model.Slider.Sign = FileUtils.FileCreate(model.Slider.signFile);
            model.SliderImgs = new List<SliderImage>();
            foreach (var item in model.files)
            {
                if (!item.IsContains())
                {
                    ModelState.AddModelError(nameof(model.Slider.signFile), "File is not supported");
                    return View();
                }

                if (item.IsRightSize(1000))
                {
                    ModelState.AddModelError(nameof(model.Slider.signFile), "Size can not be greater than 1mb.");
                }

                model.SliderImgs.Add(new SliderImage { Image=FileUtils.FileCreate(item), Id=model.Slider.Id });
            }
            await _context.SliderImages.AddRangeAsync(model.SliderImgs);
            await _context.Slider.AddAsync(model.Slider);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
