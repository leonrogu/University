using System.ComponentModel.DataAnnotations.Schema; //Librería necesaria para agregar los decoradores []

namespace University.BL.Models
{
    [Table("Enrollment", Schema = "dbo")]
    public class Enrollment
    {
        public int EnrollmentID { get; set; }

        [ForeignKey("Course")]
        public int CourseID { get; set; }

        [ForeignKey("Student")]
        public int StudentID { get; set; }

        //NAVEGACIÓN LÓGICA HACIA LAS OTRAS TABLAS REFERENCIADAS EN LAS LLAVES FORÁNEAS
        public Course Course { get; set; }
        public Student Student { get; set; }
    }
}
