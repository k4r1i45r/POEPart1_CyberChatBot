// See https://aka.ms/new-console-template for more information
// Initial setup

using System;
using System.Threading;
using POEPart1_CyberChatBot;

namespace SypherChatbot
{
    class Program
    {
        static void Main(string[] args)
        {
            SetupConsole();
            PlayGreeting();
            DisplayAsciiArt();

            string userName = GetUserName();
            User user = new User(userName);

            DisplayWelcome(user);
            StartChat(user);
            DisplayGoodbye(user);
        }

        static void SetupConsole()
        {
            Console.Title = "Sypher - Cybersecurity Assistant";
            Console.ForegroundColor = ConsoleColor.Cyan;
        }

        static void PlayGreeting()
        {
            AudioPlayer audio = new AudioPlayer();

            try
            {
                audio.PlayGreeting("Greeting.wav");
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Audio could not be played.");
            }
        }

        static void DisplayAsciiArt()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║        SYPHER CYBER BOT              ║");
            Console.WriteLine("╚══════════════════════════════════════╝");

            Console.WriteLine(@"
   _____             _               
  / ____|           | |              
 | (___  _   _ _ __ | |__   ___ _ __ 
  \___ \| | | | '_ \| '_ \ / _ \ '__|
  ____) | |_| | |_) | | | |  __/ |   
 |_____/ \__, | .__/|_| |_|\___|_|   
          __/ | |                    
         |___/|_|                    
");

            Console.ResetColor();
        }

        static string GetUserName()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\nEnter your name: ");
            string name = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(name))
            {
                Console.Write("Please enter a valid name: ");
                name = Console.ReadLine();
            }

            return name.Trim();
        }

        static void DisplayWelcome(User user)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"Hello {user.Name}, I am Sypher. Your cybersecurity assistant.");
            Console.WriteLine("Type 'exit' or 'quit' to end the conversation.");
            Console.WriteLine(new string('-', 50));
            Console.WriteLine();
        }

        static void StartChat(User user)
        {
            ChatBot bot = new ChatBot();
            string userInput;

            do
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{user.Name}: ");
                userInput = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(userInput))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Sypher: Please type something.");
                    continue;
                }

                if (userInput.Equals("exit", StringComparison.OrdinalIgnoreCase) ||
                    userInput.Equals("quit", StringComparison.OrdinalIgnoreCase))
                    break;

                ShowLoading();
                string response = bot.GetResponse(userInput, user.Name);

                if (response.StartsWith("EXIT|"))
                {
                    string exitMessage = response.Substring(5);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("Sypher: ");
                    TypeText(exitMessage);
                    break;
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Sypher: ");
                TypeText(response);

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(new string('-', 40));

            } while (true);
        }

        static void DisplayGoodbye(User user)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"Goodbye {user.Name}. Stay safe online!");
            Console.WriteLine(new string('-', 50));
            Console.ResetColor();
        }

        static void ShowLoading()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("Sypher is thinking");

            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(300);
                Console.Write(".");
            }

            Console.WriteLine();
        }

        static void TypeText(string text, int minDelay = 20, int maxDelay = 50)
        {
            Random rand = new Random();

            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(rand.Next(minDelay, maxDelay));
            }

            Console.WriteLine();
        }
    }
}