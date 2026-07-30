using FlashcardTrainer;

string filePath = "flashcards.txt";
List<Flashcard> flashcards = new List<Flashcard>();

if (File.Exists(filePath))
{
    string[] lines = File.ReadAllLines(filePath);

    foreach (string line in lines)
    {
        if (line != "")
        {
            flashcards.Add(Flashcard.FromFileLine(line));
        }
    }

    Console.WriteLine($"Loaded {flashcards.Count} flashcards.");

}

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

            if (flashcards.Count == 0)
            {
                Console.WriteLine("No flashcards yet. Add a few first!");
            }
            else
            {
                foreach (Flashcard card in flashcards)
                {
                    Console.WriteLine(card.GetPreview());
                }
            }

            break;

        case MenuOption.StudyMode:
            if (flashcards.Count == 0)
            {
                Console.WriteLine("No flashcards yet. Add a few first!");
                break;
            }

            int gotIt = 0;

            foreach (Flashcard card in flashcards)
            {
                Console.WriteLine();
                Console.WriteLine(card.Question);
                Console.WriteLine("(Press Enter when you want to see the answer.)");
                Console.ReadLine();
                Console.WriteLine(card.Answer);
                Console.Write("Did you get it right? (y/n): ");
                string selfCheck = Console.ReadLine();

                if (selfCheck.ToLower() == "y")
                {
                    gotIt++;
                }
            }

            Console.WriteLine();
            Console.WriteLine($"Self-check: {gotIt} / {flashcards.Count}");

            break;

        case MenuOption.QuizMode:
            if (flashcards.Count == 0)
            {
                Console.WriteLine("No flashcards yet. Add a few first!");
                break;
            }

            int correct = 0;
            int rounds = flashcards.Count;

            for (int i = 0; i < rounds; i++)
            {
                int cardIndex = Random.Shared.Next(0, flashcards.Count);
                Flashcard card = flashcards[cardIndex];
                Console.WriteLine();
                Console.WriteLine(card.Question);
                Console.Write("Your answer: ");
                string guess = Console.ReadLine();

                if (guess.ToLower() == card.Answer.ToLower())
                {
                    Console.WriteLine("Correct!");
                    correct++;
                }
                else
                {
                    Console.WriteLine($"Not quite — the answer was: {card.Answer}");
                }
            }

            Console.WriteLine();
            Console.WriteLine($"Score: {correct} / {rounds}");

            break;

        case MenuOption.Exit:
            running = false;
            break;

        default:
            Console.WriteLine("Invalid option.");
            break;
    }
}