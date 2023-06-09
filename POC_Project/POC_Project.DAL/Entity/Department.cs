﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace POC_Project.DAL.Entity
{
    [Table("Department")]
    public class Department
    {
        public int Id { get; set; }

        [Required,StringLength(50)]
        public string Name { get; set; }

        [Required,StringLength(50)]
        public string Code { get; set; } 

    }
}
