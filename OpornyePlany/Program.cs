using System;


class Transporter
{
    public static (int[,], int) MinElementMethod(int[] supply, int[] demand, int[,] cost)
    {
        int rows = supply.Length;
        int cols = demand.Length;
        int [,]plan = new int[rows, cols];
        int[] a = (int[])supply.Clone();
        int[] b = (int[])demand.Clone();
        int totalcost = 0;
        while (true)
        {
            int mincost =  int.MaxValue;
            int minI = -1, minJ = -1;
            for (int i = 0; i < rows; i++)
            {
                if (a[i] == 0 ) continue;
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
        int [,]plan = new int[rows, cols];
        int[] a = (int[])supply.Clone();
        int[] b = (int[])demand.Clone();
        int totalcost = 0;
        int i = 0, j = 0;
        while (i < rows && j < cols)
        {
            int amount  = Math.Min(a[i], b[j]);
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
        Console.WriteLine("Перевозки");
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
}











