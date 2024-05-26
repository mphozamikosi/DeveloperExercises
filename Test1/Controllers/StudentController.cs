using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test1.Models;
using Test1.Repositories;

namespace Test1.Controllers
{
    public class StudentController : Controller
    {
        public readonly IStudentRepository _studentRepository;
        private readonly IConfiguration _configuration;
        private readonly string _fileLocation = "";
        public StudentController(IStudentRepository studentRepository, IConfiguration configuration) 
        {
            _studentRepository = studentRepository;
            _configuration = configuration;
            _fileLocation = configuration["StudentFileLocation"];
        }
        // GET: StudentController
        public ActionResult Index()
        {
            var students = _studentRepository.ListStudents(_fileLocation);
            return View(students);
        }

        // GET: StudentController/Details/5
        public ActionResult Details(int id)
        {
            return View(_studentRepository.GetStudent(id, _fileLocation));
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
                _studentRepository.CreateStudent(student, _fileLocation);
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
            return View(_studentRepository.GetStudent(id, _fileLocation));
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Student student)
        {
            try
            {
                _studentRepository.EditStudent(student, _fileLocation);
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
            return View(_studentRepository.GetStudent(id, _fileLocation));
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _studentRepository.DeleteStudent(id, _fileLocation);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
