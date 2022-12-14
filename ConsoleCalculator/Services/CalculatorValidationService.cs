using System.Data;
using System.Text.RegularExpressions;

namespace ConsoleCalculator.Services
{
    public class CalculatorValidationService : ICalculatorValidationService
    {
        public void ValidateLineCount(string lineCount)
        {
            if (!Regex.IsMatch(lineCount, "[0-9]"))
            {
                throw new SyntaxErrorException("Your input is not valid!");
            }
        }

        public void ValidateLineOfOperations(string operations)
        {
            if (string.IsNullOrEmpty(operations))
            {
                throw new SyntaxErrorException("Your input is not valid!");
            }
        }
    }
}
