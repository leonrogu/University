using PagedList;
using System;
using System.Linq;
using System.Web.Mvc;
using University.BL.Data;
using University.BL.DTOs;
using University.BL.Models;

namespace University.Web.Controllers
{
    public class CourseController : Controller
    {
        private readonly UniversityContext context = new UniversityContext();

        // GET: Course
        [HttpGet]
        public ActionResult Index(int? courseId, int? pageSize, int? page)
        {
            var query = context.Courses.ToList();

            //CONSULTA DE LA FORMA 2
            var students = query.Select(x => new CourseDTO
            {
                CourseID = x.CourseID,
                Title = x.Title,
                Credits = x.Credits
            }).ToList();

            if (courseId != null)
            {

                // SELECT r.*
                //FROM[dbo].[Enrollment] q
                //JOIN Course r ON q.CourseID = r.CourseID
                //WHERE q.StudentID = 1

                var instructors = (from q in context.CourseInstructors
                                   join r in context.Instructors on q.InstructorID equals r.ID
                                   where q.CourseID == courseId
                                   select new InstructorDTO
                                   {
                                       ID = r.ID,
                                       FirstMidName = r.FirstMidName,
                                       LastName = r.LastName
                                   }).ToList();

                ViewBag.Instructors = instructors;
            }

            //ViewData["Data"] = "Data de prueba";
            //ViewData["Message"] = "Mensaje de prueba";


            #region Paginacion
            pageSize = (pageSize ?? 10);
            page = (page ?? 1);
            ViewBag.PageSize = pageSize;
            #endregion

            return View(students.ToPagedList(page.Value, pageSize.Value));
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            //var query = context.Students.Find(id);
            //CONSULTA DE LA FORMA 2
            var course = context.Courses.Where(x => x.CourseID == id)
                             .Select(x => new CourseDTO
                             {
                                 CourseID = x.CourseID,
                                 Title = x.Title,
                                 Credits = x.Credits,
                             }).FirstOrDefault();


            return View(course);
        }

        [HttpPost]
        public ActionResult Edit(CourseDTO course)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(course);

                //var studentModel = context.Students.Where(x => x.ID == student.ID).Select(x => x).FirstOrDefault();
                var courseModel = context.Courses.FirstOrDefault(x => x.CourseID == course.CourseID);

                //CAMPOS QUE SE VAN A MODIFICAR
                //SE SOBRESCRIBE LAS PROPIEDADES DEL MOELO DE LA BD
                courseModel.Title = course.Title;
                courseModel.Credits = course.Credits;

                //UPDATE Student SET LastName = @LastName, FirstMidName = @FirstMidName, EnrollmentDate = @EnrollmentDate WHERE ID = @ID;

                //AQUÍ SE APLICAN LOS CAMBIOS EN LA BD
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(course);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (!context.CourseInstructors.Any(x => x.CourseID == id))
            {


                var courseModel = context.Courses.FirstOrDefault(x => x.CourseID == id);
                context.Courses.Remove(courseModel);
                context.SaveChanges();

            }
            else
            {
                ViewBag.Data = "No se puede eliminar el curso";
                ViewBag.Message = "Hay Instructores relacionados al curso";
            }

            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CourseDTO course)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(course);

                //INSERT INTO Students(FirstMidName,LastName,EnrollmentDate) VALUES(@FirstMidName, @LastName, @EnrollmentDate)
                context.Courses.Add(new Course
                {
                    CourseID = course.CourseID,
                    Title = course.Title,
                    Credits = course.Credits
                });

                context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(course);
        }
    }
}