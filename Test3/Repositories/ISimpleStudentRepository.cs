using Test1.Models;

namespace Test1.Repositories
{
    public interface ISimpleStudentRepository
    {
        List<SimpleStudent> ListStudents(string fileLocation);
        SimpleStudent GetStudent(int id, string fileLocation);
        void CreateStudent(SimpleStudent student, string fileLocation);
        void DeleteStudent(int id, string fileLocation);
    }
}
