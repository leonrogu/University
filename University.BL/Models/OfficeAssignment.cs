using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; //Librería necesaria para agregar los decoradores []

namespace University.BL.Models
{
    [Table("OfficeAssignment", Schema = "dbo")]

    public class OfficeAssignment
    {
        [Key]
        [ForeignKey("Instructor")]
        public int InstructorID { get; set; }
        public string Location { get; set; }

        public Instructor Instructor { get; set; }
    }
}
