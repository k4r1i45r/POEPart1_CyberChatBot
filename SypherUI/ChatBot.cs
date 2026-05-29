using System;
using System.Collections.Generic;

namespace SypherUI
{
    public class ChatBot
    {
        private Dictionary<string, string> _memory = new Dictionary<string, string>();
        private Random _random = new Random();

        private Dictionary<string, List<string>> _keywordResponses;

        public ChatBot()
        {
            _keywordResponses = new Dictionary<string, List<string>>()
            {
                { "password", new List<string>()
                    {
                        "Use a strong password with at least 12 characters including numbers and symbols.",
                        "Never reuse the same password across different websites or accounts.",
                        "Consider using a passphrase like Correct-Horse-Battery-Staple instead of a single word.",
                        "A password manager can help you generate and store unique passwords safely."
                    }
                },
                { "scam", new List<string>()
                    {
                        "Scammers often create a sense of urgency. Always take time to think before acting.",
                        "Never share personal information or send money to someone who contacted you first.",
                        "Be suspicious of unexpected calls, emails, or messages asking for help or money.",
                        "Verify the identity of anyone asking for sensitive information by calling them back on an official number."
                    }
                },
                { "privacy", new List<string>()
                    {
                        "Review your social media privacy settings regularly to control who sees your information.",
                        "Limit the amount of personal details you share online like your address or birthday.",
                        "Use a VPN when connecting to public Wi-Fi networks to protect your data.",
                        "Check which apps have access to your camera, microphone, and location."
                    }
                },
                { "phishing", new List<string>()
                    {
                        "Check the sender's email address carefully. Scammers use addresses that look real but are slightly different.",
                        "Hover over links before clicking to see where they actually lead.",
                        "Never enter your password on a website you reached from an email link.",
                        "Look for poor grammar, urgent language, or generic greetings like Dear Customer."
                    }
                },
                { "malware", new List<string>()
                    {
                        "Keep your antivirus software updated and run regular scans.",
                        "Avoid downloading software from unofficial or unknown websites.",
                        "Do not click on pop-ups that say your computer is infected. That is often a scam.",
                        "Be careful with email attachments from senders you do not recognise."
                    }
                },
                { "2fa", new List<string>()
                    {
                        "Two-factor authentication adds an extra layer of security beyond just your password.",
                        "Use an authenticator app like Google Authenticator instead of SMS when possible.",
                        "Hardware keys like YubiKey offer the highest level of protection for your accounts.",
                        "Enable 2FA on your email, banking, and social media accounts first as they are most important."
                    }
                },
                { "vpn", new List<string>()
                    {
                        "A VPN encrypts your internet traffic, making it safer to use public Wi-Fi networks.",
                        "Choose a VPN provider that has a clear no-logs policy.",
                        "Always turn on your VPN before accessing sensitive accounts like banking on public networks.",
                        "Free VPNs may sell your data. Paid reputable services are generally safer."
                    }
                },
                { "update", new List<string>()
                    {
                        "Software updates often contain important security patches for known vulnerabilities.",
                        "Enable automatic updates on your operating system, browser, and apps when possible.",
                        "Outdated software is one of the most common ways hackers gain access to devices.",
                        "Do not ignore update reminders. Set aside time to install them regularly."
                    }
                }
            };
        }

        public string GetName()
        {
            if (_memory.ContainsKey("name"))
                return _memory["name"];
            return "there";
        }

        public void RememberInfo(string key, string value)
        {
            if (!_memory.ContainsKey(key))
                _memory.Add(key, value);
            else
                _memory[key] = value;
        }

