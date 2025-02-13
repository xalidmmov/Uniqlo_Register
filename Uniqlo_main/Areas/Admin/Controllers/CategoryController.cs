﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Uniqlo_main.DataAccess;
using Uniqlo_main.Models;
using Uniqlo_main.ViewModels.Category;

namespace Uniqlo_main.Areas.Admin.Controllers
{
    
    public class CategoryController(UniqloDbContext _context) : Controller
    {
        [Area("Admin")]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateVm vm)
        {
            if (!ModelState.IsValid) return View();


            Category category = new Category
            {
                Name = vm.Name
            };

            await _context.AddAsync(category);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Update(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var data = await _context.Categories.FindAsync(id);

            if (data is null) return NotFound();

            CategoryUpdateVM vm = new();

            vm.Name = data.Name;

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, CategoryUpdateVM vm)
        {

            if (!id.HasValue) return BadRequest();
            if (!ModelState.IsValid) return View();

            var data = await _context.Categories.FindAsync(id);

            if (data is null) return View();

            data.Name = vm.Name;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            var data = await _context.Categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == id);
            
            if (data is null) return View();

            _context.Categories.Remove(data);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Hide(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var data = await _context.Categories.FindAsync(id);

            if (data is null) return View();

            data.IsDeleted = true;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Show(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var data = await _context.Categories.FindAsync(id);

            if (data is null) return View();

            data.IsDeleted = false;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        


    }
}
