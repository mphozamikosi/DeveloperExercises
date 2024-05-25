using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test1.Models;
using Test1.Repositories;
using Microsoft.Extensions.Configuration;
using Test1.Helpers;
using NSubstitute;

namespace DevExercisesTests.Test1
{
    public class Test2Tests
    {
        [SetUp]

        public void Setup()
        {

        }
        private struct Stubs
        {
            public IFileWrapper FileWrapper { get; set; }
            public IConfiguration Configuration { get; set; }
            public IServiceProvider ServiceProvider { get; set; }
        }
        private static Stubs GetStubs()
        {
            var stubs = new Stubs 
            {
                FileWrapper = Substitute.For<IFileWrapper>(),
                Configuration = Substitute.For<IConfiguration>(),
                ServiceProvider = Substitute.For<IServiceProvider>()
            };
            return stubs;
        }
        //public StudentRepository CreateStudentRepository()
        //{
        //    return new StudentRepository(this);
        //}
        [Test]
        public void GivenStudent_WhenValidFileLocationSpecified_ThenShouldNotReturnNull() 
        {
            var stubs = GetStubs();
            stubs.ServiceProvider.GetService(typeof(IConfiguration))
                .Returns(stubs.Configuration);
            stubs.ServiceProvider.GetService(typeof(IFileWrapper))
                .Returns(stubs.FileWrapper);
            var fileWrapper = new FileWrapper();

            var studentRepo = new StudentRepository(fileWrapper, stubs.Configuration);

            string fileLocation = "../../../../Test1/FileLocation/Students.xml";
            var students = studentRepo.ListStudents(fileLocation);

            Assert.NotNull(students);
        }

        [Test]
        public void GivenStudent_WhenInvalidFileLocationSpecified_ThenShouldThrowException()
        {
            var stubs = GetStubs();
            stubs.ServiceProvider.GetService(typeof(IConfiguration))
                .Returns(stubs.Configuration);
            stubs.ServiceProvider.GetService(typeof(IFileWrapper))
                .Returns(stubs.FileWrapper);
            var fileWrapper = new FileWrapper();

            var studentRepo = new StudentRepository(fileWrapper, stubs.Configuration);
            string fileLocation = "../FileLocation/Students.xml";

            Assert.Throws<Exception>(() => studentRepo.ListStudents(fileLocation));
        }


        [Test]
        public void GivenStudent_WhenValidIdSpecified_ThenShouldReturnStudent()
        {
            var stubs = GetStubs();
            stubs.ServiceProvider.GetService(typeof(IConfiguration))
                .Returns(stubs.Configuration);
            stubs.ServiceProvider.GetService(typeof(IFileWrapper))
                .Returns(stubs.FileWrapper);
            var fileWrapper = new FileWrapper();

            var studentRepo = new StudentRepository(fileWrapper, stubs.Configuration);

            string fileLocation = "../../../../Test1/FileLocation/Students.xml";

            int validId = studentRepo.ListStudents(fileLocation).Select(x => x.Id).FirstOrDefault();
            var students = studentRepo.GetStudent(validId, fileLocation);

            Assert.NotNull(students);
        }


        [Test]
        public void GivenStudent_WhenInvalidIdSpecified_ThenShouldThrowException()
        {
            var stubs = GetStubs();
            stubs.ServiceProvider.GetService(typeof(IConfiguration))
                .Returns(stubs.Configuration);
            stubs.ServiceProvider.GetService(typeof(IFileWrapper))
                .Returns(stubs.FileWrapper);
            var fileWrapper = new FileWrapper();

            var studentRepo = new StudentRepository(fileWrapper, stubs.Configuration);

            string fileLocation = "../../../../Test1/FileLocation/Students.xml";

            Assert.Throws<Exception>(() => studentRepo.GetStudent(-1, fileLocation));
        }

