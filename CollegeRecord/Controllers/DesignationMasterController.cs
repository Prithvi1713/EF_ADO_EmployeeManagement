using CollegeRecord.DataContext;
using CollegeRecord.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollegeRecord.Controllers
{
    public class DesignationMasterController : Controller
    {
        private readonly AppDbContext _appDbContext;
        public DesignationMasterController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _appDbContext.designationMasters.ToListAsync();

            return View(data);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DesignationMaster designationMaster)
        {
            if (!ModelState.IsValid)
            {
                return View("Create");
            }
            await _appDbContext.designationMasters.AddAsync(designationMaster);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id)
        {
            var data = await _appDbContext.designationMasters.FindAsync(id);
            return View(data);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var data = await _appDbContext.designationMasters.FindAsync(id);

            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(DesignationMaster designationMaster)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit");
            }
            _appDbContext.designationMasters.Update(designationMaster);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _appDbContext.designationMasters.FindAsync(id);
            return View(data);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var data = await _appDbContext.designationMasters.FindAsync(id);


            _appDbContext.designationMasters.Remove(data);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
