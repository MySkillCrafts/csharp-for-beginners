using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementSystem
{
    internal class Student
    {
        private int _grade;

        public string Name;

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
    }
}
