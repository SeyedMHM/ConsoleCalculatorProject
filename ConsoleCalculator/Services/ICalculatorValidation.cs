namespace ConsoleCalculator.Services
{
    public interface ICalculatorValidationService
    {
        public void ValidateLineCount(string lineCount);

        public void ValidateLineOfOperations(string operations);
    }
}
