using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string userInput = Console.ReadLine();
            double userOutput = Calculate(userInput);

            Console.WriteLine(userOutput);
            Console.ReadLine();
        }

        public static double Calculate(string userInput)
        {
            double sum = double.Parse(userInput.Split(' ')[0]);
            double rate = double.Parse(userInput.Split(' ')[1]) / 100;
            int t = int.Parse(userInput.Split(' ')[2]);

            for (int i = 1; i <= t; i++)
            {
                sum += sum * rate / 12;
            }
            return sum;
        }
    }
}
