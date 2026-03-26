using System;
using System.Collections.Generic;

class Transporter
{
    // Существующие методы MinElementMethod, SeveroZapad, PrintPlan остаются без изменений
    public static (int[,], int) MinElementMethod(int[] supply, int[] demand, int[,] cost)
    {
        int rows = supply.Length;
        int cols = demand.Length;
        int[,] plan = new int[rows, cols];
        int[] a = (int[])supply.Clone();
        int[] b = (int[])demand.Clone();
        int totalcost = 0;

        while (true)
        {
            int mincost = int.MaxValue;
            int minI = -1, minJ = -1;
            for (int i = 0; i < rows; i++)
            {
                if (a[i] == 0) continue;
                for (int j = 0; j < cols; j++)
                {
                    if (b[j] == 0) continue;
                    if (cost[i, j] < mincost)
                    {
                        mincost = cost[i, j];
                        minI = i;
                        minJ = j;
                    }
                }
            }

            if (minI == -1) break;
            int amount = Math.Min(a[minI], b[minJ]);
            plan[minI, minJ] = amount;
            totalcost += amount * cost[minI, minJ];
            a[minI] -= amount;
            b[minJ] -= amount;
        }

        return (plan, totalcost);
    }

    public static (int[,], int) SeveroZapad(int[] supply, int[] demand, int[,] cost)
    {
        int rows = supply.Length;
        int cols = demand.Length;
        int[,] plan = new int[rows, cols];
        int[] a = (int[])supply.Clone();
        int[] b = (int[])demand.Clone();
        int totalcost = 0;
        int i = 0, j = 0;

        while (i < rows && j < cols)
        {
            int amount = Math.Min(a[i], b[j]);
            plan[i, j] = amount;
            totalcost += amount * cost[i, j];
            a[i] -= amount;
            b[j] -= amount;
            if (a[i] == 0) i++;
            if (b[j] == 0) j++;
        }

        return (plan, totalcost);
    }

    public static void PrintPlan(int[,] plan, int totalcost)
    {
        Console.WriteLine("Перевозки:");
        for (int i = 0; i < plan.GetLength(0); i++)
        {
            for (int j = 0; j < plan.GetLength(1); j++)
            {
                Console.Write($"{plan[i, j],5}");
            }

            Console.WriteLine();
        }

        Console.WriteLine($"Суммарная стоимость: {totalcost}");
    }

    // НОВЫЙ МЕТОД: Метод потенциалов для проверки оптимальности
    public static void PotentialMethod(int[] supply, int[] demand, int[,] cost, int[,] initialPlan)
    {
        int rows = supply.Length;
        int cols = demand.Length;
        int[,] currentPlan = (int[,])initialPlan.Clone();
        int iteration = 1;

        while (true)
        {
            Console.WriteLine($"\n=== Итерация {iteration} ===");

            //подсчет потенциалов
            double[] u = new double[rows];
            double[] v = new double[cols];
            bool[] uCalculated = new bool[rows];
            bool[] vCalculated = new bool[cols];

            
            u[0] = 0;
            uCalculated[0] = true;

            bool changed;
            do
            {
                changed = false;

                //потенциалы занятых
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        if (currentPlan[i, j] > 0)
                        {
                            if (uCalculated[i] && !vCalculated[j])
                            {
                                v[j] = cost[i, j] - u[i];
                                vCalculated[j] = true;
                                changed = true;
                            }
                            else if (!uCalculated[i] && vCalculated[j])
                            {
                                u[i] = cost[i, j] - v[j];
                                uCalculated[i] = true;
                                changed = true;
                            }
                        }
                    }
                }
            } while (changed);

            //принт
            Console.WriteLine("\nПотенциалы:");
            Console.Write("u: ");
            for (int i = 0; i < rows; i++)
            {
                Console.Write($"{u[i],8:F2} ");
            }

            Console.WriteLine();
            Console.Write("v: ");
            for (int j = 0; j < cols; j++)
            {
                Console.Write($"{v[j],8:F2} ");
            }

            Console.WriteLine();

            //свободные яч
            Console.WriteLine("\nОценки свободных ячеек:");
            List<(int i, int j, double delta)> negativeDeltas = new List<(int, int, double)>();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (currentPlan[i, j] == 0)
                    {
                        double delta = cost[i, j] - (u[i] + v[j]);
                        Console.Write($"{delta,8:F2} ");

                        if (delta < 0)
                        {
                            negativeDeltas.Add((i, j, delta));
                        }
                    }
                    else
                    {
                        Console.Write($"{"занят",8} ");
                    }
                }

                Console.WriteLine();
            }

            //проверка
            if (negativeDeltas.Count == 0)
            {
                Console.WriteLine("\n✓ План оптимален!");
                break;
            }
            else
            {
                Console.WriteLine($"\n✗ План неоптимален. Найдено {negativeDeltas.Count} отрицательных оценок.");

                
                Console.WriteLine("Ячейки с отрицательными оценками:");
                foreach (var neg in negativeDeltas)
                {
                    Console.WriteLine($"  Ячейка [{neg.i + 1},{neg.j + 1}]: Δ = {neg.delta:F2}");
                }
                
                Console.WriteLine("\nВведите улучшенный опорный план (матрица перевозок):");
                int[,] newPlan = new int[rows, cols];

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        Console.Write($"Ячейка [{i + 1},{j + 1}]: ");
                        newPlan[i, j] = int.Parse(Console.ReadLine());
                    }
                }

                currentPlan = newPlan;
                iteration++;
            }
        }
    }
}
