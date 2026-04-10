using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POEPart1_CyberChatBot
{
    internal class ChatBot
    {
        Random rand = new Random();

        public string GetResponse(string userInput, string userName)
        {
            string input = userInput.ToLower().Trim();

            if (input.Contains("thank") || input.Contains("bye") || input.Contains("goodbye") || input.Contains("quit"))
            {
                return $"EXIT|No problem, {userName}. Stay safe out there 👋";
            }

            if (input.Contains("hello") || input.Contains("hi"))
            {
                string[] responses =
                {
                    $"Hey {userName}, how can I help you today?",
                    $"Hi {userName}! Got any cybersecurity questions?",
                    $"Hello {userName} 👋 what would you like to know?"
                };
                return Pick(responses);
            }

            if (input.Contains("how are you"))
            {
                string[] responses =
                {
                    $"Doing good, {userName}. Ready to keep you safe online 😄",
                    $"All good here. What do you need help with?",
                    $"I'm good! Let's talk cybersecurity."
                };
                return Pick(responses);
            }

            if (input.Contains("purpose") || input.Contains("what do you do"))
            {
                return $"I help you understand cybersecurity basics, {userName}. Things like passwords, scams, and staying safe online.";
            }

            if (input.Contains("password"))
            {
                string[] responses =
                {
                    $"Make your passwords long and unpredictable, {userName}. Think phrases, not just words.",
                    $"A strong password = 12+ characters, mix of symbols, numbers, and letters.",
                    $"Tip: Don't reuse passwords across sites. One breach can expose everything."
                };
                return Pick(responses);
            }

            if (input.Contains("phishing"))
            {
                string[] responses =
                {
                    $"Phishing is basically fake messages trying to trick you into giving info.",
                    $"If a link looks suspicious, don’t click it. That’s how phishing usually works.",
                    $"Always double-check emails asking for passwords or banking info."
                };
                return Pick(responses);
            }

            if (input.Contains("malware") || input.Contains("virus"))
            {
                string[] responses =
                {
                    $"Malware is harmful software. Avoid sketchy downloads.",
                    $"Viruses often come from unsafe files or websites.",
                    $"Keep your antivirus updated, it actually matters."
                };
                return Pick(responses);
            }

            if (input.Contains("2fa") || input.Contains("two factor") || input.Contains("mfa"))
            {
                return $"2FA adds a second lock to your account, {userName}. Even if someone gets your password, they’re still blocked.";
            }

            if (input.Contains("vpn"))
            {
                return $"A VPN hides your internet activity, especially useful on public Wi-Fi.";
            }

            if (input.Contains("update") || input.Contains("patch"))
            {
                return $"Updates fix security holes. Skipping them = leaving your door open.";
            }

            if (input.Contains("help"))
            {
                return $"You can ask me about passwords, phishing, malware, VPNs, or 2FA.";
            }

            return $"Hmm {userName}, I’m not sure about that. Try asking something about cybersecurity.";
        }

        private string Pick(string[] options)
        {
            return options[rand.Next(options.Length)];
        }
    }
}