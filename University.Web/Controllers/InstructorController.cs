using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using University.BL.Data;
using University.BL.DTOs;
using University.BL.Models;


namespace University.Web.Controllers
{
    public class InstructorController : Controller
    {
        private readonly UniversityContext context = new UniversityContext();

        // GET: Instructor
        [HttpGet]
        public ActionResult Index(int? departmentId, int? pageSize, int? page)
        {

            #region Listar Instructor
            //SELECT * FROM Students
            var query = context.Instructors.ToList();

            //var students = (from q in query
            //                where q.EnrollmentDate < DateTime.Now
            //                select new StudentDTO
            //                {
            //                    ID = q.ID,
            //                    LastName = q.LastName,
            //                    FirstMidName = q.FirstMidName,
            //                    EnrollmentDate = q.EnrollmentDate
            //                }).ToList();

            //Linq
            var instructor = query.Where(x => x.HireDate < DateTime.Now)
                                .Select(x => new InstructorDTO
                                {
                                    ID = x.ID,
                                    LastName = x.LastName,                                   
                                    FirstMidName = x.FirstMidName,
                                    HireDate = x.HireDate
                                }).ToList();
            #endregion

            var department = (from q in context.Instructors
                              join r in context.Departments on q.ID equals r.InstructorID
                              where r.DepartmentID == departmentId
                              select new DepartmentDTO
                              {
                                  DepartmentID = r.DepartmentID,
                                  Name = r.Name,
                                  Budget = r.Budget,
                                  StartDate = r.StartDate
                              }).ToList();

            ViewBag.Department = department;

            ViewBag.Data = "Data de prueba";
            ViewBag.Message = "Mensaje de prueba";

            //ViewData["Data"] = "Data de prueba";
            //ViewData["Message"] = "Mensaje de prueba";

            #region Paginación
            pageSize = (pageSize ?? 10);
            page = (page ?? 1);
            ViewBag.PageSize = pageSize;
            #endregion

            return View(instructor.ToPagedList(page.Value, pageSize.Value));
        }

        // GET: Instructor/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Instructor/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Instructor/Create
        [HttpPost]
        public ActionResult Create(InstructorDTO Instructor)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(Instructor);

                if (Instructor.HireDate > DateTime.Now)
                    throw new Exception("La fecha de matricula no puede ser mayor a la fecha actual");

                //INSERT INTO Students(FirstMidName,LastName,EnrollmentDate) VALUES(@FirstMidName, @LastName, @EnrollmentDate)
                context.Instructors.Add(new Instructor{
                    FirstMidName = Instructor.FirstMidName,
                    LastName = Instructor.LastName,
                    HireDate = Instructor.HireDate
                });
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(Instructor);
        }

        // GET: Instructor/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            //var query = context.Students.Find(id);
            var instructor = context.Instructors.Where(x => x.ID == id)
                                          .Select(x => new InstructorDTO
                                          {
                                              ID = x.ID,
                                              LastName = x.LastName,
                                              FirstMidName = x.FirstMidName,
                                              HireDate = x.HireDate
                                          }).FirstOrDefault();

            return View(instructor);
        }

        // POST: Instructor/Edit/5
        [HttpPost]
        public ActionResult Edit(InstructorDTO instructor)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(instructor);

                if (instructor.HireDate > DateTime.Now)
                    throw new Exception("La fecha de matricula no puede ser mayor a la fecha actual");

                //var studentModel = context.Students.Where(x => x.ID == student.ID).Select(x => x).FirstOrDefault();
                var instructorModel = context.Instructors.FirstOrDefault(x => x.ID == instructor.ID);

                //campos que se van a modificar
                //sobreescribo las propiedades del modelo de base de datos
                instructorModel.LastName = instructor.LastName;
                instructorModel.FirstMidName = instructor.FirstMidName;
                instructorModel.HireDate = instructor.HireDate;

                //  UPDATE Student 
                //  SET LastName = @LastName, FirstMidName = @FirstMidName, EnrollmentDate = @EnrollmentDate 
                //  WHERE ID = @ID;

                //aplique los cambios en base de datos
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(instructor);
        }

        // GET: Instructor/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                if (context.Instructors.Any(x => x.ID == id))
            {
                var InstructorModel = context.Instructors.FirstOrDefault(x => x.ID == id);
                context.Instructors.Remove(InstructorModel);
                context.SaveChanges();
            }

            return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        
    }
}
