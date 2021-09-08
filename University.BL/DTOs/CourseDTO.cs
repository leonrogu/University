using System;
using System.ComponentModel.DataAnnotations;

namespace University.BL.DTOs
{
    public class CourseDTO
    {
        [Display(Name = "ID")]
        [Required(ErrorMessage = "El campo ID es requerido.")]
        public int CourseID { get; set; }
       
        [Display(Name = "Title")]
        [Required(ErrorMessage = "El campo Title es requerido.")]
        public String Title { get; set; }
      
        [Display(Name = "Credits")]
        [Required(ErrorMessage = "El campo Credits es requerido.")]
        public int Credits { get; set; } 

    }
}
