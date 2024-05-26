using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Test1.Models;
using Test1.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Test3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        public readonly ISimpleStudentRepository _simpleStudentRepository;
        private readonly IConfiguration _configuration;
        private readonly string _fileLocation = "";
        public StudentController(ISimpleStudentRepository simpleStudentRepository, IConfiguration configuration)
        {
            _simpleStudentRepository = simpleStudentRepository;
            _configuration = configuration;
            _fileLocation = configuration["StudentFileLocation"];
        }
        // GET: api/<StudentController>
        [HttpGet]
        [Route("GetAllStudents")]
        public IEnumerable<SimpleStudent> Get()
        {
            return _simpleStudentRepository.ListStudents(_fileLocation);
        }

        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        public SimpleStudent Get(int id)
        {
            return _simpleStudentRepository.GetStudent(id, _fileLocation);
        }

        // POST api/<StudentController>
        [HttpPost]
        [Route("CreateSudent")]
        public void Post(SimpleStudent student)
        {
            _simpleStudentRepository.CreateStudent(student, _fileLocation);
        }


        // DELETE api/<StudentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _simpleStudentRepository.DeleteStudent(id, _fileLocation);
        }
    }
}