        [Test]
        public void GivenStudent_WhenValidDetailsSpecified_ThenCreateStudentAndDoesNotThrowException()
        {
            var stubs = GetStubs();
            stubs.ServiceProvider.GetService(typeof(IConfiguration))
                .Returns(stubs.Configuration);
            stubs.ServiceProvider.GetService(typeof(IFileWrapper))
                .Returns(stubs.FileWrapper);
            var fileWrapper = new FileWrapper();
            var studentRepo = new StudentRepository(fileWrapper, stubs.Configuration);

            string fileLocation = "../../../../Test1/FileLocation/Students.xml";
            var student = new Student
            {
                Name = "Test Name",
                Surname = "Test Surname",
                CellNumber = "0665775467"
            };

            Assert.DoesNotThrow(() =>
            studentRepo.CreateStudent(student, fileLocation));
        }

        [Test]
        public void GivenStudent_WhenValidDetailsSpecified_ThenEditsStudentAndDoesNotThrowException()
        {
            var stubs = GetStubs();
            stubs.ServiceProvider.GetService(typeof(IConfiguration))
                .Returns(stubs.Configuration);
            stubs.ServiceProvider.GetService(typeof(IFileWrapper))
                .Returns(stubs.FileWrapper);
            var fileWrapper = new FileWrapper();
            var studentRepo = new StudentRepository(fileWrapper, stubs.Configuration);

            string fileLocation = "../../../../Test1/FileLocation/Students.xml";
            int validId = studentRepo.ListStudents(fileLocation).Select(x => x.Id).FirstOrDefault();

            var student = new Student
            {
                Id = validId,
                Name = "Edit Name",
                Surname = "Edit Surname",
                CellNumber = "0665775467"
            };

            Assert.DoesNotThrow(() =>
            studentRepo.EditStudent(student, fileLocation));
        }

        [Test]
        public void GivenStudent_WhenInvalidIdSpecified_ThenThrowException()
        {
            var stubs = GetStubs();
            stubs.ServiceProvider.GetService(typeof(IConfiguration))
                .Returns(stubs.Configuration);
            stubs.ServiceProvider.GetService(typeof(IFileWrapper))
                .Returns(stubs.FileWrapper);
            var fileWrapper = new FileWrapper();
            var studentRepo = new StudentRepository(fileWrapper, stubs.Configuration);

            string fileLocation = "../../../../Test1/FileLocation/Students.xml";

            var student = new Student
            {
                Id = -54,
                Name = "Edit Name",
                Surname = "Edit Surname",
                CellNumber = "0665775467"
            };

            Assert.Throws<Exception>(() =>
            studentRepo.EditStudent(student, fileLocation));
        }


        [Test]
        public void GivenStudent_WhenValidDetailsSpecified_ThenDeletesStudentAndDoesNotThrowException()
        {
            var stubs = GetStubs();
            stubs.ServiceProvider.GetService(typeof(IConfiguration))
                .Returns(stubs.Configuration);
            stubs.ServiceProvider.GetService(typeof(IFileWrapper))
                .Returns(stubs.FileWrapper);
            var fileWrapper = new FileWrapper();
            var studentRepo = new StudentRepository(fileWrapper, stubs.Configuration);

            string fileLocation = "../../../../Test1/FileLocation/Students.xml";
            int validId = studentRepo.ListStudents(fileLocation).Select(x => x.Id).FirstOrDefault();

            Assert.DoesNotThrow(() =>
            studentRepo.DeleteStudent(validId, fileLocation));
        }

        [Test]
        public void GivenStudent_WhenInvalidIdSpecified_ThenDoesNotDeleteAndThrowException()
        {
            var stubs = GetStubs();
            stubs.ServiceProvider.GetService(typeof(IConfiguration))
                .Returns(stubs.Configuration);
            stubs.ServiceProvider.GetService(typeof(IFileWrapper))
                .Returns(stubs.FileWrapper);
            var fileWrapper = new FileWrapper();
            var studentRepo = new StudentRepository(fileWrapper, stubs.Configuration);

            string fileLocation = "../../../../Test1/FileLocation/Students.xml";

            Assert.DoesNotThrow(() =>
            studentRepo.DeleteStudent(-60, fileLocation));
        }
    }
}
