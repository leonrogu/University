using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; //Librería necesaria para agregar los decoradores []

namespace University.BL.Models
{
    [Table("Department", Schema = "dbo")]

    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] //Se usa cuando la PK no es autoincrementable
        public int DepartmentID { get; set; }
        public string Name { get; set; }
        public double Budget { get; set; }
        public DateTime StartDate { get; set; }
        [ForeignKey("Instructor")]
        public int InstructorID { get; set; }
        public Instructor Instructor { get; set; }
    }
}
