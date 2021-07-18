using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RainbowSchoolProject
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome To Rainbow School Teacher System");

            
            ConsoleColorMessage(ConsoleColor.Yellow, "Select option:");
            ConsoleColorMessage(ConsoleColor.Yellow, "Press 1 to display all teachers.");
            ConsoleColorMessage(ConsoleColor.Yellow, "Press 2 to add new teacher.");
            ConsoleColorMessage(ConsoleColor.Yellow, "Press 3 to update a teacher data.");
            ConsoleColorMessage(ConsoleColor.Yellow, "Press x to exit.");

            string choise = Console.ReadLine();

            var teachersData = new TeachersData();

            while (true)
            {
                switch (choise)
                {
                    case "1":
                        ConsoleColorMessage(ConsoleColor.Magenta, "-----> All Teachers <-----");
                        teachersData.DisplayAllTeachers();
                        break;
                    case "2":
                        ConsoleColorMessage(ConsoleColor.Magenta, "-----> Adding New Teacher <-----");
                        teachersData.AddnewTeacher();
                        break;
                    case "3":
                        ConsoleColorMessage(ConsoleColor.Magenta, "-----> Updating Teacher's data <-----");
                        teachersData.DisplayAllTeachers();
                        teachersData.UpdateTeacher();
                        break;
                    case "x":
                        ConsoleColorMessage(ConsoleColor.Magenta, "Bye");
                        return;
                    default:
                        break;
                }
                ConsoleColorMessage(ConsoleColor.Green, "============================");
                ConsoleColorMessage(ConsoleColor.Green, "Select another option:");
                ConsoleColorMessage(ConsoleColor.Green, "1 to display all teachers.");
                ConsoleColorMessage(ConsoleColor.Green, "2 to add new teacher.");
                ConsoleColorMessage(ConsoleColor.Green, "3 to update a teacher data.");
                ConsoleColorMessage(ConsoleColor.Green, "x to exit.");

                choise = Console.ReadLine();
            }
            
        }

        //Print colored message to the console
        static void ConsoleColorMessage(ConsoleColor color, string message)
        {
            Console.ForegroundColor = color;

            Console.WriteLine(message);

            Console.ResetColor();
        }
    }
}
