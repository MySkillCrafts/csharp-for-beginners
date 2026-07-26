using FlashcardTrainer;

Flashcard card = new Flashcard("Capital of Poland?", "Warsaw");
string line = card.ToFileLine();
Console.WriteLine(line);

Flashcard loaded = Flashcard.FromFileLine(line);
Console.WriteLine(loaded.GetPreview());

bool running = true;
while (running)
{
    Console.WriteLine();
    Console.WriteLine("--- Flashcard Trainer ---");
    Console.WriteLine("1. Add New Flashcard");
    Console.WriteLine("2. View All Flashcards");
    Console.WriteLine("3. Study Mode");
    Console.WriteLine("4. Quiz Mode");
    Console.WriteLine("0. Exit");
    
    Console.Write("Your choice: ");

    if (!int.TryParse(Console.ReadLine(), out int choice))
    {
        Console.WriteLine("Invalid option.");
        continue;
    }

    switch ((MenuOption)choice)
    {       
        case MenuOption.AddNewFlashcard:
            break;

        case MenuOption.ViewAllFlashcards:
            break;

        case MenuOption.StudyMode:
            break;

        case MenuOption.QuizMode:
            break;

        case MenuOption.Exit:
            running = false;
            break;

        default:
            Console.WriteLine("Invalid option.");
            break;
    }
}