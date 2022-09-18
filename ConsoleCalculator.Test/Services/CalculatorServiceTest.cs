using ConsoleCalculator.Services;
using Moq;
using System.Data;

namespace ConsoleCalculator.Test.Services
{
    public class CalculatorServiceTest
    {
        private Mock<ICalculatorService> _calculatorService = new Mock<ICalculatorService>();
        Mock<ICalculatorValidationService> _valculatorValidationService = new Mock<ICalculatorValidationService>();

        public CalculatorServiceTest()
        {
            _calculatorService
                .Setup(q => q.GetLineCount(It.IsAny<string>()))
                .Returns(It.IsAny<int>());

            _calculatorService
                .Setup(q => q.GetLineCount(It.IsRegex("[^0-9]")))
                .Throws(new SyntaxErrorException());

            _valculatorValidationService
                .Setup(q => q.ValidateLineOfOperations(""))
                .Throws(new SyntaxErrorException());
        }


        #region GetLineCount

        [Fact]
        public void When_Call_GetLineCount_Method_Should_Call_Exactly_One_ValidateLineCount_Method()
        {
            CalculatorService calculatorService = new CalculatorService(_valculatorValidationService.Object);

            calculatorService.GetLineCount(It.IsAny<string>());

            _valculatorValidationService.Verify(q => q.ValidateLineCount(It.IsAny<string>()), Times.Once);
        }


        [Theory]
        [InlineData("abc")]
        [InlineData("123a")]
        [InlineData("#123")]
        [InlineData("12.3")]
        public void When_String_Input_Not_A_Number_Should_Return_Exception(string number)
        {
            Assert.Throws<SyntaxErrorException>(() => _calculatorService.Object.GetLineCount(number));
        }


        [Fact]
        public void When_String_Input_Is_A_Number_Should_Return_Int_Number()
        {
            Assert.IsType<int>(_calculatorService.Object.GetLineCount("123"));
        }

        #endregion


        #region CalculateInput

        public void When_Call_CalculateInput_Method_Should_Call_Exactly_One_ValidateLineOfOperations_Method()
        {
            CalculatorService calculatorService = new CalculatorService(_valculatorValidationService.Object);

            calculatorService.CalculateInput(It.IsAny<string>());

            _valculatorValidationService.Verify(q => q.ValidateLineCount(It.IsAny<string>()), Times.Once);
        }


        [Theory]
        [InlineData("")]
        [InlineData("1 9")]
        [InlineData("-9 3")]
        [InlineData("+5 1")]
        public void When_Is_Not_Valid_String_Operation_Should_Return_Exception(string operations)
        {
            CalculatorService calculatorService = new CalculatorService(_valculatorValidationService.Object);

            Assert.Throws<SyntaxErrorException>(() => calculatorService.CalculateInput(operations));
        }


        [Theory]
        [InlineData("-1,1", 0)]
        [InlineData("-5 -1", -6)]
        [InlineData("17+1", 18)]
        [InlineData("-0 +0", 0)]
        [InlineData("6+2-1-5", 2)]
        [InlineData(",6+ 2 -1,5,", 12)]
        public void When_Is_Valid_String_Operation_Should_Return_Sum_Of_Operation(string operations, int sum)
        {
            CalculatorService calculatorService = new CalculatorService(_valculatorValidationService.Object);

            var excepted = calculatorService.CalculateInput(operations);

            Assert.IsType<int>(excepted);
            Assert.Equal(excepted, sum);
        }

        #endregion
    }
}
