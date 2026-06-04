using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementSystem
{
    internal class Student
    {
        private int _grade;

        public string Name { get; set; }

        public Student(string name, int grade)
        {
            Name = name;
            Grade = grade;
        }

        public int Grade
        {
            get { return _grade; }
            set
            {
                if (value >= 0 && value <= 100)
                {
                    _grade = value;
                }
            }
        }

        public string GetInfo()
        {
            return $"{Name} - Grade: {Grade}";
        }

        public bool IsPassingGrade()
        {
            return Grade >= 60;
        }
    }
}
