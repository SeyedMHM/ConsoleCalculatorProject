using System.Data;
using System.Text.RegularExpressions;

namespace ConsoleCalculator.Services
{
    public class CalculatorService : ICalculatorService
    {
        private readonly ICalculatorValidationService _calculatorValidationService;

        public CalculatorService(ICalculatorValidationService calculatorValidationService)
        {
            _calculatorValidationService = calculatorValidationService;
        }

        public int GetLineCount(string inputLineCount)
        {
            _calculatorValidationService.ValidateLineCount(inputLineCount);
            return Convert.ToInt32(inputLineCount);
        }
        
        public int CalculateInput(string operations)
        {
            _calculatorValidationService.ValidateLineOfOperations(operations);

            operations = ReplacePlusOperandWithComma(operations);

            return CalculateStringOperation(operations);
        }
        
        private string ReplacePlusOperandWithComma(string operations)
        {
            operations = RemoveAdditionalComma(operations);

            var partOfOperators = operations.Split(",");

            if (operations.Split(",").Length > 1 && !(partOfOperators[1].StartsWith("-") || partOfOperators[1].StartsWith("+")))
            {
                partOfOperators[1] = "+" + partOfOperators[1];
            }

            return string.Join("", partOfOperators);
        }

        private string RemoveAdditionalComma(string operations)
        {
            return Regex.Replace(operations, @",{1,}", ",").Trim(',');
        }

        private int CalculateStringOperation(string operations)
        {
            try
            {
                return (int)new DataTable().Compute(operations, null);
            }
            catch (SyntaxErrorException)
            {
                throw new SyntaxErrorException("Your input is not valid!");
            }
        }
    }
}
