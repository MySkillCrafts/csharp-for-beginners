using StudentManagementSystem;

Student alice = new Student("Alice", 95);
Student bob = new Student("Bob", 88);
Student carol = new Student("Carol", 92);

List<Student> students = new List<Student> { alice, bob, carol };
bool running = true;

while (running) {
    Console.WriteLine();
    Console.WriteLine("--- Student Management ---");
    Console.WriteLine("1. Add Student");
    Console.WriteLine("2. List All Students");
    Console.WriteLine("3. Search by Name");
    Console.WriteLine("4. Show Student Count");
    Console.WriteLine("0. Exit");
    
    Console.Write("Your choice: ");

    if (!int.TryParse(Console.ReadLine(), out int input)) {
        Console.WriteLine("Please enter a number.");
        continue;
    }

    MenuChoice choice = (MenuChoice)input;

    switch (choice)
    {
        case MenuChoice.Exit:
            running = false;
            break;
        case MenuChoice.AddStudent:          
            break;
        case MenuChoice.ListAll:
            break;
        case MenuChoice.SearchByName:
            break;
        case MenuChoice.ShowCount:
            break;
      
        default:
            Console.WriteLine("Invalid option.");
            break;

    }
}

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
