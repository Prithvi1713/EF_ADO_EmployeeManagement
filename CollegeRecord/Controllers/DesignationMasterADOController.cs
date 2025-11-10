using CollegeRecord.Models;
using EF_EmployeeRecordMgt.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace EF_EmployeeRecordMgt.Controllers
{
    public class DesignationMasterADOCOntroller : Controller
    {
        private readonly IDesignationMaster _designationMaster;

        public DesignationMasterADOCOntroller(IDesignationMaster designationMaster)
        {
            _designationMaster = designationMaster;
        }

       
        public IActionResult Index()
        {
            var data = _designationMaster.GetAllDesignations();
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DesignationMaster designationMaster)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Create));
            }
            _designationMaster.AddDesignation(designationMaster);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int id)
        {
            var data = _designationMaster.GetDesignationById(id);
            return View(data);
        }
        public IActionResult Edit(int id)
        {
            var data = _designationMaster.GetDesignationById(id);
            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(DesignationMaster designationMaster)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Edit));
            }
            _designationMaster.EditDesignation(designationMaster);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            var data = _designationMaster.GetDesignationById(id);
            return View(data);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _designationMaster.DeleteDesignation(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
