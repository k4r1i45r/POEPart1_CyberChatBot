using System;
using System.Collections.Generic;

namespace SypherUI
{
    public class ChatBot
    {
        private readonly Dictionary<string, string> _memory = new Dictionary<string, string>();
        private readonly Random _random = new Random();

        private readonly Dictionary<string, List<string>> _responses;

        public ChatBot()
        {
            _responses = new Dictionary<string, List<string>>
            {
                { "password", new List<string> { "Use strong unique passwords for every account.", "Never reuse passwords.", "Use a password manager." } },
                { "scam", new List<string> { "Scammers create urgency. Pause and verify.", "Never send money to strangers.", "Verify requests before acting." } },
                { "phishing", new List<string> { "Check sender email carefully.", "Hover over links before clicking.", "Never enter password from email." } },
                { "privacy", new List<string> { "Use VPN on public Wi-Fi.", "Review app permissions.", "Limit personal info shared online." } },
                { "malware", new List<string> { "Keep antivirus updated.", "Avoid cracked software.", "Be careful with unknown USBs." } },
                { "2fa", new List<string> { "Enable 2FA on important accounts.", "Use authenticator apps.", "Better than SMS." } },
                { "vpn", new List<string> { "VPN encrypts your connection.", "Use on public Wi-Fi.", "Choose reputable providers." } },
                { "update", new List<string> { "Always install updates.", "They fix security holes.", "Enable auto updates." } }
            };
        }

        public void RememberInfo(string key, string value) => _memory[key] = value;

        public string GetName() => _memory.ContainsKey("name") ? _memory["name"] : "User";

        public void ClearMemory() => _memory.Clear();

        public string GetResponse(string userInput)
        {
            if (string.IsNullOrWhiteSpace(userInput))
                return "Please type a message.";

            string input = userInput.ToLower().Trim();
            string name = GetName();

            // Name Memory
            if (input.Contains("my name is") || input.Contains("call me"))
            {
                string newName = ExtractName(userInput);
                if (!string.IsNullOrEmpty(newName))
                {
                    RememberInfo("name", newName);
                    return $"Got it! I'll call you {newName}.";
                }
            }

            if (input.Contains("what is my name") || input.Contains("what's my name"))
                return _memory.ContainsKey("name") ? $"Your name is {_memory["name"]}." : "I don't know your name yet.";

            // Favorite Topic Memory
            if (input.Contains("interested in") || input.Contains("favorite topic"))
            {
                string detectedTopic = DetectTopic(input);
                if (!string.IsNullOrEmpty(detectedTopic))
                {
                    RememberInfo("favoritetopic", detectedTopic);
                    return $"I'll remember your favorite topic is {detectedTopic}. {GetRandomResponse(detectedTopic)}";
                }
            }

            if (input.Contains("favorite topic"))
                return _memory.ContainsKey("favoritetopic") ? $"Your favorite topic is {_memory["favoritetopic"]}." : "You haven't told me your favorite topic yet.";

            // Sentiment
            if (input.Contains("worried") || input.Contains("scared"))
                return $"I understand you're worried, {name}. " + GetRandomResponse(DetectTopic(input));

            if (input.Contains("frustrated") || input.Contains("confused"))
                return $"I hear you, {name}. Let's take it step by step. " + GetRandomResponse(DetectTopic(input));

            // Keyword Response
            string topic = DetectTopic(input);
            if (!string.IsNullOrEmpty(topic))
                return GetRandomResponse(topic);

            return $"Hi {name}, what would you like to know about cybersecurity?";
        }

        private string DetectTopic(string input)
        {
            if (input.Contains("password")) return "password";
            if (input.Contains("scam")) return "scam";
            if (input.Contains("phish")) return "phishing";
            if (input.Contains("privac")) return "privacy";
            if (input.Contains("malware")) return "malware";
            if (input.Contains("2fa") || input.Contains("two factor")) return "2fa";
            if (input.Contains("vpn")) return "vpn";
            if (input.Contains("update")) return "update";
            return "";
        }

        private string GetRandomResponse(string topic)
        {
            if (!string.IsNullOrEmpty(topic) && _responses.ContainsKey(topic))
            {
                var list = _responses[topic];
                return list[_random.Next(list.Count)];
            }
            return "Let me know more details and I'll help you.";
        }

        private string ExtractName(string input)
        {
            string[] words = input.Split(' ');
            for (int i = 0; i < words.Length - 1; i++)
            {
                if (words[i].ToLower() == "is" || words[i].ToLower() == "me")
                {
                    if (i + 1 < words.Length)
                        return char.ToUpper(words[i + 1][0]) + words[i + 1].Substring(1).ToLower();
                }
            }
            return "";
        }
    }
}