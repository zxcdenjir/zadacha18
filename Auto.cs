class Auto
{
    private Random random = new();

    private string? Number;
    private float Fuel;
    private float Rashod;
    private int Probeg;
    private bool isMoving = false;
    private bool isAvaria = false;

    public void Info(string nom, float bak, float ras)
    {
        Number = nom;
        Fuel = bak;
        Rashod = ras;
        Probeg = 0;
    }
    public void Out()
    {
        Console.Write("Номер автомобиля: " + Number);
        Console.Write(" | Остаток топлива: " + $"{Ostatok():F2}" + " л");
        Console.WriteLine(" | Расход топлива на 100 км: " + Rashod + " л");
    }
    public void Zapravka(float top)
    {
        Console.WriteLine();
        Fuel += top;
        Console.WriteLine($"Автомобиль заправлен на {top} литров\nТекущее количества топлива: {Ostatok():F2} л ");
    }
    public void Move(int km)
    {
        Console.WriteLine();
        if (Calculate(km))
        {
            double avariaChance = random.NextDouble();
            if (avariaChance <= 0.10)
            {
                int avariaKm = random.Next(1, km);
                float requiredFuel = Rashod / 100 * avariaKm;
                Fuel -= requiredFuel;
                Probeg += avariaKm;
                Avariya(avariaKm);
                return;
            }
            else
            {
                float requiredFuel = Rashod / 100 * km;
                Fuel -= requiredFuel;
                Probeg += km;
                Console.WriteLine($"Машина проехала {km} км. Остаток топлива: {Fuel:F2} л\n");
            }
        }
        else
        {
            Console.WriteLine("Недостаточно топлива для поездки.");
            Tormoz();
            Console.Write("Хотите заправить автомобиль? (да/нет): ");
            string answer = Console.ReadLine();
            Console.WriteLine();
            if (answer.ToLower() == "да" || answer == "")
            {
                Console.WriteLine("Сколько литров хотите заправить? ");
                do
                {
                    float fuelForZapravka = Program.FloatInput("Введите количество топлива для заправки: ", 0);
                    Zapravka(fuelForZapravka);
                    if (!Calculate(km))
                        Console.WriteLine("Всё ещё мало топлива, нужно дозаправить.");
                } while (!Calculate(km));
                Razgon();
                Move(km);
            }
        }
    }
    private float Ostatok()
    {
        return Fuel;
    }
    public bool Calculate(int distance)
    {
        float tmp_fuel = Fuel;
        while (distance > 0)
        {
            float requiredFuel = Rashod / 100;
            if (tmp_fuel >= requiredFuel)
            {
                tmp_fuel = float.Round(tmp_fuel - requiredFuel, 2);
                distance--;
            }
            else
            {
                return false;
            }
        }
        return true;
    }
    public void Razgon()
    {
        if (!isMoving)
        {
            isMoving = true;
            Console.WriteLine();
            Console.WriteLine("Автомобиль разгоняется...");
        }
    }
    public void Tormoz()
    {
        if (isMoving)
        {
            isMoving = true;
            Console.WriteLine("Автомобиль тормозит...");
            Console.WriteLine();
        }
    }
    public int GetProbeg()
    {
        return Probeg;
    }
    public void Avariya(int km)
    {
        Console.WriteLine($"Автомобиль попал в аварию на {km} км и не может продолжать движение.");
        isAvaria = true;
    }
    public bool AvariyaStatus() { return isAvaria; }

}