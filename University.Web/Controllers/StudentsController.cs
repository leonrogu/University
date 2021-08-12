using System;
using System.Collections.Generic;
using System.Web.Mvc;
using University.Web.Models;

namespace University.Web.Controllers
{
    public class StudentsController : Controller
    {
        //  Students/Index {controller}/{action}
        [HttpGet]
        public ActionResult Index()
        {
            var students = new List<Student>(); //tamaño no fijo
            students.Add(new Student { ID = 1, FirstMidName = "David", LastName = "Santafe", EnrollmentDate = DateTime.Now });
            students.Add(new Student { ID = 2, FirstMidName = "Carson", LastName = "Alexander", EnrollmentDate = DateTime.Now });
            students.Add(new Student { ID = 3, FirstMidName = "Meredith", LastName = "Alonso", EnrollmentDate = DateTime.Now });

            ViewBag.Data = "Data de prueba";
            ViewBag.Message = "Mensaje de prueba";

            //ViewData["Data"] = "Data de prueba";
            //ViewData["Message"] = "Mensaje de prueba";

            return View(students);
        }

        // Students/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Student student)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(student);

                if (student.EnrollmentDate > DateTime.Now)
                    throw new Exception("La fecha de matricula no puede ser mayor a la fecha actual");

                var id = student.ID;
                var firstMidName = student.FirstMidName;
                var lastName = student.LastName;
                var enrollmentDate = student.EnrollmentDate;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(student);
        }
    }
}