using System;
using System.Collections.Generic;


//Шаблон СТРАТЕГІЯ


namespace Labka_2_shablony 
{

    public interface IUserInterface
    {
        string GetUserInput();
        void DisplayQuestion(string questionText);
        void DisplayAnswer(object answer);
    }
    public class Question
    {
        public string Text { get; set; }
        public Type AnswerType { get; set; }
        public List<string> Options { get; set; }

        public Question(string text, Type answerType, List<string> options = null)
        {
            Text = text;
            AnswerType = answerType;
            Options = options;
        }
    }

    public class UserInterface
    {
        private IUserInterface platform;

        public UserInterface(IUserInterface platform)
        {
            this.platform = platform;
        }

        public void Run(Question question)
        {
            platform.DisplayQuestion(question.Text);

            string userInput = platform.GetUserInput();

            if (question.AnswerType == typeof(string))
            {
                platform.DisplayAnswer(userInput);
            }
            else if (question.AnswerType == typeof(int))
            {
                if (int.TryParse(userInput, out int result))
                {
                    platform.DisplayAnswer(result);
                }
                else
                {
                    platform.DisplayAnswer("Невірний формат відповіді.");
                }
            }
            else
            {
                platform.DisplayAnswer("Не підтримуваний тип відповіді.");
            }
        }
    }
    public class ConsoleUI : IUserInterface
    {
        public string GetUserInput()
        {
            return Console.ReadLine();
        }

        public void DisplayQuestion(string questionText)
        {
            Console.WriteLine(questionText);
        }

        public void DisplayAnswer(object answer)
        {
            Console.WriteLine("Відповідь: " + answer);
        }
    }

    public class WebUI : IUserInterface
    {
        public string GetUserInput()
        {
            return "<input type=\"text\" placeholder=\"ваша відповідь\">";
        }

        public void DisplayQuestion(string questionText)
        {
            Console.WriteLine(questionText);
        }

        public void DisplayAnswer(object answer)
        {
            Console.WriteLine("<h3>Відповідь: " + answer + "</h3>");
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var question = new Question("Як вас звати?", typeof(string));

            var consoleUI = new UserInterface(new ConsoleUI());
            var webUI = new UserInterface(new WebUI());

            consoleUI.Run(question);
            webUI.Run(question);
        }
    }

}