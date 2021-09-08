using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace University.BL.DTOs
{
    public class DepartmentDTO
    {
        public int DepartmentID { get; set; }
        [StringLength(50)]
        public string Name { get; set; }

        public decimal Budget { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }
        public int InstructorID { get; set; }
    }
}
