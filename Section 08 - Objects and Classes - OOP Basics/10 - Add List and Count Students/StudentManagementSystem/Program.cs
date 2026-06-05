using StudentManagementSystem;


List<Student> students = new List<Student> ();
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
            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Grade (0-100): ");
            int grade = int.Parse(Console.ReadLine());

            if (!Student.IsValidGrade(grade))
            {
                Console.WriteLine("Grade must be between 0 and 100.");
                break;
            }

            Student newStudent = new Student(name, grade);
            students.Add(newStudent);
            Console.WriteLine("Student added!");        
            break;
        case MenuChoice.ListAll:
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
            break;
        case MenuChoice.SearchByName:
            break;
        case MenuChoice.ShowCount:
            Console.WriteLine($"Total students: {students.Count}");
            break;
      
        default:
            Console.WriteLine("Invalid option.");
            break;

    }
}