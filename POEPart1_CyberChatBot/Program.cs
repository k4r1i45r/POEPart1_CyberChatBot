// See https://aka.ms/new-console-template for more information
// Initial setup

using System;
using System.Media;
using System.Threading;
using POEPart1_CyberChatBot;

namespace POEPart1_CyberChatBot
{

        class Program
        {
            static void Main(string[] args)
            {
                Console.Title = "Sypher - Cybersecurity Assistant";
                Console.ForegroundColor = ConsoleColor.Cyan;

                AudioPlayer audio = new AudioPlayer();
                audio.PlayGreeting();

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
                Console.WriteLine("Welcome to Sypher - Your Cybersecurity Awareness Assistant");
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("May I know your name? ");
                string userName = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(userName))
                {
                    Console.Write("Please tell me your name: ");
                    userName = Console.ReadLine();
                }

                User user = new User(userName);
                ChatBot bot = new ChatBot();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(new string('=', 55));
                Console.WriteLine($"Hello {user.Name}! I'm Sypher, your personal cybersecurity guide.");
                Console.WriteLine($"Ask me about passwords, phishing, malware, 2FA, VPNs, and more.");
                Console.WriteLine(new string('=', 55));
                Console.WriteLine();

                string userInput;
                do
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"{user.Name}: ");
                    userInput = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(userInput))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Sypher: Please type something. I'm here to help!");
                        Console.WriteLine();
                        continue;
                    }

                    if (userInput.ToLower() == "exit" || userInput.ToLower() == "quit")
                        break;

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
                    Console.WriteLine();  

                } while (true);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(new string('=', 55));
                Console.WriteLine($"Goodbye {user.Name}! Stay safe, stay secure.");
                Console.WriteLine(new string('=', 55));
                Console.ResetColor();
            }

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