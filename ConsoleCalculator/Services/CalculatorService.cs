using System.Data;
using System.Text.RegularExpressions;

namespace ConsoleCalculator.Services
{
    public class CalculatorService : ICalculatorService
    {
        private readonly ICalculatorValidation _calculatorValidation;

        public CalculatorService(ICalculatorValidation calculatorValidation)
        {
            _calculatorValidation = calculatorValidation;
        }

        public int GetLineCount(string inputLineCount)
        {
            _calculatorValidation.ValidateLineCount(inputLineCount);
            return Convert.ToInt32(inputLineCount);
        }
        
        public int CalculateInput(string operations)
        {
            _calculatorValidation.ValidateLineOfOperations(operations);

            operations = ReplacePlusOperandWithComma(operations);

            return CalculateStringOperation(operations);
        }
        
        private string ReplacePlusOperandWithComma(string operations)
        {
            var partOfOperators = operations.Split(",");

            if (operations.Split(",").Length > 1 && !(partOfOperators[1].StartsWith("-") || partOfOperators[1].StartsWith("+")))
            {
                partOfOperators[1] = "+" + partOfOperators[1];
            }

            return string.Join("", partOfOperators);
        }

        private int CalculateStringOperation(string operations)
        {
            try
            {
                return (int)new DataTable().Compute(operations, null);
            }
            catch (Exception)
            {
                throw new Exception("Your input is not valid!");
            }
        }
    }
}