        public string GetResponse(string userInput)
        {
            if (string.IsNullOrWhiteSpace(userInput))
                return "Please type a message so I can help you with cybersecurity.";

            string input = userInput.ToLower().Trim();
            string name = GetName();

            // Handle first-time name capture
            if (!_memory.ContainsKey("name"))
            {
                string detectedName = userInput.Trim();
                if (detectedName.Length > 0 && detectedName.Length < 20 && !detectedName.Contains(" "))
                {
                    char first = char.ToUpper(detectedName[0]);
                    string rest = detectedName.Length > 1 ? detectedName.Substring(1).ToLower() : "";
                    string properName = first + rest;
                    _memory["name"] = properName;
                    return $"Thank you, {properName}. How can I help you with cybersecurity today?";
                }
                else
                {
                    return "Hello. I am Sypher AI. What is your name?";
                }
            }

            // Explicit name setting
            if (input.Contains("my name is") || input.Contains("call me"))
            {
                string[] parts = userInput.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < parts.Length; i++)
                {
                    if (parts[i].ToLower() == "is" && i + 1 < parts.Length)
                    {
                        string extractedName = parts[i + 1];
                        char first = char.ToUpper(extractedName[0]);
                        string rest = extractedName.Length > 1 ? extractedName.Substring(1).ToLower() : "";
                        _memory["name"] = first + rest;
                        return $"Nice to meet you, {_memory["name"]}. How can I assist with cybersecurity today?";
                    }
                    else if (parts[i].ToLower() == "me" && i + 1 < parts.Length)
                    {
                        string extractedName = parts[i + 1];
                        char first = char.ToUpper(extractedName[0]);
                        string rest = extractedName.Length > 1 ? extractedName.Substring(1).ToLower() : "";
                        _memory["name"] = first + rest;
                        return $"Got it, {_memory["name"]}. What would you like to know about cybersecurity?";
                    }
                }
            }

            // WHAT IS MY NAME - recall
            if (input.Contains("what is my name") || input.Contains("what's my name") || input.Contains("do you know my name"))
            {
                if (_memory.ContainsKey("name"))
                    return $"Your name is {_memory["name"]}. I have remembered it from our conversation.";
                else
                    return "I do not know your name yet. Please tell me what I should call you.";
            }

            // WHAT IS MY FAVORITE TOPIC - recall
            if (input.Contains("favorite topic") || input.Contains("favourite topic") || input.Contains("what topic do i like"))
            {
                if (_memory.ContainsKey("favoritetopic"))
                {
                    string topic = _memory["favoritetopic"];
                    string article = (topic == "2fa" || topic == "vpn") ? "an" : "a";
                    return $"You told me your favorite topic is {article} {topic}. Would you like me to share more tips about {topic}?";
                }
                else
                {
                    return "You have not told me your favorite cybersecurity topic yet. You can say something like 'My favorite topic is privacy' or 'I am interested in passwords'.";
                }
            }

            // SET FAVORITE TOPIC - only when user EXPLICITLY says they are interested or it is their favorite
            if (input.Contains("my favorite topic is") || input.Contains("my favourite topic is") || input.Contains("i am interested in") || input.Contains("i'm interested in"))
            {
                string topic = DetectTopic(input);
                if (!string.IsNullOrEmpty(topic))
                {
                    RememberInfo("favoritetopic", topic);
                    string article = (topic == "2fa" || topic == "vpn") ? "an" : "a";
                    return $"Great! I will remember that your favorite cybersecurity topic is {article} {topic}. {GetRandomKeywordResponse(topic)}";
                }
                else
                {
                    return "What topic are you interested in? You can choose from passwords, scams, privacy, phishing, malware, 2FA, VPNs, or updates.";
                }
            }

            // HOW ARE YOU
            if (input.Contains("how are you"))
            {
                return $"I am doing well, {name}. Thank you for asking. I am here to help you with cybersecurity. How are you today?";
            }

            // WHO ARE YOU / WHAT IS YOUR PURPOSE
            if (input.Contains("who are you") || input.Contains("what is your purpose") || input.Contains("what are you"))
            {
                return $"I am Sypher AI, your cybersecurity awareness assistant. My purpose is to help you understand online threats and stay safe. I can give you tips on passwords, scams, privacy, phishing, malware, 2FA, VPNs, and software updates.";
            }

            // Sentiment: worried / scared
            if (input.Contains("worried") || input.Contains("scared") || input.Contains("afraid") || input.Contains("nervous") || input.Contains("anxious"))
            {
                string topicToSuggest = DetectTopic(input);
                string tip = "";
                if (!string.IsNullOrEmpty(topicToSuggest))
                    tip = " " + GetRandomKeywordResponse(topicToSuggest);
                else
                    tip = " Let me share something helpful. " + GetRandomKeywordResponse("scam");

                return $"It is completely understandable to feel that way, {name}. Cybersecurity can seem overwhelming at first.{tip}";
            }

