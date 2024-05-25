using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test1.Models;
using Test1.Repositories;

namespace Test1.Controllers
{
    public class StudentController : Controller
    {
        public static readonly Student student = new Student();
        public readonly IStudentRepository _studentRepository;
        public StudentController(IStudentRepository studentRepository) 
        {
            _studentRepository = studentRepository;
        }
        // GET: StudentController
        public ActionResult Index()
        {
            var students = _studentRepository.ListStudents("../Test1/FileLocation/Students.xml");
            return View(students);
        }

        // GET: StudentController/Details/5
        public ActionResult Details(int id)
        {
            return View(_studentRepository.GetStudent(id));
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {
            var student = new Student();
            return View(student);
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student)
        {
            try
            {
                _studentRepository.CreateStudent(student);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_studentRepository.GetStudent(id));
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Student student)
        {
            try
            {
                _studentRepository.EditStudent(student);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_studentRepository.GetStudent(id));
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _studentRepository.DeleteStudent(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
