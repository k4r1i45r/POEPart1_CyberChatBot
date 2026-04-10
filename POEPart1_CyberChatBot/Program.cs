// See https://aka.ms/new-console-template for more information
using POEPart1_CyberChatBot;

namespace SypherChatbot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Sypher - Cybersecurity Assistant";
            Console.ForegroundColor = ConsoleColor.Cyan;

            // Display Sypher ASCII art (paste your art here)
            Console.WriteLine(@"   _____             _               
  / ____|           | |              
 | (___  _   _ _ __ | |__   ___ _ __ 
  \___ \| | | | '_ \| '_ \ / _ \ '__|
  ____) | |_| | |_) | | | |  __/ |   
 |_____/ \__, | .__/|_| |_|\___|_|   
          __/ | |                    
         |___/|_|                    ");

            // Ask for user name
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\nEnter your name: ");
            string userName = Console.ReadLine();

            // Validate name isn't empty
            while (string.IsNullOrWhiteSpace(userName))
            {
                Console.Write("Please enter a valid name: ");
                userName = Console.ReadLine();
            }

            // Create user object
            User user = new User(userName);

            // Create chatbot
            ChatBot bot = new ChatBot();

            // Personalised welcome
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nHello {user.Name}, I am Sypher. Your cybersecurity assistant.");
            Console.WriteLine("Type 'exit' or 'quit' to end the conversation.\n");

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
                    Console.WriteLine("Sypher: Please type something.");
                    continue;
                }

                // Check for exit command
                if (userInput.ToLower() == "exit" || userInput.ToLower() == "quit")
                    break;

                // Get response from chatbot
                string response = bot.GetResponse(userInput, user.Name);

                // Display response
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Sypher: {response}");

            } while (true);

            // Goodbye message
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nGoodbye {user.Name}. Stay safe online!");
            Console.ResetColor();
        }
    }
}


