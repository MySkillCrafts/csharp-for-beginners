using FlashcardTrainer;

Flashcard card = new Flashcard("Capital of Poland?", "Warsaw");
string line = card.ToFileLine();
Console.WriteLine(line);

Flashcard loaded = Flashcard.FromFileLine(line);
Console.WriteLine(loaded.GetPreview());