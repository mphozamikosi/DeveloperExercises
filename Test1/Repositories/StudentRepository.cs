using Test1.Helpers;
using System.Xml.Linq;
using Test1.Models;

namespace Test1.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly IFileWrapper _fileWrapper;
        private readonly IConfiguration _configuration;
        private readonly string _fileLocation = ""; 
        public StudentRepository(IFileWrapper fileWrapper, IConfiguration configuration) 
        {
            _fileWrapper = fileWrapper;
            _configuration = configuration;
            _fileLocation = _configuration["StudentFileLocation"];
        }

        public List<Student> ListStudents(string fileLocation)
        {
            var students = new List<Student>();
            try
            {
                //if (!_fileWrapper.FileExists(_fileLocation))
                //{
                //    _fileWrapper.CreateFile(_fileLocation);
                //    var student = new Student();
                //    CreateStudent(student);
                //}
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

        public Student GetStudent(int id)
        {
            try 
            { 
                //ValidateFileExistence();
                XDocument xDocument = XDocument.Load(_fileLocation);
                XElement studentElement = xDocument.Root.Elements("Student")
                    .FirstOrDefault(e => (int)e.Element("Id") == id);

                var student = new Student();

                student.Id = int.Parse(studentElement.Element("Id").Value);
                student.Name = studentElement.Element("Name").Value;
                student.Surname = studentElement.Element("Surname").Value;
                student.CellNumber = studentElement.Element("CellNumber").Value;
                return student;
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message, ex);

            }
        }


        public void CreateStudent(Student student)
        {
            try
            {
                //ValidateFileExistence();

                //var currentStudents = ListStudents();
                //int newId = currentStudents.OrderByDescending(student => student.Id).Select(i => i.Id).First() + 1;

                XDocument document = XDocument.Load(_fileLocation);

                XElement newStudent = new XElement("Student",
                    new XElement("Id", 1 /*newId*/),
                    new XElement("Name", student.Name),
                    new XElement("Surname", student.Surname),
                    new XElement("CellNumber", student.CellNumber)
                );

                document.Element("Students").Add(newStudent);

                document.Save(_fileLocation);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public void EditStudent(Student student)
        {
            try
            {
                //ValidateFileExistence();

                XDocument xDocument = XDocument.Load(_fileLocation);
                XElement studentElement = xDocument.Root.Elements("Student")
                    .FirstOrDefault(e => (int)e.Element("Id") == student.Id);

                studentElement.SetElementValue("Name", student.Name);
                studentElement.SetElementValue("Surname", student.Surname);
                studentElement.SetElementValue("CellNumber", student.CellNumber);
                xDocument.Save(_fileLocation);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }
        public void DeleteStudent(int id)
        {
            try
            {
                //ValidateFileExistence();

                XDocument document = XDocument.Load(_fileLocation);

                document.Root.Elements("Student")
                    .Where(e => (int)e.Element("Id") == id)
                    .Remove();

                document.Save(_fileLocation);
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
                var students = new List<Student>();
                //_fileWrapper.CreateFile(_fileLocation);
                XDocument document = new XDocument(
                    new XElement("Students"));
                        //from student in students
                        //select new XElement("Student",
                        //    new XElement("Id", student.Id),
                        //    new XElement("Name", student.Name),
                        //    new XElement("Surname", student.Surname),
                        //    new XElement("CellNumber", student.CellNumber)
                        //)
                //     )
                //);
                document.Save(fileLocation);
                //CreateStudent(student);
            }
        }
    }
}
