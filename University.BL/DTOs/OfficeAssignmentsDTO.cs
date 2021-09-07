using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace University.BL.DTOs
{
    public class OfficeAssignmentsDTO
    {
        [Required]
        public int InstructorID { get; set; }
        [Required]
        [StringLength(50)]
        public string Location { get; set; }

        //NAVS

        public InstructorDTO Instructor { get; set; }

    }
}
