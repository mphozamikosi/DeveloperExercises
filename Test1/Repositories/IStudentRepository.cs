using Test1.Models;

namespace Test1.Repositories
{
    public interface IStudentRepository
    {
        public List<Student> ListStudents(string fileLocation);
        public Student GetStudent(int id, string fileLocation);
        public void CreateStudent(Student student, string fileLocation);
        public void EditStudent(Student student, string fileLocation);
        public void DeleteStudent(int id, string fileLocation);
    }
}
