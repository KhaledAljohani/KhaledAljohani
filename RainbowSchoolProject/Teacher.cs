using System;
namespace RainbowSchoolProject
{
    public class Teacher
    {
        public int ID { get; set; }
        
        public string Name { get; set; }
        public string[] ClassesAndSections { get; set; }

        public string JoinClassesAndSections()
        {
            return string.Join("-", ClassesAndSections);
        }
    }
}
