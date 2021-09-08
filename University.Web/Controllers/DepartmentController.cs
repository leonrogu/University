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
    public class DepartmentController : Controller
    {
        private readonly UniversityContext context = new UniversityContext();

        //  Department/Index {controller}/{action}
        [HttpGet]
        public ActionResult Index(int? departmentId, int? pageSize, int? page)
        {
            #region Listar department
            //SELECT * FROM Students
            var query = context.Departments.ToList();

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
            var students = query.Where(x => x.StartDate < DateTime.Now)
                                .Select(x => new DepartmentDTO
                                {
                                    DepartmentID = x.DepartmentID,
                                    Name = x.Name,
                                    Budget = x.Budget,
                                    StartDate = x.StartDate
                                }).ToList();
            #endregion
            var instructor = (from q in context.Instructors
                              join r in context.Departments on q.ID equals r.InstructorID
                           where r.DepartmentID == departmentId
                              select new InstructorDTO
                           {
                               ID = q.ID,
                               FirstMidName = q.FirstMidName,
                               LastName = q.LastName
                           }).ToList();

            ViewBag.Instructor = instructor;


            ViewBag.Data = "Data de prueba";
            ViewBag.Message = "Mensaje de prueba";

            //ViewData["Data"] = "Data de prueba";
            //ViewData["Message"] = "Mensaje de prueba";

            #region Paginación
            pageSize = (pageSize ?? 10);
            page = (page ?? 1);
            ViewBag.PageSize = pageSize;
            #endregion

            return View(students.ToPagedList(page.Value, pageSize.Value));
        }



        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (context.Departments.Any(x => x.DepartmentID == id))
            {
                var departmenttModel = context.Departments.FirstOrDefault(x => x.DepartmentID == id);
                context.Departments.Remove(departmenttModel);
                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(DepartmentDTO Department)
        {

          
            try
            {
                if (!ModelState.IsValid)
                    return View(Department);

                if (Department.StartDate > DateTime.Now)
                    throw new Exception("La fecha de matricula no puede ser mayor a la fecha actual");

                //INSERT INTO Students(FirstMidName,LastName,EnrollmentDate) VALUES(@FirstMidName, @LastName, @EnrollmentDate)
                context.Departments.Add(new Department
                {
                    
                    Name = Department.Name,
                    Budget = Department.Budget,
                    StartDate = Department.StartDate,
                    InstructorID = 1,
                });
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
         

            return View(Department);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            //var query = context.Students.Find(id);
            var Department = context.Departments.Where(x => x.DepartmentID == id)
                                          .Select(x => new DepartmentDTO
                                          {
                                              DepartmentID = x.DepartmentID,
                                              Name = x.Name,
                                              Budget = x.Budget,
                                              StartDate = x.StartDate,
                                              
                                          }).FirstOrDefault();

            return View(Department);
        }
        [HttpPost]
        public ActionResult Edit(DepartmentDTO Department)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(Department);

                if (Department.StartDate > DateTime.Now)
                    throw new Exception("La fecha de matricula no puede ser mayor a la fecha actual");

                //var studentModel = context.Students.Where(x => x.ID == student.ID).Select(x => x).FirstOrDefault();
                var DepartmentModel = context.Departments.FirstOrDefault(x => x.DepartmentID == Department.DepartmentID);

                //campos que se van a modificar
                //sobreescribo las propiedades del modelo de base de datos
                DepartmentModel.DepartmentID = Department.DepartmentID;
                DepartmentModel.Name = Department.Name;
                DepartmentModel.Budget = Department.Budget;
                DepartmentModel.StartDate = Department.StartDate;

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

            return View(Department);
        }
    }
    
}

