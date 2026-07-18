using StudentManagementSystem;

Student alice = new Student();
alice.Name = "Alice";
alice.Grade = 95;

Student bob = new Student();
bob.Name = "Bob";
bob.Grade = 88;

Student carol = new Student();
carol.Name = "Carol";
carol.Grade = 92;

List<Student> students = new List<Student> { alice, bob, carol };

foreach (Student student in students) {
    Console.WriteLine($"{student.Name}: {student.Grade}");
}
