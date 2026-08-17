using StudentManagementSystem;

Student alice = new Student("Alice", 95);
Student bob = new Student("Bob", 88);
Student carol = new Student("Carol", 92);

List<Student> students = new List<Student> { alice, bob, carol };

foreach (Student student in students)
{
    Console.WriteLine(student.GetInfo());

    if (student.IsPassingGrade())
    {
        Console.WriteLine("  Status: Passing");
    }
    else
    {
        Console.WriteLine("  Status: Needs improvement");
    }
}

Console.WriteLine(Student.IsValidGrade(85));
Console.WriteLine(Student.IsValidGrade(150));
