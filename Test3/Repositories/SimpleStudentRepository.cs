using Test1.Helpers;
using System.Xml.Linq;
using Test1.Models;
using OfficeOpenXml;

namespace Test1.Repositories
{
    public class SimpleStudentRepository : ISimpleStudentRepository
    {
        private readonly IFileWrapper _fileWrapper;
        private readonly IConfiguration _configuration;
        public SimpleStudentRepository(IFileWrapper fileWrapper, IConfiguration configuration) 
        {
            _fileWrapper = fileWrapper;
            _configuration = configuration;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public List<SimpleStudent> ListStudents(string fileLocation)
        {
            var students = new List<SimpleStudent>();
            try
            {
                ValidateFileExistence(fileLocation);

                using (var package = new ExcelPackage(new FileInfo(fileLocation)))
                {
                    var worksheet = package.Workbook.Worksheets[0];

                    int rowCount = worksheet.Dimension.Rows;

                    for(int row = 2; row <= rowCount ; row++)
                    {
                        students.Add(new SimpleStudent()
                        {
                            Id = int.Parse(worksheet.Cells[row, 1].Value.ToString()),
                            Name = worksheet.Cells[row, 2].Value.ToString(),
                            Surname = worksheet.Cells[row, 3].Value.ToString(),
                            CellNumber = worksheet.Cells[row, 4].Value.ToString()
                        });
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return students;
        }

        public SimpleStudent GetStudent(int id, string fileLocation)
        {
            try 
            {
                SimpleStudent student = null;
                using (var package = new ExcelPackage(new FileInfo(fileLocation)))
                {
                    student = GetExistingStudentFromFile(id, student, package);
                }

                if (student == null)
                {
                    throw new Exception("Error: Student does not exist");
                }
                return student;
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message, ex);
            }
        }

        private static SimpleStudent GetExistingStudentFromFile(int id, SimpleStudent student, ExcelPackage package)
        {
            var worksheet = package.Workbook.Worksheets[0];
            int rowCount = worksheet.Dimension.Rows;
            for (int row = 2; row <= rowCount; row++)
            {
                int currentId = int.Parse(worksheet.Cells[row, 1].Value.ToString());
                if (currentId == id)
                {
                    student = AssignExistingStudentDetails(worksheet, row, currentId);
                    break;
                }
            }

            return student;
        }

        public void CreateStudent(SimpleStudent student, string fileLocation)
        {
            int newId = 1;
            try
            {
                ValidateFileExistence(fileLocation);

                var currentStudents = ListStudents(fileLocation);
                if(currentStudents.Count > 0)
                    newId = currentStudents.OrderByDescending(student => student.Id).Select(i => i.Id).First() + 1;

                using (var package = new ExcelPackage(new FileInfo(fileLocation)))
                {
                    var worksheet = package.Workbook.Worksheets[0];

                    int rowCount = worksheet.Dimension.Rows;

                    worksheet.Cells[rowCount + 1, 1].Value = newId;
                    worksheet.Cells[rowCount + 1, 2].Value = student.Name;
                    worksheet.Cells[rowCount + 1, 3].Value = student.Surname;
                    worksheet.Cells[rowCount + 1, 4].Value = student.CellNumber;

                    package.Save();
                }
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
                var student = GetStudent(id, fileLocation);
                if (student != null)
                {
                    var allStudents = ListStudents(fileLocation).Where(i => i.Id != id).ToList();
                    _fileWrapper.DeleteFile(fileLocation);
                    foreach(var oneStudent in allStudents)
                    {
                        CreateStudent(oneStudent, fileLocation);
                    }
                }
                else
                {
                    throw new Exception("Error: Student does not exist");
                }
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
                using (var package = new ExcelPackage(new FileInfo(fileLocation)))
                {
                    var worksheet = package.Workbook.Worksheets.Add("Student Sheet");
                    worksheet.Cells[1, 1].Value = "Id";
                    worksheet.Cells[1, 2].Value = "Name";
                    worksheet.Cells[1, 3].Value = "Surname";
                    worksheet.Cells[1, 4].Value = "Cell Number";
                    worksheet.Column(4).Style.Numberformat.Format = "@";
                    package.Save();
                }

            }
        }

        private static SimpleStudent AssignExistingStudentDetails(ExcelWorksheet worksheet, int row, int currentId)
        {
            return new SimpleStudent
            {
                Id = currentId,
                Name = worksheet.Cells[row, 2].Value.ToString(),
                Surname = worksheet.Cells[row, 3].Value.ToString(),
                CellNumber = worksheet.Cells[row, 4].Value.ToString()
            };
        }
    }
}
