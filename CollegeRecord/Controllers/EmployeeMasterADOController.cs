using CollegeRecord.Models;
using EF_EmployeeRecordMgt.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EF_EmployeeRecordMgt.Controllers
{
    public class EmployeeMasterADOController : Controller
    {
        private readonly IEmployeeMaster _employeeMaster;
        private readonly IDepartmentMaster _departmentMaster;

        private readonly IDesignationMaster _designationMaster;


        public EmployeeMasterADOController(IEmployeeMaster employeeMaster, IDepartmentMaster departmentMaster, IDesignationMaster designationMaster)
        {
            _employeeMaster = employeeMaster;
            _designationMaster = designationMaster;
            _departmentMaster = departmentMaster;
        }


        public IActionResult Index()
        {
            var data = _employeeMaster.GetAllEmployeeMaster();
            return View(data);
        }

        public IActionResult Create()
        {
            var dept = _departmentMaster.GetAllDepartment();
            ViewBag.DeptList = new SelectList(dept, "DeptId", "DeptName");

            var design = _designationMaster.GetAllDesignations();
            ViewBag.DesignList = new SelectList(design, "DesignId", "DesignName");

            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeMaster employeeMaster)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Create));
            }
            _employeeMaster.AddEmployee(employeeMaster);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int id)
        {
            var data = _employeeMaster.GetEmployeeById(id);
            return View(data);
        }
        public IActionResult Edit(int id)
        {
            var dept = _departmentMaster.GetAllDepartment();
            ViewBag.DeptList = new SelectList(dept, "DeptId", "DeptName");

            var design = _designationMaster.GetAllDesignations();
            ViewBag.DesignList = new SelectList(design, "DesignId", "DesignName");


            var data = _employeeMaster.GetEmployeeById(id);
            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(EmployeeMaster employeeMaster)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Edit));
            }
            _employeeMaster.EditEmployee(employeeMaster);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            var data = _employeeMaster.GetEmployeeById(id);
            return View(data);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _employeeMaster.DeleteEmployee(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
