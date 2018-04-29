using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Slaw is sharp with C#");
            int number = 100;
            Console.WriteLine(number);
            Console.WriteLine("Enter number: ", number);
            number = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(number);

            int[] tabx = new int [3];
            tabx[0] = 12;
            tabx[1] = 11;
            tabx[2] = 1500;
            for (int i = 0; i < 3; i++)
                Console.WriteLine(tabx[i]);
            
            Console.ReadKey();
        }
    }
}
