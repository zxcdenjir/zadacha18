class Auto
{
    private int Id;
    private string? Number;
    private float Fuel = 0;
    private float MaxFuel;
    private float Rashod;
    private int Probeg = 0;
    private bool isMoving = false;
    private bool isAvaria = false;
    private string Marka;
    public int[] Coordinates;

    public Auto()
    {
        Id = 0;
        Marka = "Автомобиль";
        Number = "A000AA";
        MaxFuel = 50;
        Rashod = 5;
        Coordinates = [0, 0];
        SetOnField();
    }
    public Auto(int id, string marka, string number, float maxFuel, float rashod)
    {
        Id = id;
        Marka = marka;
        Number = number;
        MaxFuel = maxFuel;
        Rashod = rashod;
        Coordinates = GenerateCoordinates();
        SetOnField();
    }

    public override string ToString()
    {
        return $"{Id,2} | {Marka,10} | {Number,6} | {Ostatok(),5:F2} л | {MaxFuel,8} л | {Rashod,14} л | {$"[{Coordinates[0]}; {Coordinates[1]}]",10}";
    }

    private static int[] GenerateCoordinates()
    {
        Random random = new();
        int[] coordinates = new int[2];
        do
        {
            coordinates[0] = random.Next(0, Program.Field.GetLength(0));
            coordinates[1] = random.Next(0, Program.Field.GetLength(1));
        } while (Program.Field[coordinates[0], coordinates[1]] != "·");
        return coordinates;
    }
    public void SetOnField()
    {
        Program.Field[Coordinates[0], Coordinates[1]] = Id.ToString();
    }
    public void Out()
    {
        Console.Write("Id: " + Id);
        Console.Write(" | Марка: " + Marka);
        Console.Write(" | Номер: " + Number);
        Console.Write(" | Топливо: " + $"{Ostatok():F2}" + " л");
        Console.Write(" | Объём бака: " + MaxFuel + " л");
        Console.Write(" | Расход на 100 км: " + Rashod + " л");
        Console.WriteLine($" | Координаты: [{Coordinates[0]}; {Coordinates[1]}]");
    }
    public void Zapravka(float top)
    {
        if (top > MaxFuel)
        {
            top -= top - MaxFuel;
        }
        Console.WriteLine();
        Fuel += top;
        Console.WriteLine($"Автомобиль заправлен на {top} л\nТекущее количества топлива: {Fuel} л ");
    }
    public void Move(int km, bool avaria)
    {
        Random random = new();
        Console.WriteLine();
        if (Calculate(km))
        {
            if (avaria)
            {
                int avariaKm = km;
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
                    float fuelForZapravka = Program.FloatInput("Введите количество топлива для заправки: ", 0, GetMaxFuel());
                    Zapravka(fuelForZapravka);
                    if (!Calculate(km))
                        Console.WriteLine("Всё ещё мало топлива, нужно дозаправить.");
                } while (!Calculate(km));
                Razgon();
                Move(km, false);
            }
        }
    }
    private float Ostatok()
    {
        return Fuel;
    }
    public float GetMaxFuel()
    {
        return MaxFuel;
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
            isMoving = false;
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