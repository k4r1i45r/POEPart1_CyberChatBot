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
                { "password", new List<string>
                    {
                        "Use strong, unique passwords for every account. A password manager makes this much easier.",
                        "Never reuse the same password across different websites.",
                        "Consider using passphrases like 'Correct-Horse-Battery-Staple'."
                    }
                },
                { "scam", new List<string>
                    {
                        "Scammers create urgency. Always pause and verify before acting.",
                        "Never send money or personal details to someone who contacted you first.",
                        "Be suspicious of unexpected requests for information or money."
                    }
                },
                { "phishing", new List<string>
                    {
                        "Always check the sender's email address carefully.",
                        "Hover over links before clicking to see the real destination.",
                        "Never enter your password on a site reached from an email."
                    }
                },
                { "privacy", new List<string>
                    {
                        "Use a VPN on public Wi-Fi to protect your data.",
                        "Review app permissions regularly.",
                        "Limit personal information shared on social media."
                    }
                },
                { "malware", new List<string>
                    {
                        "Keep your antivirus updated and avoid cracked software.",
                        "Be careful with email attachments and unknown USB drives.",
                        "Don't click on pop-ups saying your computer is infected."
                    }
                },
                { "2fa", new List<string>
                    {
                        "2FA adds a strong second layer of protection.",
                        "Use an authenticator app instead of SMS when possible.",
                        "Enable 2FA on your email and banking accounts first."
                    }
                },
                { "vpn", new List<string>
                    {
                        "A VPN encrypts your connection, especially on public Wi-Fi.",
                        "Choose reputable VPNs with no-logs policy.",
                        "Always turn on your VPN before using public networks."
                    }
                },
                { "update", new List<string>
                    {
                        "Software updates contain important security patches.",
                        "Enable automatic updates whenever possible.",
                        "Outdated software is a common target for hackers."
                    }
                }
            };
        }

        public void RememberInfo(string key, string value) => _memory[key] = value;
        public string GetName() => _memory.ContainsKey("name") ? _memory["name"] : "User";
        public void ClearMemory() => _memory.Clear();

        public string GetResponse(string userInput)
        {
            if (string.IsNullOrWhiteSpace(userInput))
                return "Please type a message so I can help you.";

            string input = userInput.ToLower().Trim();
            string name = GetName();

            // General conversation
            if (input.Contains("how are you"))
                return $"I'm doing great, {name}! Ready to help you with cybersecurity. How are you?";

            if (input.Contains("who are you") || input.Contains("purpose"))
                return "I'm Sypher AI, your cybersecurity awareness assistant. My purpose is to help you stay safe online.";

            // Name handling
            if (input.Contains("my name is") || input.Contains("call me"))
            {
                string extractedName = ExtractName(userInput);
                if (!string.IsNullOrEmpty(extractedName))
                {
                    RememberInfo("name", extractedName);
                    return $"Nice to meet you, {extractedName}! How can I help you today?";
                }
            }

            // Sentiment
            if (input.Contains("worried") || input.Contains("scared") || input.Contains("afraid"))
                return $"I understand you're worried, {name}. That's completely normal. " + GetRandomResponse(DetectTopic(input));

            if (input.Contains("frustrated") || input.Contains("confused"))
                return $"I hear you, {name}. Let's take this one step at a time. " + GetRandomResponse(DetectTopic(input));

            // Keyword detection
            string topic = DetectTopic(input);
            if (!string.IsNullOrEmpty(topic))
                return GetRandomResponse(topic);

            // Default
            return $"Good question, {name}. Would you like tips on passwords, scams, phishing, malware, 2FA, VPNs, or updates?";
        }

        private string DetectTopic(string input)
        {
            if (input.Contains("password")) return "password";
            if (input.Contains("scam")) return "scam";
            if (input.Contains("phish")) return "phishing";
            if (input.Contains("privac")) return "privacy";
            if (input.Contains("malware") || input.Contains("virus")) return "malware";
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
            return "Let me know more details and I'll give you helpful advice.";
        }

        private string ExtractName(string input)
        {
            string[] words = input.Split(' ');
            for (int i = 0; i < words.Length - 1; i++)
            {
                if (words[i].ToLower() == "is" || words[i].ToLower() == "me")
                {
                    if (i + 1 < words.Length)
                        return words[i + 1];
                }
            }
            return "";
        }
    }
}