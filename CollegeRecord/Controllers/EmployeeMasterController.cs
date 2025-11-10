using CollegeRecord.DataContext;
using CollegeRecord.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CollegeRecord.Controllers
{
    public class EmployeeMasterController : Controller
    {
        private readonly AppDbContext _appDbContext;
        public EmployeeMasterController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IActionResult> Index()
        {

            var data = await _appDbContext.employeeMasters
                .Include(e => e.departmentMaster)
                .Include(e => e.designationMaster)
                .ToListAsync();

            return View(data);
        }

        public async Task<IActionResult> Create()
        {
            var dept = await _appDbContext.departmentMasters.ToListAsync();
            ViewBag.DeptList = new SelectList(dept, "DeptId", "DeptName");

            var design = await _appDbContext.designationMasters.ToListAsync();
            ViewBag.DesignList = new SelectList(design, "DesignId", "DesignName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeMaster employeeMaster)
        {
            if (!ModelState.IsValid)
            {
                return View("Create");
            }
            await _appDbContext.employeeMasters.AddAsync(employeeMaster);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id)
        {
            var data = await _appDbContext.employeeMasters
                .Include(e => e.departmentMaster)
                .Include(e => e.designationMaster)
                .FirstOrDefaultAsync(m => m.EmpId == id);
            return View(data);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var data = await _appDbContext.employeeMasters.FindAsync(id);
          
            var dept = await _appDbContext.departmentMasters.ToListAsync();
            ViewBag.DeptList = new SelectList(dept, "DeptId", "DeptName");

            var design = await _appDbContext.designationMasters.ToListAsync();
            ViewBag.DesignList = new SelectList(design, "DesignId", "DesignName");
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeMaster employeeMaster)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit");
            }
            _appDbContext.employeeMasters.Update(employeeMaster);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _appDbContext.employeeMasters
               .Include(e => e.departmentMaster)
               .Include(e => e.designationMaster)
               .FirstOrDefaultAsync(m => m.EmpId == id);
            return View(data);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var data = await _appDbContext.employeeMasters.FindAsync(id);


            _appDbContext.employeeMasters.Remove(data);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
