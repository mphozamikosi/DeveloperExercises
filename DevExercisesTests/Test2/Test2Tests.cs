using Microsoft.VisualStudio.TestPlatform.TestHost;
using NSubstitute.Routing.Handlers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test2;

namespace DevExercisesTests.Test1
{
    public class Test1Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void GivenProgramStart_WhenEmptyStringGiven_ReturnZero()
        {
            var program = new Test2.Program();

            int result = program.Add("");

            Assert.AreEqual(0, result);
        }

        public static IEnumerable TestCases
        {
            get
            {
                yield return new TestCaseData("", 0);
                yield return new TestCaseData("1\n2,3", 6);
                yield return new TestCaseData("//;\n1;2", 3);
            }
        }

        [TestCaseSource(nameof(TestCases))]
        public void GivenProgramStart_WhenSpecificDelimeterGiven_ReturnResultExpected(string inputString, int expectedResult)
        {
            var program = new Test2.Program();

            int result = program.Add(inputString);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void GivenProgramStart_WhenInvalidStringGiven_ThrowException()
        {
            var program = new Test2.Program();

            Assert.Throws<Exception>(() => program.Add("1,\n"));
        }
    }
}
