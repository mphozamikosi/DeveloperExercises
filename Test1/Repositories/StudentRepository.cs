using Test1.Helpers;
using System.Xml.Linq;
using Test1.Models;

namespace Test1.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly IFileWrapper _fileWrapper;
        private readonly IConfiguration _configuration;
        public StudentRepository(IFileWrapper fileWrapper, IConfiguration configuration) 
        {
            _fileWrapper = fileWrapper;
            _configuration = configuration;
        }

        public List<Student> ListStudents(string fileLocation)
        {
            var students = new List<Student>();
            try
            {
                ValidateFileExistence(fileLocation);

                XDocument document = XDocument.Load(fileLocation);
                foreach (XElement studentElement in document.Element("Students").Elements("Student"))
                {
                    students.Add(new Student
                    {
                        Id = int.Parse(studentElement.Element("Id").Value),
                        Name = studentElement.Element("Name").Value,
                        Surname = studentElement.Element("Surname").Value,
                        CellNumber = studentElement.Element("CellNumber").Value,
                    });
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return students;
        }

        public Student GetStudent(int id, string fileLocation)
        {
            try 
            { 
                ValidateFileExistence(fileLocation);
                XDocument xDocument = XDocument.Load(fileLocation);
                XElement studentElement = xDocument.Root.Elements("Student")
                    .FirstOrDefault(e => (int)e.Element("Id") == id);

                if (studentElement != null)
                {
                    var student = new Student
                    {
                        Id = int.Parse(studentElement.Element("Id").Value),
                        Name = studentElement.Element("Name").Value,
                        Surname = studentElement.Element("Surname").Value,
                        CellNumber = studentElement.Element("CellNumber").Value
                    };
                    return student;
                }
                else
                {
                    throw new Exception("Error: Student does not exist");
                }
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message, ex);

            }
        }


        public void CreateStudent(Student student, string fileLocation)
        {
            int newId = 1;
            try
            {
                ValidateFileExistence(fileLocation);

                var currentStudents = ListStudents(fileLocation);
                if(currentStudents.Count > 0)
                    newId = currentStudents.OrderByDescending(student => student.Id).Select(i => i.Id).First() + 1;

                XDocument document = XDocument.Load(fileLocation);

                XElement newStudent = new XElement("Student",
                    new XElement("Id", newId),
                    new XElement("Name", student.Name),
                    new XElement("Surname", student.Surname),
                    new XElement("CellNumber", student.CellNumber)
                );

                document.Element("Students").Add(newStudent);

                document.Save(fileLocation);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public void EditStudent(Student student, string fileLocation)
        {
            try
            {
                ValidateFileExistence(fileLocation);

                XDocument xDocument = XDocument.Load(fileLocation);
                XElement studentElement = xDocument.Root.Elements("Student")
                    .FirstOrDefault(e => (int)e.Element("Id") == student.Id);

                studentElement.SetElementValue("Name", student.Name);
                studentElement.SetElementValue("Surname", student.Surname);
                studentElement.SetElementValue("CellNumber", student.CellNumber);
                xDocument.Save(fileLocation);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }
        public void DeleteStudent(int id, string fileLocation)
        {
            try
            {
                ValidateFileExistence(fileLocation);

                XDocument document = XDocument.Load(fileLocation);

                document.Root.Elements("Student")
                    .Where(e => (int)e.Element("Id") == id)
                    .Remove();

                document.Save(fileLocation);
            }
            catch(Exception ex )
            {
                throw new Exception (ex.Message, ex);
            }
        }


        private void ValidateFileExistence(string fileLocation)
        {
            if (!_fileWrapper.FileExists(fileLocation))
            {
                XDocument document = new XDocument(
                    new XElement("Students"));
                document.Save(fileLocation);
            }
        }
    }
}
