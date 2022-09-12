using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalculator.Services
{
    public interface ICalculatorService
    {
        int GetLineCount(string inputLineCount);
        int CalculateInput(string operations);
    }
}
