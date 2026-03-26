using System;

class Program
{
    static void Main()
    {
        // ввод кол-ва потребителей и поставщиков
        Console.Write("Введите количество поставщиков: ");
        int rows = int.Parse(Console.ReadLine());

        Console.Write("Введите количество потребителей: ");
        int cols = int.Parse(Console.ReadLine());

        // ввод запаса
        int[] supply = new int[rows];
        Console.WriteLine("\nВведите запасы поставщиков");
        for (int i = 0; i < rows; i++)
        {
            Console.Write($"Поставщик {i + 1}: ");
            supply[i] = int.Parse(Console.ReadLine());
        }

        // ввод потребности
        int[] demand = new int[cols];
        Console.WriteLine("\nВведите потребности потребителей");
        for (int j = 0; j < cols; j++)
        {
            Console.Write($"Потребитель {j + 1}: ");
            demand[j] = int.Parse(Console.ReadLine());
        }



        // ввод тарифа
        int[,] cost = new int[rows, cols];
        Console.WriteLine("\n Введите тарифы (стоимость перевозки)");
        for (int i = 0; i < rows; i++)
        {
            Console.WriteLine($"\nПоставщик {i + 1}:");
            for (int j = 0; j < cols; j++)
            {
                Console.Write($"  Стоимость доставки потребителю {j + 1}: ");
                cost[i, j] = int.Parse(Console.ReadLine());
            }
        }

        // вывод
        Console.WriteLine("\nвведенные данные");
        Console.WriteLine("Запасы поставщиков: " + string.Join(", ", supply));
        Console.WriteLine("Потребности потребителей: " + string.Join(", ", demand));
        Console.WriteLine("\nМатрица тарифов:");
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write($"{cost[i, j],5}");
            }

            Console.WriteLine();
        }

        // выбор
        Console.WriteLine("1 - Метод минимального элемента");
        Console.WriteLine("2 - Метод северо-западного угла");
        Console.Write("ваш выбор: ");

        int choice = int.Parse(Console.ReadLine());

        (int[,] plan, int totalCost) result;

        if (choice == 1)
        {
            Console.WriteLine("\nметод минимального элемента");
            result = Transporter.MinElementMethod(supply, demand, cost);
        }
        else if (choice == 2)
        {
            Console.WriteLine("\nметод северо западного угла");
            result = Transporter.SeveroZapad(supply, demand, cost);
        }

        else
        {
            Console.WriteLine("Неверный выбор!");
            return;
        }


        Transporter.PrintPlan(result.plan, result.totalCost);


    }
}


 


