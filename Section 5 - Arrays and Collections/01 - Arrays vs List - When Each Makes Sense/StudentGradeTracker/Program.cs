class Program
{
    static void Main()
    {
        // =============================================================================
        // LESSON 1: Arrays vs Lists - When Each Makes Sense
        // =============================================================================

        // Arrays - Fixed size collections
        string[] subjects = { "Math", "Science", "English", "History" };

        // Lists - Dynamic collections  
        List<string> studentNames = new List<string>();

        // Track sizes for comparison
        int arraySize = subjects.Length;
        int initialCount = studentNames.Count;

        // Add students to list
        studentNames.Add("Alice Johnson");
        studentNames.Add("Bob Smith");
        studentNames.Add("Carol Davis");

        int afterAdding = studentNames.Count;

        // Add more students
        studentNames.Add("David Wilson");
        studentNames.Add("Eva Brown");

        int finalCount = studentNames.Count;

        // Show comparison in beautiful web interface
        WebUIHelper.ShowArrayVsList(subjects, studentNames, arraySize, initialCount, afterAdding, finalCount);

        Console.ReadKey();
        WebUIHelper.Stop();
    }
}