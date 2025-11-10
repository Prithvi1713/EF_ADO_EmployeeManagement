using CollegeRecord.DataContext;
using CollegeRecord.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollegeRecord.Controllers
{
    public class DepartmentMasterController : Controller
    {
        private readonly AppDbContext _appDbContext;
        public DepartmentMasterController(AppDbContext appDbContext) 
        {
            _appDbContext = appDbContext;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _appDbContext.departmentMasters.ToListAsync();

            return View(data);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>Create(DepartmentMaster departmentMaster)
        {
            if(!ModelState.IsValid)
            {
                return View("Create");
            }
            await _appDbContext.departmentMasters.AddAsync(departmentMaster);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id)
        {
            var data = await _appDbContext.departmentMasters.FindAsync(id);
            return View(data);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var data = await _appDbContext.departmentMasters.FindAsync(id);

            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(DepartmentMaster departmentMaster)
        {
            if(!ModelState.IsValid)
            {
                return View("Edit");
            }
            _appDbContext.departmentMasters.Update(departmentMaster);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _appDbContext.departmentMasters.FindAsync(id);
            return View(data);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var data = await _appDbContext.departmentMasters.FindAsync(id);
            
            
            _appDbContext.departmentMasters.Remove(data);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
