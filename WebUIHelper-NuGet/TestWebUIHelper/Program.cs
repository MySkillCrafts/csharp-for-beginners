using CSharpLearning.WebUIHelper;

Console.WriteLine("🎓 Testing CSharpLearning.WebUIHelper NuGet Package");
Console.WriteLine("================================================\n");

// Example 1: Arrays vs Lists
Console.WriteLine("📚 Example 1: Arrays vs Lists");
string[] students = { "Alice", "Bob", "Charlie" };
List<string> studentList = new List<string> { "David", "Eve" };

WebUIHelper.ShowArrayVsList(students, studentList, 3, 2, 5, 7);
Console.ReadKey();

// Example 2: Generics Demo
Console.WriteLine("\n🔧 Example 2: Generics Demo");
int[] numbers = { 1, 2, 3, 4, 5 };
WebUIHelper.ShowGenericsDemo(numbers, "Generics with Numbers");
Console.ReadKey();

// Example 3: List Operations
Console.WriteLine("\n📋 Example 3: List Operations");
List<string> grades = new List<string> { "A", "B", "C", "D" };
WebUIHelper.ShowListOperations(grades, "Sort", "A, B, C, D", "List Sorting");
Console.ReadKey();

// Example 4: Custom Lesson
Console.WriteLine("\n🎨 Example 4: Custom Lesson");
string customHtml = @"
    <div class='card'>
        <div class='card-title'>🎯 Custom Lesson</div>
        <div class='highlight'>
            <strong>This is a custom lesson!</strong><br>
            You can create any HTML content you want for your educational presentations.
        </div>
    </div>";
WebUIHelper.ShowCustomLesson(customHtml, "Custom Educational Content");

Console.WriteLine("\n✨ All examples completed!");
Console.WriteLine("Press any key to exit...");
Console.ReadKey();

// Clean up
WebUIHelper.Stop();
