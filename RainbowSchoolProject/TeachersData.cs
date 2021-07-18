using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RainbowSchoolProject
{
    public class TeachersData
    {
        private string _fileName = "TeacherData.txt";

        private List<Teacher> _teachers { get; set; } = new List<Teacher>();

        public TeachersData()
        {
            _FindFileOrCreate();
            RetrieveData();

        }

        private void _FindFileOrCreate()
        {
            if (!File.Exists(_fileName))
            {
                File.CreateText(_fileName);

            }
        }

        //get the data form the text file and store it in Teacher List
        private void RetrieveData()
        {
            List<String> FileLines = File.ReadAllLines(_fileName).ToList();

            foreach (var line in FileLines)
            {
                string[] enteries = line.Split(',');

                Teacher newTeacher = new Teacher();

                newTeacher.ID = Convert.ToInt32(enteries[0]);
                newTeacher.Name = enteries[1];
                newTeacher.ClassesAndSections = enteries[2].Split('-');

                _teachers.Add(newTeacher);

            }
        }


        //Store the teachers' data to the text file
        private void StoreData()
        {

            List<string> output = new List<string>();

            foreach (var teacher in _teachers)
            {


                output.Add($"{teacher.ID},{teacher.Name},{teacher.JoinClassesAndSections()}");

            }

            File.WriteAllLines(_fileName, output);

        }

        //Print colored message to the console
        private void ConsoleColorMessage(ConsoleColor color, string message)
        {
            Console.ForegroundColor = color;

            Console.WriteLine(message);

            Console.ResetColor();
        }

        //Display the teachers' data to the console
        public void DisplayAllTeachers()
        {

            foreach (var teacher in _teachers)
            {
                Console.Write($"Id: {teacher.ID} | Name: {teacher.Name} | Class(Section): ");

                foreach (var classSec in teacher.ClassesAndSections)
                {
                    Console.Write($" {classSec} ");
                }

                Console.WriteLine();
            }
        }

        //Add new teacher's data
        public void AddnewTeacher()
        {
            Teacher newTeacher = new Teacher();

            newTeacher.ID = _teachers.Count + 1;

            ConsoleColorMessage(ConsoleColor.Blue,"Enter the name of the teacher: ");

            newTeacher.Name = Console.ReadLine();

            ConsoleColorMessage(ConsoleColor.Blue, "Enter how many class and section to add: ");

            int numOfClassSec = Convert.ToInt32(Console.ReadLine());

            string[] newClassSec = new string[numOfClassSec];

            for (int i = 0; i < numOfClassSec; i++)
            {
                ConsoleColorMessage(ConsoleColor.Blue, $"Enter the name of class#{i + 1}: ");

                string className = Console.ReadLine();

                ConsoleColorMessage(ConsoleColor.Blue, $"Enter the section number of class#{i + 1}: ");

                string sectionNum = Console.ReadLine();

                newClassSec[i] = $"{className}({sectionNum})";
            }

            newTeacher.ClassesAndSections = newClassSec;

            _teachers.Add(newTeacher);

            ConsoleColorMessage(ConsoleColor.Green, "New teacher added successfully");

            //Store the data after adding new teacher
            StoreData();

        }

        //Update teacher's data (Ask the user which data to update)
        public void UpdateTeacher()
        {
            ConsoleColorMessage(ConsoleColor.Blue, "Enter the id of the teacher");
            string teacherIdString = Console.ReadLine();
            int enteredTeacherId;

            Int32.TryParse(teacherIdString, out enteredTeacherId);

            //Check if entered teacher id is exists
            while (!_teachers.Exists(x => x.ID == enteredTeacherId))
            {
                ConsoleColorMessage(ConsoleColor.Red, "The id is not vaild.");
                ConsoleColorMessage(ConsoleColor.Blue, "Enter another teacher id");

                teacherIdString = Console.ReadLine();
                Int32.TryParse(teacherIdString, out enteredTeacherId);
            }

            Teacher teacherToUodate = FindTeacherById(enteredTeacherId);

            ConsoleColorMessage(ConsoleColor.Blue, "Enter 1 to update teacher's name.");
            ConsoleColorMessage(ConsoleColor.Blue, "Enter 2 to update teacher's classes and sections.(this will remove all old classes and sections)");

            string choise = Console.ReadLine();

            bool rightChoise = true;
            while (rightChoise)
            {
                if (choise == "1" || choise == "2")
                {
                    rightChoise = false;

                }
                else
                {
                    ConsoleColorMessage(ConsoleColor.Red, "Invalid option, select a vaild option:");
                    ConsoleColorMessage(ConsoleColor.Blue, "Enter 1 to update teacher's name.");
                    ConsoleColorMessage(ConsoleColor.Blue, "Enter 2 to update teacher's classes and sections.(this will remove all old classes and sections)");
                    choise = Console.ReadLine();
                    rightChoise = true;
                    
                }
                

            }

            switch (choise)
            {
                case "1":
                    UpdateTeacherName(teacherToUodate);
                    break;
                case "2":
                    UpdateTeacherClassesAndSections(teacherToUodate);
                    break;
                default:
                    break;
            } 

            //Store the data after updating data
            StoreData();
        }

        //Update teacher's data (update the the name)
        private void UpdateTeacherName(Teacher teacherToUodate)
        {
            string oldTeacherName = teacherToUodate.Name;

            ConsoleColorMessage(ConsoleColor.Blue, "Enetr the new name");
            string newTeacherName = Console.ReadLine();

            teacherToUodate.Name = newTeacherName;

            ConsoleColorMessage(ConsoleColor.Green, "Teacher name updated successfully.");
            ConsoleColorMessage(ConsoleColor.Green, $"Old name: {oldTeacherName}, New name: {newTeacherName}");

        }

        //Update teacher's data (update the classes and sections)
        private void UpdateTeacherClassesAndSections(Teacher teacherToUodate)
        {
            string oldClassSec = teacherToUodate.JoinClassesAndSections();

            ConsoleColorMessage(ConsoleColor.Blue, "Enter how many class and section to add: ");
            int numOfClassSec = Convert.ToInt32(Console.ReadLine());

            string[] newClassSecArray = new string[numOfClassSec];

            for (int i = 0; i < numOfClassSec; i++)
            {
                ConsoleColorMessage(ConsoleColor.Blue, $"Enter the name of class#{i + 1}: ");

                string className = Console.ReadLine();

                ConsoleColorMessage(ConsoleColor.Blue, $"Enter the section number of class#{i + 1}: ");

                string sectionNum = Console.ReadLine();

                newClassSecArray[i] = $"{className}({sectionNum})";
            }

            teacherToUodate.ClassesAndSections = newClassSecArray;

            string newClassSec = teacherToUodate.JoinClassesAndSections();

            ConsoleColorMessage(ConsoleColor.Green, "Teacher classes and sections updated successfully.");
            ConsoleColorMessage(ConsoleColor.Green, $"Old classes: {oldClassSec}, New classes: {newClassSec}");
        }

        //Find the teacher data by the given Id and return it
        private Teacher FindTeacherById(int teacherID)
        {
            return _teachers.Find(x => x.ID == teacherID);
        }

    }
}
