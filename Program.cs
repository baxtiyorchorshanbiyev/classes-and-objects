internal class Program
{
    private static void Main()
    {

        // -- 1

        TrafficLight light1 = new TrafficLight();
        for (int i = 0; i < 4; i++)
        {
            light1.NextColor();
            Console.WriteLine($"Birinchi svetofor {i + 1}: {light1.GetColor()}");
        }

        // -- 2

        Wallet wallet1 = new Wallet();
        wallet1.AddMoney(50000);
        Console.WriteLine($"Birinchi hamyon balansi: {wallet1.GetBalance()}");
        wallet1.SpendMoney(12000);
        Console.WriteLine($"Birinchi hamyon balansi: {wallet1.GetBalance()}");
        wallet1.SpendMoney(40000);
        Console.WriteLine($"Birinchi hamyon balansi: {wallet1.GetBalance()}");

        // -- 3
        WorkoutLog log1 = new WorkoutLog();
        log1.AddWorkout(30);
        log1.AddWorkout(45);
        log1.AddWorkout(0);
        log1.AddWorkout(25);
        log1.AddWorkout(60);
        log1.AddWorkout(20);
        Console.Write("Kiritishlar ");

        foreach (var duration in log1.Durations)
        {
            Console.Write($"{duration}: {log1.isAdded}, ");
        }
        Console.WriteLine();
        Console.WriteLine($"Jami vaqt: {log1.GetTotalMinutes()}");
        Console.WriteLine($"O'rtacha vaqt: {log1.GetAverageMinutes()}");
        Console.WriteLine($"Eng uzun trening: {log1.GetLongestWorkout()}");


        // -- 4
        Song song1 = new Song
        {
            Title = "Song 1",
            DurationSeconds = 180
        };
        song1.Play();
        song1.Play();
        song1.GetInfo();
        Playlist playlist1 = new Playlist();
        playlist1.AddSong(song1);
        Console.WriteLine($"Umumiy davomiylik: {playlist1.GetTotalDuration()} soniya");

        // -- 5

        Product product1 = new Product
        {
            Code = "P001",
            Name = "Notebook",
            Price = 12000,
            Quantity = 10
        };
        Product product2 = new Product
        {
            Code = "P002",
            Name = "Pen",
            Price = 3000,
            Quantity = 20
        };
        Product product3 = new Product
        {
            Code = "P003",
            Name = "Bag",
            Price = 85000,
            Quantity = 2
        };

        Warehouse warehouse1 = new Warehouse();
        warehouse1.AddProduct(product1);
        warehouse1.AddProduct(product2);
        warehouse1.AddProduct(product3);

        bool isSold1 = warehouse1.TrySellProduct("P001", 3);
        Console.WriteLine($"{isSold1}, Qoldiq: {product1.Quantity}");

        bool isSold2 = warehouse1.TrySellProduct("P003", 3);
        Console.WriteLine($"{isSold2}, Qoldiq: {product3.Quantity}");

        bool isSold = warehouse1.TrySellProduct("P99", 1);
        Console.WriteLine($"{isSold}");

        Console.WriteLine($"Umumiy ombor qiymati: {warehouse1.GetTotalStockValue()}");
        warehouse1.PrintLowStock(2);


    }

    class TrafficLight
    {
        public string Color = "Red";

        public void NextColor()
        {
            if (Color == "Red")
            {
                Color = "Green";
            }
            else if (Color == "Green")
            {
                Color = "Yellow";
            }
            else if (Color == "Yellow")
            {
                Color = "Red";
            }
        }

        public string GetColor()
        {
            return Color;
        }
    }

    class Wallet
    {
        public int Balance;

        public void AddMoney(int amount)
        {
            if (amount < 0)
            {
                return;
            }
            Balance += amount;
        }

        public bool SpendMoney(int amount)
        {
            if (amount < 0 || amount > Balance)
            {
                return false;
            }
            Balance -= amount;
            return true;
        }

        public int GetBalance()
        {
            return Balance;
        }
    }

    class WorkoutLog
    {
        public int[] Durations = new int[5];
        public int Count = 0;
        public bool isAdded = false;

        public void AddWorkout(int duration)
        {
            if (Count < Durations.Length && duration > 0)
            {
                Durations[Count] = duration;
                Count++;
                isAdded = true;
            }
        }

        public int GetTotalMinutes()
        {
            int total = 0;
            for (int i = 0; i < Count; i++)
            {
                total += Durations[i];
            }
            return total;
        }
        public int GetAverageMinutes()
        {
            if (Count == 0)
            {
                return 0;
            }
            return GetTotalMinutes() / Count;
        }

        public int GetLongestWorkout()
        {
            if (Count == 0)
            {
                return 0;
            }
            int longest = Durations[0];
            for (int i = 1; i < Count; i++)
            {
                if (Durations[i] > longest)
                {
                    longest = Durations[i];
                }
            }
            return longest;
        }

    }

    class Song
    {
        public string Title = "";
        public int DurationSeconds;
        public int PlayCount;

        public void Play()
        {
            PlayCount++;
        }

        public void GetInfo()
        {
            Console.WriteLine($"Title: {Title}, {DurationSeconds} soniya, {PlayCount} marta ijro etildi");
        }
    }

    class Playlist
    {
        public Song[] Songs = new Song[5];
        public int Count = 0;

        public void AddSong(Song song)
        {
            if (Count < Songs.Length)
            {
                Songs[Count] = song;
                Count++;
            }
        }

        public bool PlaySong(int index)
        {
            if (index >= 0 && index < Count)
            {
                Songs[index].Play();
                return true;
            }
            return false;
        }
        public int GetTotalDuration()
        {
            int totalDuration = 0;
            for (int i = 0; i < Count; i++)
            {
                totalDuration += Songs[i].DurationSeconds;
            }
            return totalDuration;
        }
    }

    class Product
    {
        public string Code = "";
        public string Name = "";
        public decimal Price;
        public int Quantity;

        public bool TrySell(int quantity)
        {
            if (quantity <= 0 || quantity > Quantity)
            {
                return false;
            }
            Quantity -= quantity;
            return true;
        }

        public int GetStockValue()
        {
            return Quantity * (int)Price;
        }
    }

    class Warehouse
    {
        public Product[] Products = new Product[5];
        public int Count = 0;

        public bool AddProduct(Product product)
        {
            if (Count < Products.Length)
            {
                Products[Count] = product;
                Count++;
                return true;
            }
            return false;
        }

        public bool TrySellProduct(string code, int amount)
        {
            for (int i = 0; i < Count; i++)
            {
                if (Products[i].Code == code)
                {
                    return Products[i].TrySell(amount);
                }
            }
            return false;
        }

        public int GetTotalStockValue()
        {
            int totalValue = 0;
            for (int i = 0; i < Count; i++)
            {
                totalValue += Products[i].GetStockValue();
            }
            return totalValue;
        }

        public void PrintLowStock(int threshold)
        {
            for (int i = 0; i < Count; i++)
            {
                if (Products[i].Quantity <= threshold)
                {
                    Console.WriteLine($"{Products[i].Name} (Code: {Products[i].Code}, Quantity: {Products[i].Quantity})");
                }
            }
        }
    }
}
