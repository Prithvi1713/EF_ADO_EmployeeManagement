

using CollegeRecord.Models;

namespace EF_EmployeeRecordMgt.IRepository
{
    public interface IDesignationMaster
    {
        IEnumerable<DesignationMaster> GetAllDesignations();
        void AddDesignation(DesignationMaster designationMaster);
        void EditDesignation(DesignationMaster designationMaster);

        DesignationMaster GetDesignationById(int id);

        void DeleteDesignation(int id);
    }
}
