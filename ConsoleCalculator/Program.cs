using ConsoleCalculator.Services;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = GetServiceProvider().BuildServiceProvider();

ICalculatorService _calculatorService = serviceProvider.GetService<ICalculatorService>();


Console.Write("Please enter the number of operation lines: ");

int lineCount = _calculatorService.GetLineCount(Console.ReadLine());

int sum = 0;

for (int i = 0; i < lineCount; i++)
{
    Console.Write($"Please enter {i + 1} of line operations: ");

    sum += _calculatorService.CalculateInput(Console.ReadLine());
}

Console.WriteLine($"Sum of you inputs: {sum}");
Console.ReadLine();


IServiceCollection GetServiceProvider()
{
    //Register services to DI-Container
    var serviceProvider = new ServiceCollection()
        .AddScoped<ICalculatorService, CalculatorService>()

        //In This Project, this service can be Singletone
        .AddSingleton<ICalculatorValidationService, CalculatorValidationService>() 
        ;

    return serviceProvider;
}