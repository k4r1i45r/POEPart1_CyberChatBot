// See https://aka.ms/new-console-template for more information
// Initial setup

using System;
using System.Threading;
using POEPart1_CyberChatBot;

// See https://aka.ms/new-console-template for more information
// Initial setup

using System.Media;
using System.Threading;
using POEPart1_CyberChatBot;

namespace SypherChatbot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Sypher - Cybersecurity Assistant";
            Console.ForegroundColor = ConsoleColor.Cyan;

            // Play voice greeting using AudioPlayer
            AudioPlayer audio = new AudioPlayer();
            audio.PlayGreeting("greeting.wav");

            // Display Sypher ASCII art
            Console.WriteLine(@"   _____             _               
  / ____|           | |              
 | (___  _   _ _ __ | |__   ___ _ __ 
  \___ \| | | | '_ \| '_ \ / _ \ '__|
  ____) | |_| | |_) | | | |  __/ |   
 |_____/ \__, | .__/|_| |_|\___|_|   
          __/ | |                    
         |___/|_|                    ");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("~ Welcome to Sypher - Your Cybersecurity Awareness Assistant ~");
            Console.WriteLine();

            // Ask for user name with better prompt
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Before we begin, may I know your name? ");
            string userName = Console.ReadLine();

            // Validate name isn't empty
            while (string.IsNullOrWhiteSpace(userName))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("I didn't catch that. What should I call you? ");
                Console.ForegroundColor = ConsoleColor.White;
                userName = Console.ReadLine();
            }

            // Create user object
            User user = new User(userName);

            // Create chatbot
            ChatBot bot = new ChatBot();

            // Personalised welcome with border
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(new string('=', 55));
            Console.WriteLine($"Hello {user.Name}! I'm Sypher, your personal cybersecurity guide.");
            Console.WriteLine($"I'm here to help you learn about online safety, {user.Name}.");
            Console.WriteLine("Type 'exit' or 'quit' to end our conversation.");
            Console.WriteLine(new string('=', 55));
            Console.WriteLine();

            // Main chat loop
            string userInput;
            do
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{user.Name}: ");
                userInput = Console.ReadLine();

                // Handle empty input
                if (string.IsNullOrWhiteSpace(userInput))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Sypher: Please type something. I'm here to help!");
                    continue;
                }

                // Check for exit command
                if (userInput.ToLower() == "exit" || userInput.ToLower() == "quit")
                    break;

                // Get response from chatbot
                string response = bot.GetResponse(userInput, user.Name);

                // Display response with typing effect
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Sypher: ");
                TypeText(response);

            } while (true);

            // Goodbye message with border
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(new string('=', 55));
            Console.WriteLine($"Goodbye {user.Name}! Remember: Stay safe, stay secure!");
            Console.WriteLine("Sypher will always be here when you need cybersecurity advice.");
            Console.WriteLine(new string('=', 55));
            Console.ResetColor();
        }

        // Typing effect method
        static void TypeText(string text, int delay = 30)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
        }
    }
}