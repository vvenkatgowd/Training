using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Student
    {
        static void Main(string[] args)
        {
            int stu_size;
            Console.WriteLine("Enter Number of Students");
            stu_size = int.Parse(Console.ReadLine());
            String[] StuName = new String[stu_size];
            int[,] StuMarks = new int[stu_size, 5];
            int j = 0;
            int sum = 0;

            for (int i = 0; i < stu_size; i++)
            {
                Console.WriteLine("Enter Student Name:");
                StuName[i] = Console.ReadLine();
                Console.WriteLine("Enter 5 Marks of Student");
                for (int k = 0; k < 5; k++)
                {
                    StuMarks[j, k] = int.Parse(Console.ReadLine());


                }

                j++;
            }
            Console.WriteLine();
            Console.WriteLine("Name" + '\t' + "mark1" + '\t' + "mark2" + '\t' + "mark3" + '\t' + "mark4" + '\t' + "mark5" + '\t' + "total" + '\t' + "avg");
            for (int i = 0; i < stu_size; i++)
            {
                sum = 0;
                Console.Write(StuName[i] + "\t");

                for (int k = 0; k < 5; k++)
                {
                    Console.Write(StuMarks[i, k] + "\t");
                    sum = sum + StuMarks[i, k];
                }
                Console.Write(sum + "\t");
                Console.Write((sum / 5) + "\t");
                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }
}