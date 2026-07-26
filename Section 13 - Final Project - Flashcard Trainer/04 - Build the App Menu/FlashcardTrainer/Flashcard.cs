using System;
using System.Collections.Generic;
using System.Text;

namespace FlashcardTrainer
{
    internal class Flashcard
    {
        public string Question { get; set; }
        public string Answer { get; set; }

        public Flashcard(string question, string answer)
        {
            Question = question;
            Answer = answer;
        }

        public string GetPreview()
        {
            return $"Q: {Question}  |  A: {Answer}";
        }

        public string ToFileLine()
        {
            return $"{Question}|{Answer}";
        }

        public static Flashcard FromFileLine(string line)
        {
            string[] parts = line.Split('|');

            Flashcard result = new Flashcard(parts[0], parts[1]);

            return result;
        }
    }
}
