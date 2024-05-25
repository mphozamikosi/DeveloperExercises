using Test1.Models;

namespace Test1.Repositories
{
    public interface IStudentRepository
    {
        public List<Student> ListStudents(string fileLocation);
        public Student GetStudent(int id);
        public void CreateStudent(Student student);
        public void EditStudent(Student student);
        public void DeleteStudent(int id);
    }
}
