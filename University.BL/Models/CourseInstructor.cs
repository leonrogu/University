using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; //Librería necesaria para agregar los decoradores []

namespace University.BL.Models
{
    [Table("CourseInstructor", Schema = "dbo")]

    public class CourseInstructor
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Course")]
        public int CourseID { get; set; }
        [ForeignKey("Instructor")]
        public int InstructorID { get; set; }

        //NAVS
        public Course Course { get; set; }
        public Instructor Instructor { get; set; }
    }
}
