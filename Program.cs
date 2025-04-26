using System;

class Program
{
    static void Main()
    {
        

        Auto[] autos = [
            new Auto(),
            new Auto(),
            new Auto()
        ];
        autos[0].Info("A123BC", 0, 5);
        autos[1].Info("B456CD", 0, 7);
        autos[2].Info("C789EF", 0, 10);

        PrintAllCars(autos);

        Auto currentCar = autos[IntInput("Выберите автомобиль: ", 1, autos.Length) - 1];

        Console.WriteLine();
        currentCar.Out();

        float fuelAmount = FloatInput("Введите количество топлива для заправки: ", 0);
        currentCar.Zapravka(fuelAmount);
        
        Drive(currentCar);

    }

    static void PrintAllCars(Auto[] autos)
    {
        for (int i = 0; i < autos.Length; i++)
        {
            Console.Write($"Автомобиль {i + 1}: ");
            autos[i].Out();
        }
        Console.WriteLine();
    }

    static void Drive(Auto car)
    {
        Random random = new();

        bool continueDriving = true;
        int distance = random.Next(5, 100);
        if (car.Calculate(distance))
            car.Razgon();
        do
        {
            Console.WriteLine($"Случайная дистанция: {distance} км");
            car.Move(distance);

            if (car.AvariyaStatus())
                break;

            Console.Write("Продолжить движение? (да/нет): ");
            string answer = Console.ReadLine();
            if (!(answer.ToLower() == "да" || answer == ""))
                continueDriving = false;
            distance = random.Next(5, 100);
            Console.WriteLine();
        } while (continueDriving);
        car.Tormoz();
        Console.WriteLine($"Было пройдено {car.GetProbeg()} км");
    }
    
    public static float FloatInput(string text, float minValue)
    {
        float value;
        bool isCorrect;
        do
        {
            Console.Write(text);
            isCorrect = float.TryParse(Console.ReadLine(), out value);
            if (!isCorrect)
                Console.WriteLine("Неверный ввод данных");
            else if (value <= minValue)
                Console.WriteLine($"Значение должно быть больше {minValue}");
        } while (!isCorrect || value <= minValue);
        return value;
    }

    static int IntInput(string text, int minValue, int maxValue)
    {
        int value;
        bool isCorrect;
        do
        {
            Console.Write(text);
            isCorrect = int.TryParse(Console.ReadLine(), out value);
            if (!isCorrect)
                Console.WriteLine("Неверный ввод данных");
            else if (value < minValue || value > maxValue)
                Console.WriteLine($"Некорректный ввод. Пожалуйста, введите число от {minValue} до {maxValue}");
        } while (!isCorrect || value < minValue || value > maxValue);
        return value;
    }
}