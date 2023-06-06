using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC_Project.DAL.Entity
{
    public class District
    { 
        public int Id { get; set; }

        public string? Name { get; set; }

        public int CityId { get; set; }

        [ForeignKey("CityId")]
        public City? City { get; set; }
    }
}
