using Test1.Models;

namespace Test1.Repositories
{
    public interface IStudentRepository
    {
        List<Student> ListStudents(string fileLocation);
        Student GetStudent(int id, string fileLocation);
        void CreateStudent(Student student, string fileLocation);
        void EditStudent(Student student, string fileLocation);
        void DeleteStudent(int id, string fileLocation);
    }
}
