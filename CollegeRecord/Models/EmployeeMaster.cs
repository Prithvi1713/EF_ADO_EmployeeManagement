using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeRecord.Models
{
    public class EmployeeMaster
    {
        [Key]
        public int EmpId { get; set; }
        [Required(ErrorMessage = "Please enter First Name")]
        [StringLength(32, MinimumLength = 2, ErrorMessage = "length must be in between 2 to 32")]
        [Display(Name = "First Name")]

        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter Middle Name")]
        [StringLength(32, MinimumLength = 2, ErrorMessage = "length must be in between 2 to 32")]
        [Display(Name = "Middle Name")]

        public string MiddleName { get; set; }
        [Required(ErrorMessage = "Please enter Last Name")]
        [StringLength(32, MinimumLength = 2, ErrorMessage = "length must be in between 2 to 32")]
        [Display(Name = "Last Name")]

        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter E-mail Id")]
        [EmailAddress(ErrorMessage = "Wrong Email Address Format")]
        [Display(Name = "E-mail ID")]

        public string EmailId { get; set; }
        [Required(ErrorMessage = "Please enter Mobile Number")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "number must be 10 digit only")]
        [Display(Name = "Mobile Number")]

        public string MobileNo { get; set; }
        [Required(ErrorMessage = "Please select Birth Date")]
        [Display(Name = "Birth Date")]

        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "Please select Gender")]

        [Display(Name = "Gender ")]


        public string Gender { get; set; }
        [Required(ErrorMessage = "Please enter Employee Name")]
        [DisplayFormat(DataFormatString = "{0:0}")]
        [Display(Name = "salary")]

        public decimal salary { get; set; }
        [Required(ErrorMessage = "Please select status")]
        [Display(Name = " Status")]


        public bool status { get; set; }
        [Display(Name = "Department")]

        [ForeignKey(nameof(departmentMaster))]
        public int DeptId { get; set; }
        [Display(Name = "Department")]

        public DepartmentMaster? departmentMaster { get; set; }

        [ForeignKey(nameof(designationMaster))]

        public int DesignId { get; set; }
        [Display(Name = "Designation")]

        
        public DesignationMaster? designationMaster { get; set; }
        


    }
}
