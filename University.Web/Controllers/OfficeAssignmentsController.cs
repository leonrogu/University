using PagedList;
using System;
using System.Linq;
using System.Web.Mvc;
using University.BL.Data;
using University.BL.DTOs;

namespace University.Web.Controllers
{
    public class OfficeAssignmentsController : Controller
    {
        private readonly UniversityContext context = new UniversityContext();
        // GET: OfficeAssignments
        public ActionResult Index(int? pageSize, int? page)
        {
            // SELECT * FROM OfficeAssigment
            var query = context.OfficeAssignments.Include("Instructor").ToList();
            var offices = query.Select(x => new OfficeAssignmentsDTO
            {
                InstructorID = x.InstructorID,
                Location = x.Location,
                Instructor = new InstructorDTO
                {
                    FirstMidName = x.Instructor.FirstMidName,
                    LastName = x.Instructor.LastName
                }

            });

            #region Paginacion
            pageSize = (pageSize ?? 10);
            page = (page ?? 1);
            ViewBag.PageSize = pageSize;
            #endregion

            return View(offices.ToPagedList(page.Value, pageSize.Value));
        }
        [HttpGet]
        public ActionResult Create()
        {
            LoadData();

            return View();
        }

        [HttpPost]
        public ActionResult Create(OfficeAssignmentsDTO office)
        {
            LoadData();

            if (!ModelState.IsValid)
                return View(ModelState);

            context.OfficeAssignments.Add(new BL.Models.OfficeAssignment
            {
                InstructorID = office.InstructorID,
                Location = office.Location
            });
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        private void LoadData()
        {

            var Instructors = context.Instructors.Select(x => new InstructorDTO

            {
                ID = x.ID,
                FirstMidName = x.FirstMidName,
                LastName = x.LastName
            }).ToList();

            //value - text
            ViewData["Instructors"] = new SelectList(Instructors, "ID", "FullName");

        }

        [HttpGet]
        public ActionResult Edit(int id)
        {

            var OfficeAssignment = context.OfficeAssignments.Where(x => x.InstructorID == id)
                             .Select(x => new OfficeAssignmentsDTO
                             {
                                 InstructorID = x.InstructorID,
                                 Location = x.Location
                             }).FirstOrDefault();


            return View(OfficeAssignment);
        }
   
        [HttpPost]
        public ActionResult Edit(OfficeAssignmentsDTO officeAssignment)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(officeAssignment);

                //var studentModel = context.Students.Where(x => x.ID == student.ID).Select(x => x).FirstOrDefault();
                var officeAssignmentsModel = context.OfficeAssignments.FirstOrDefault(x => x.InstructorID == officeAssignment.InstructorID);

                //CAMPOS QUE SE VAN A MODIFICAR
                //SE SOBRESCRIBE LAS PROPIEDADES DEL MOELO DE LA BD
                officeAssignmentsModel.InstructorID = officeAssignment.InstructorID;
                officeAssignmentsModel.Location = officeAssignment.Location;

                //UPDATE Student SET LastName = @LastName, FirstMidName = @FirstMidName, EnrollmentDate = @EnrollmentDate WHERE ID = @ID;

                //AQUÍ SE APLICAN LOS CAMBIOS EN LA BD
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(officeAssignment);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (context.OfficeAssignments.Any(x => x.InstructorID == id))
            {

                var officeAssignmentsModel = context.OfficeAssignments.FirstOrDefault(x => x.InstructorID == id);
                context.OfficeAssignments.Remove(officeAssignmentsModel);
                context.SaveChanges();

            }

            return RedirectToAction("Index");
        }
    }
}