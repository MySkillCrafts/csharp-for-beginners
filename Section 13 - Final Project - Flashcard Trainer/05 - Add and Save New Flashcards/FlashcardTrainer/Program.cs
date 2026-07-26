using FlashcardTrainer;

//Flashcard card = new Flashcard("Capital of Poland?", "Warsaw");
//string line = card.ToFileLine();
//Console.WriteLine(line);

//Flashcard loaded = Flashcard.FromFileLine(line);
//Console.WriteLine(loaded.GetPreview());

string filePath = "flashcards.txt";
List<Flashcard> flashcards = new List<Flashcard>();

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
            Console.Write("Question: ");
            string question = Console.ReadLine();

            if (question == "")
            {
                Console.WriteLine("Question cannot be empty.");
                break;
            }

            Console.Write("Answer: ");
            string answer = Console.ReadLine();
            if (answer == "")
            {
                Console.WriteLine("Answer cannot be empty.");
                break;
            }

            Flashcard flashcard = new Flashcard(question, answer);
            flashcards.Add(flashcard);

            Flashcard.SaveFlashcards(flashcards, filePath);
            Console.WriteLine("Flashcard saved!");

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