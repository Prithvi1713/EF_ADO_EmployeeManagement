using CollegeRecord.Models;
using EF_EmployeeRecordMgt.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace EF_EmployeeRecordMgt.Controllers
{
    public class DepartmentMasterADOController : Controller
    {
        private readonly IDepartmentMaster _departmentMaster;
        public DepartmentMasterADOController(IDepartmentMaster departmentMaster)
        {
            _departmentMaster = departmentMaster;
        }
        public IActionResult Index()
        {
            var data = _departmentMaster.GetAllDepartment();
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DepartmentMaster departmentMaster)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Create));
            }
            _departmentMaster.AddDepartment(departmentMaster);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int id)
        {
            var data = _departmentMaster.GetDepartmentById(id);
            return View(data);
        }
        public IActionResult Edit(int id)
        {
            var data = _departmentMaster.GetDepartmentById(id);
            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(DepartmentMaster departmentMaster)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Edit));
            }
            _departmentMaster.EditDepartment(departmentMaster);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            var data = _departmentMaster.GetDepartmentById(id);
            return View(data);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _departmentMaster.DeleteDepartment(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
