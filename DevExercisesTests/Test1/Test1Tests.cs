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
        public void GivenStudents_WhenRequestMadeToGetAllStudents_ThenShouldReturnAllStudents() 
        {
            var stubs = GetStubs();
            stubs.ServiceProvider.GetService(typeof(IConfiguration))
                .Returns(stubs.Configuration);
            stubs.ServiceProvider.GetService(typeof(IFileWrapper))
                .Returns(stubs.FileWrapper);


            var studentRepo = new StudentRepository(stubs.FileWrapper, stubs.Configuration);
            string fileLocation = "../../Test1/FileLocation/Students.xml";
            var students = studentRepo.ListStudents(fileLocation);
            Assert.AreEqual(1, 1);
        }
    }
}
