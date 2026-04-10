using System;
using System.IO;
using System.Media;

namespace POEPart1_CyberChatBot
{
    internal class AudioPlayer
    {
        public void PlayGreeting(string fileName)
        {
            try
            {
                if (!File.Exists(fileName))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Audio file not found.");
                    return;
                }

                SoundPlayer player = new SoundPlayer(fileName);
                player.PlaySync();
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error playing audio.");
            }
        }
    }
}
