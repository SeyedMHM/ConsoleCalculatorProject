namespace ConsoleCalculator.Services
{
    public interface ICalculatorService
    {
        int GetLineCount(string inputLineCount);
        int CalculateInput(string operations);
    }
}
