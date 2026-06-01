using StudentManagementSystem;

Student alice = new Student();
alice.Name = "Alice";
alice.Grade = 95;

Student bob = new Student();
bob.Name = "Bob";
bob.Grade = 88;

Console.WriteLine($"{alice.Name}: {alice.Grade}");
Console.WriteLine($"{bob.Name}: {bob.Grade}");