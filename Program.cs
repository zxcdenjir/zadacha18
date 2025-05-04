class Program
{
    public static string[,] Field = new string[25, 25];

    static void Main()
    {
        Console.SetWindowSize(125, 43);

        for (int i = 0; i < Field.GetLength(0); i++)
        {
            for (int j = 0; j < Field.GetLength(1); j++)
            {
                Field[i, j] = "·";
            }
        }

        List<Auto> autos = [
            new(),
            new(1, "BMW", "A123BC", 30, 8),
            new(2, "Toyota", "X777XX", 40, 6),
            new(3, "Audi", "B456CD", 50, 7),
            new(4, "Mercedes", "C789EF", 60, 9)
        ];

        PrintField();

        while (true)
        {
            PrintAllCars(autos);

            Console.WriteLine("1. Выбрать автомобиль");
            Console.WriteLine("2. Вывести поле");
            Console.WriteLine("3. Выход");

            switch (IntInput("Выберите действие: ", 1, 3))
            {
                case 1:
                    Auto currentCar = autos[IntInput("Выберите автомобиль: ", 0, autos.Count - 1)];
                    if (currentCar.AvariyaStatus())
                    {
                        Console.WriteLine("Автомобиль в аварии. Невозможно продолжить движение\n");
                        break;
                    }
                    Console.WriteLine();
                    currentCar.Out();

                    float fuelAmount = FloatInput("Введите количество топлива для заправки: ", 0);
                    currentCar.Zapravka(fuelAmount);

                    Drive(currentCar);

                    break;

                case 2:
                    Console.Clear();
                    PrintField();
                    break;

                case 3:
                    return;
            }
        }
    }

    static void Drive(Auto car)
    {
        while (true)
        {
            Console.WriteLine();
            Console.Write("Выберите направление движения (стрелки на клавиатуре): ");
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.UpArrow:
                    Console.WriteLine("\nТекущее направление: вверх\n");

                    int distance = IntInput("Введите расстояние для движения: ", 1);

                    if (car.Coordinates[0] - distance < 0)
                    {
                        distance = car.Coordinates[0];

                        Field[car.Coordinates[0], car.Coordinates[1]] = "·";
                        car.Coordinates[0] = 0;
                        while (Field[car.Coordinates[0], car.Coordinates[1]] != "·") 
                            car.Coordinates[0] += 1;
                        car.SetOnField();

                        Console.Clear();
                        PrintField();

                        if (car.Calculate(distance))
                            car.Razgon();

                        car.Move(distance, true);
                    }
                    else
                    {
                        bool isAvariya = false;

                        Field[car.Coordinates[0], car.Coordinates[1]] = "·";
                        car.Coordinates[0] -= distance;
                        while (Field[car.Coordinates[0], car.Coordinates[1]] != "·")
                        {
                            car.Coordinates[0] += 1;
                            distance -= 1;
                            isAvariya = true;
                        }
                        car.SetOnField();

                        Console.Clear();
                        PrintField();

                        if (car.Calculate(distance))
                            car.Razgon();

                        car.Move(distance, isAvariya);
                    }
                    car.Tormoz();
                    break;

                case ConsoleKey.DownArrow:
                    Console.WriteLine("\nТекущее направление: вниз\n");

                    distance = IntInput("Введите расстояние для движения: ", 1);

                    if (car.Coordinates[0] + distance >= Field.GetLength(0))
                    {
                        distance = Field.GetLength(0) - 1 - car.Coordinates[0];

                        Field[car.Coordinates[0], car.Coordinates[1]] = "·";
                        car.Coordinates[0] = Field.GetLength(0) - 1;
                        while (Field[car.Coordinates[0], car.Coordinates[1]] != "·")
                            car.Coordinates[0] -= 1;
                        car.SetOnField();

                        Console.Clear();
                        PrintField();

                        if (car.Calculate(distance))
                            car.Razgon();

                        car.Move(distance, true);
                    }
                    else
                    {
                        bool isAvariya = false;

                        Field[car.Coordinates[0], car.Coordinates[1]] = "·";
                        car.Coordinates[0] += distance;
                        while (Field[car.Coordinates[0], car.Coordinates[1]] != "·")
                        {
                            car.Coordinates[0] -= 1;
                            distance -= 1;
                            isAvariya = true;
                        }
                        car.SetOnField();

                        Console.Clear();
                        PrintField();

                        if (car.Calculate(distance))
                            car.Razgon();

                        car.Move(distance, isAvariya);
                    }
                    car.Tormoz();
                    break;

                case ConsoleKey.LeftArrow:
                    Console.WriteLine("\nТекущее направление: влево\n");

                    distance = IntInput("Введите расстояние для движения: ", 1);

                    if (car.Coordinates[1] - distance < 0)
                    {
                        distance = car.Coordinates[1];

                        Field[car.Coordinates[0], car.Coordinates[1]] = "·";
                        car.Coordinates[1] = 0;
                        while (Field[car.Coordinates[0], car.Coordinates[1]] != "·")
                            car.Coordinates[1] += 1;
                        car.SetOnField();

                        Console.Clear();
                        PrintField();

                        if (car.Calculate(distance))
                            car.Razgon();

                        car.Move(distance, true);
                    }
                    else
                    {
                        bool isAvariya = false;

                        Field[car.Coordinates[0], car.Coordinates[1]] = "·";
                        car.Coordinates[1] -= distance;
                        while (Field[car.Coordinates[0], car.Coordinates[1]] != "·")
                        {
                            car.Coordinates[1] += 1;
                            distance -= 1;
                            isAvariya = true;
                        }
                        car.SetOnField();

                        Console.Clear();
                        PrintField();

                        if (car.Calculate(distance))
                            car.Razgon();

                        car.Move(distance, isAvariya);
                    }
                    car.Tormoz();
                    break;

                case ConsoleKey.RightArrow:
                    Console.WriteLine("\nТекущее направление: вправо\n");

                    distance = IntInput("Введите расстояние для движения: ", 1);

                    if (car.Coordinates[1] + distance >= Field.GetLength(1))
                    {
                        distance = Field.GetLength(1) - 1 - car.Coordinates[1];

                        Field[car.Coordinates[0], car.Coordinates[1]] = "·";
                        car.Coordinates[1] = Field.GetLength(1) - 1;
                        while (Field[car.Coordinates[0], car.Coordinates[1]] != "·")
                            car.Coordinates[1] -= 1;
                        car.SetOnField();

                        Console.Clear();
                        PrintField();

                        if (car.Calculate(distance))
                            car.Razgon();

                        car.Move(distance, true);
                    }
                    else
                    {
                        bool isAvariya = false;

                        Field[car.Coordinates[0], car.Coordinates[1]] = "·";
                        car.Coordinates[1] += distance;
                        while (Field[car.Coordinates[0], car.Coordinates[1]] != "·")
                        {
                            car.Coordinates[1] -= 1;
                            distance -= 1;
                            isAvariya = true;
                        }
                        car.SetOnField();

                        Console.Clear();
                        PrintField();

                        if (car.Calculate(distance))
                            car.Razgon();

                        car.Move(distance, isAvariya);
                    }
                    car.Tormoz();
                    break;

                default:
                    Console.WriteLine("\nНеверная клавиша!");
                    continue;
            }

            if (car.AvariyaStatus()) break;

            Console.Write("Продолжить движение? (да/нет): ");
            string answer = Console.ReadLine();
            if (!(answer.ToLower() == "да" || answer == ""))
            {
                break;
            }
        }
        Console.WriteLine($"\nБыло пройдено {car.GetProbeg()} км");
        Console.WriteLine("\nДля продолжения нажмите любую клавишу...");
        Console.ReadKey();
        Console.Clear();

    }

    static void PrintAllCars(List<Auto> autos)
    {
        Console.WriteLine($"{"Id",2} | {"Марка",10} | {"Номер",6} | {"Топливо",7} | {"Объём бака",10} | {"Расход на 100 км",16} | {"Координаты",10}");
        Console.WriteLine("---|------------|--------|---------|------------|------------------|-----------");
        for (int i = 0; i < autos.Count; i++)
        {
            Console.WriteLine(autos[i]);
        }
        Console.WriteLine();
    }

    static void PrintField()
    {
        Console.WriteLine();
        Console.Write("   Y");
        Console.ForegroundColor = ConsoleColor.Cyan;
        for (int i = 0; i < Field.GetLength(1); i++)
        {
            Console.Write($"{i,3}");
        }
        Console.WriteLine();
        //Console.WriteLine("  0  1  2  3  4  5  6  7  8  9 10 11 12 13 14 15 16 17 18 19 20 21 22 23 24");
        Console.ResetColor();
        Console.Write(" X ");
        Console.WriteLine("┌─────────────────────────────────────────────────────────────────────────────┐");
        for (int i = 0; i < Field.GetLength(0); i++)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"{i,2}");
            Console.ResetColor();
            Console.Write(" │");
            for (int j = 0; j < Field.GetLength(1); j++)
            {
                Console.ForegroundColor = Field[i, j] == "·" ? ConsoleColor.Green : ConsoleColor.Red;
                Console.Write($"{Field[i, j],3}");
                Console.ResetColor();
            }
            Console.WriteLine("  │");
        }

        Console.WriteLine("   └─────────────────────────────────────────────────────────────────────────────┘\n");
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
    static int IntInput(string text, int minValue)
    {
        int value;
        bool isCorrect;
        do
        {
            Console.Write(text);
            isCorrect = int.TryParse(Console.ReadLine(), out value);
            if (!isCorrect)
                Console.WriteLine("Неверный ввод данных");
            else if (value < minValue)
                Console.WriteLine($"Некорректный ввод. Пожалуйста, введите число от {minValue}");
        } while (!isCorrect || value < minValue);
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