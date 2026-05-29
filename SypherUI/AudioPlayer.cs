using System.Media;
using System.IO;

namespace SypherUI
{
    public static class AudioPlayer
    {
        private static SoundPlayer _player;

        public static void PlayGreeting()
        {
            try
            {
             
                string filePath = "greeting.wav";

                if (File.Exists(filePath))
                {
                    _player = new SoundPlayer(filePath);
                    _player.Play();           // Play once
                }
                else
                {
                    
                    string backupPath = @"C:\Users\Student\Downloads\greeting.wav.wav";
                    if (File.Exists(backupPath))
                    {
                        _player = new SoundPlayer(backupPath);
                        _player.Play();
                    }
                }
            }
            catch
            {
                // Fail silently - don't crash the app
            }
        }

        public static void StopGreeting()
        {
            _player?.Stop();
        }
    }
}