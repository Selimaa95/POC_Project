using POC_Project.DAL.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC_Project.BL.VModels
{
    public class DistrictVM
    { 
        public int Id { get; set; }

        public string? Name { get; set; }

        public int CityId { get; set; }

        public City? City { get; set; }
    }
}
