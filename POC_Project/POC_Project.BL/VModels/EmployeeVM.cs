using Microsoft.AspNetCore.Http;
using POC_Project.DAL.Entity;
using System.ComponentModel.DataAnnotations;

namespace POC_Project.BL.VModels
{
    public class EmployeeVM
    {

        public int Id { get; set; }

        [Required(ErrorMessage ="Name Required")]
        public string Name { get; set; }

        [RegularExpression("[1-9]{1,5}-[a-zA-Z]{1,10}-[a-zA-Z]{1,10}-[a-zA-Z]{1,10}"
            ,ErrorMessage = "Enter Like => 12-StreetName-City-Country ")]
        public string Address { get; set; }

        [Range(2000,100000,ErrorMessage ="Salary BTW 2k To 100K")]
        public double Salary { get; set; }
        [EmailAddress(ErrorMessage ="Email Invaild")]
        public string Email { get; set; }
        public string Notes { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsUpdated { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime DeleteData { get; set; }
        public DateTime UpdateData { get; set; }
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
        public int DistrictId { get; set; }
        public District? District { get; set; }

        public string? ImageName { get; set; }
        public string? CvName { get; set; }
         
        public IFormFile? Image { get; set; } 
        public IFormFile? CV { get; set; }
    }
}

