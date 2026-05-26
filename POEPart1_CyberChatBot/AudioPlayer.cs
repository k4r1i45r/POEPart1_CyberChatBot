using System;
using System.Media;
using System.IO;


namespace POEPart1_CyberChatBot
{
    internal class AudioPlayer
    {
        public void PlayGreeting()
        {
            try
            {
                string fullPath = @"C:\Users\Student\Downloads\greeting.wav.wav";

                if (File.Exists(fullPath))
                {
                    SoundPlayer player = new SoundPlayer(fullPath);
                    player.Play();  
                }
            }
            catch
            {
            
            }
        }
    }
}