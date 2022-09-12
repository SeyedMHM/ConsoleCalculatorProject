using System.Text.RegularExpressions;

namespace ConsoleCalculator.Services
{
    public class CalculatorValidationService : ICalculatorValidationService
    {
        public void ValidateLineCount(string lineCount)
        {
            if (!Regex.IsMatch(lineCount, "[0-9]"))
            {
                throw new Exception("Your input is not valid!");
            }
        }

        public void ValidateLineOfOperations(string operations)
        {
            if (!string.IsNullOrEmpty(operations))
            {
                throw new Exception("Your input is not valid!");
            }
        }
    }
}
