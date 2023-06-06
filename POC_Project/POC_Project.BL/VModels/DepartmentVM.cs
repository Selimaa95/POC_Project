using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC_Project.BL.VModels
{
    public class DepartmentVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Name Required")]
        [MaxLength(50,ErrorMessage ="Max Length 50")]
        [MinLength(3,ErrorMessage ="Min Length 3")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Code Required")]
        [Range(1,5000,ErrorMessage ="Range BTW 1 To 5000")]
        public string Code { get; set; }

    }
}
