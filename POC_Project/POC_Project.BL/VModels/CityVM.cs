using POC_Project.DAL.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC_Project.BL.VModels
{
    public class CityVM
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int CountryId{ get; set; }

        public Country? Country { get; set; } 
    }
}
