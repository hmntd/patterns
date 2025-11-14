using patterns_lab_3_1.Interfaces;
using patterns_lab_3_1.Models;

ICalculator fullCalc = new FullCalculator();
ICalculator calculator = new LightCalculator(fullCalc);

while (true)
{
    try
    {
        Console.WriteLine("\nВведіть перше число:");
        double a = double.Parse(Console.ReadLine());

        Console.WriteLine("Введіть друге число:");
        double b = double.Parse(Console.ReadLine());

        Console.WriteLine("\nОберіть дію:");
        Console.WriteLine("  1. Додавання (+)");
        Console.WriteLine("  2. Віднімання (-)");
        Console.WriteLine("  3. Множення (*)");
        Console.WriteLine("  4. Ділення (/)");
        Console.WriteLine("  5. Вихід");
        Console.Write("Ваш вибір (1-5): ");

        string choice = Console.ReadLine();
        double result = 0;

        switch (choice)
        {
            case "1":
                result = calculator.Add(a, b);
                break;
            case "2":
                result = calculator.Subtract(a, b);
                break;
            case "3":
                result = calculator.Multiply(a, b);
                break;
            case "4":
                result = calculator.Divide(a, b);
                break;
            case "5":
                Console.WriteLine("Завершення роботи. До побачення!");
                return;
            default:
                Console.WriteLine("Невірний вибір. Будь ласка, введіть число від 1 до 5.");
                continue;
        }

        Console.WriteLine("-------------------------");
        Console.WriteLine($"Результат: {result}");
        Console.WriteLine("-------------------------");
    }
    catch (FormatException)
    {
        Console.WriteLine("Помилка: Введено невірний числовий формат. Спробуйте ще раз.");
    }
    catch (DivideByZeroException ex)
    {
        Console.WriteLine($"Помилка: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Помилка: {ex.Message}");
    }
}