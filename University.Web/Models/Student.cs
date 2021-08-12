using System;
using System.ComponentModel.DataAnnotations;

namespace University.Web.Models
{
    public class Student
    {
        [Display(Name = "ID")]
        [Required(ErrorMessage = "El campo ID es requerido.")]
        public int ID { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "El campo Last Name es requerido.")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        public string LastName { get; set; }

        [Display(Name = "First MidName")]
        [Required(ErrorMessage = "El campo First MidName es requerido.")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        public string FirstMidName { get; set; }

        [Display(Name = "Enrollment Date")]
        [Required(ErrorMessage = "El campo Enrollment Date es requerido.")]
        [DataType(DataType.DateTime)]
        public DateTime EnrollmentDate { get; set; }
    }
}