            // Sentiment: frustrated
            if (input.Contains("frustrated") || input.Contains("confused") || input.Contains("overwhelmed"))
            {
                string topicToSuggest = DetectTopic(input);
                string tip = "";
                if (!string.IsNullOrEmpty(topicToSuggest))
                    tip = " " + GetRandomKeywordResponse(topicToSuggest);
                else
                    tip = " Let me help you take this step by step. " + GetRandomKeywordResponse("password");

                return $"I hear your frustration, {name}. That is completely fair. Let us slow down and focus on one thing at a time.{tip}";
            }

            // Sentiment: curious (but NOT setting favorite topic)
            if (input.Contains("curious") && !input.Contains("interested in"))
            {
                return $"I love that you are curious, {name}. Would you like to learn about passwords, scams, privacy, phishing, malware, 2FA, VPNs, or software updates? Just tell me what topic.";
            }

            // Sentiment: happy
            if (input.Contains("happy") || input.Contains("feeling good") || input.Contains("great") || input.Contains("wonderful"))
            {
                return $"I am glad to hear you are feeling positive, {name}. Staying confident helps you make better security decisions. Would you like a cybersecurity tip today?";
            }

            // KEYWORD RECOGNITION - for normal questions like "tell me about password safety"
            // This does NOT set favorite topic
            string detectedTopic = DetectTopic(input);
            if (!string.IsNullOrEmpty(detectedTopic))
            {
                return GetRandomKeywordResponse(detectedTopic);
            }

            // RANDOM RESPONSES for phishing (special case)
            if (input.Contains("phishing tip") || input.Contains("phish tip"))
            {
                List<string> phishingTips = _keywordResponses["phishing"];
                int index = _random.Next(phishingTips.Count);
                return phishingTips[index];
            }

            // THANK YOU
            if (input.Contains("thank"))
            {
                return $"You are very welcome, {name}. Stay safe online. Is there anything else I can help you with?";
            }

            // GOODBYE
            if (input.Contains("bye") || input.Contains("goodbye") || input.Contains("exit") || input.Contains("quit"))
            {
                return $"Goodbye, {name}. Remember to stay vigilant online. You can always come back if you have more questions.";
            }

            // HELP
            if (input.Contains("help") || input.Contains("what can you do") || input.Contains("what do you know"))
            {
                return $"I can help you with passwords, online scams, privacy protection, phishing attacks, malware, two-factor authentication, VPNs, and software updates. What would you like to know, {name}?";
            }

            // DEFAULT / ERROR HANDLING
            return $"I am not sure I understand, {name}. Could you try rephrasing? You can ask me about passwords, scams, privacy, phishing, malware, 2FA, VPNs, or updates.";
        }

        private string DetectTopic(string input)
        {
            if (input.Contains("password"))
                return "password";
            if (input.Contains("scam") || input.Contains("fraud"))
                return "scam";
            if (input.Contains("privacy") || input.Contains("personal data"))
                return "privacy";
            if (input.Contains("phish"))
                return "phishing";
            if (input.Contains("malware") || input.Contains("virus"))
                return "malware";
            if (input.Contains("2fa") || input.Contains("two factor") || input.Contains("authenticator"))
                return "2fa";
            if (input.Contains("vpn"))
                return "vpn";
            if (input.Contains("update") || input.Contains("patch"))
                return "update";
            return "";
        }

        private string GetRandomKeywordResponse(string topic)
        {
            if (_keywordResponses.ContainsKey(topic))
            {
                List<string> responses = _keywordResponses[topic];
                int index = _random.Next(responses.Count);
                return responses[index];
            }
            return "Let me know more about what you are concerned with, and I will give you practical advice.";
        }

        public string GetGreeting()
        {
            return "Hello. I am Sypher AI, your cybersecurity awareness chatbot. I can help you with passwords, scams, privacy, phishing, malware, 2FA, VPNs, and updates. What is your name?";
        }

        public void ClearMemory()
        {
            _memory.Clear();
        }
    }
}