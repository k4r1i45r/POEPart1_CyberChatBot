using System;
using System.Collections.Generic;
using System.Linq;

namespace SypherUI
{
    public class ChatBot
    {
        private Dictionary<string, string> _memory = new Dictionary<string, string>();
        private Random _random = new Random();

        private Dictionary<string, List<string>> _keywordResponses;
        private Dictionary<string, List<string>> _randomResponseSets;

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
                        "Software updates often include important security patches for known vulnerabilities.",
                        "Enable automatic updates on your operating system, browser, and apps when possible.",
                        "Outdated software is one of the most common ways hackers gain access to devices.",
                        "Do not ignore update reminders. Set aside time to install them regularly."
                    }
                }
            };

            _randomResponseSets = new Dictionary<string, List<string>>()
            {
                { "phishing_tips", new List<string>()
                    {
                        "Phishing emails often have spelling mistakes. Legitimate companies usually proofread their messages.",
                        "If an email asks you to click a link to verify your account, go to the website manually instead.",
                        "Scammers sometimes call pretending to be from your bank. Hang up and call the number on your card.",
                        "Be wary of messages that say your account will be closed unless you act immediately."
                    }
                },
                { "greetings", new List<string>()
                    {
                        "Hello. I am Sypher AI, your cybersecurity awareness chatbot. I can help you with passwords, scams, privacy, phishing, malware, 2FA, VPNs, and updates. What is your name?",
                        "Welcome. I am Sypher AI. I provide guidance on staying safe online. What is your name?"
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

            if (!_memory.ContainsKey("name") && !string.IsNullOrWhiteSpace(input))
            {
                bool likelyName = !input.Contains(" ") && input.Length < 20 && !input.Contains("?") && !input.Contains("help") && !input.Contains("what") && !input.Contains("how");
                if (likelyName || (input.Length > 1 && input.Length < 15))
                {
                    string detectedName = userInput.Trim();
                    if (detectedName.Length > 0)
                    {
                        char first = char.ToUpper(detectedName[0]);
                        string rest = detectedName.Length > 1 ? detectedName.Substring(1).ToLower() : "";
                        string properName = first + rest;
                        _memory["name"] = properName;
                        return $"Thank you, {properName}. How can I help you with cybersecurity today?";
                    }
                }
            }

            if (input.Contains("my name is") || input.Contains("call me"))
            {
                string[] parts = userInput.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < parts.Length; i++)
                {
                    if (parts[i].ToLower() == "is" && i + 1 < parts.Length)
                    {
                        string extractedName = parts[i + 1];
                        if (extractedName.Length > 0)
                        {
                            char first = char.ToUpper(extractedName[0]);
                            string rest = extractedName.Length > 1 ? extractedName.Substring(1).ToLower() : "";
                            _memory["name"] = first + rest;
                            return $"Nice to meet you, {_memory["name"]}. How can I assist with cybersecurity today?";
                        }
                    }
                    else if (parts[i].ToLower() == "me" && i + 1 < parts.Length)
                    {
                        string extractedName = parts[i + 1];
                        if (extractedName.Length > 0)
                        {
                            char first = char.ToUpper(extractedName[0]);
                            string rest = extractedName.Length > 1 ? extractedName.Substring(1).ToLower() : "";
                            _memory["name"] = first + rest;
                            return $"Got it, {_memory["name"]}. What would you like to know about cybersecurity?";
                        }
                    }
                }
            }

            if (input.Contains("what is my name") || input.Contains("what's my name") || input.Contains("do you know my name") || input.Contains("remember my name"))
            {
                if (_memory.ContainsKey("name"))
                {
                    return $"Your name is {_memory["name"]}. I have remembered it from our conversation.";
                }
                else
                {
                    return "I do not know your name yet. Could you please tell me what I should call you?";
                }
            }

            if (input.Contains("favorite topic") || input.Contains("favourite topic") || input.Contains("my favorite") || input.Contains("what topic do I like") || input.Contains("what am I interested in"))
            {
                if (_memory.ContainsKey("favoritetopic"))
                {
                    string topic = _memory["favoritetopic"];
                    string article = (topic == "2fa" || topic == "vpn") ? "an" : "a";
                    return $"You told me you are interested in {article} {topic} topic. Would you like me to share more tips about {topic}?";
                }
                else
                {
                    return "You have not told me your favorite cybersecurity topic yet. You can say something like I am interested in privacy or tell me about passwords.";
                }
            }

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

            if (input.Contains("curious") || input.Contains("interested in learning") || input.Contains("tell me about"))
            {
                string topic = DetectTopic(input);
                if (!string.IsNullOrEmpty(topic))
                {
                    string article = (topic == "2fa" || topic == "vpn") ? "an" : "a";
                    RememberInfo("favoritetopic", topic);
                    return $"That is great to hear, {name}. I will remember that you are interested in {article} {topic} topic. " + GetRandomKeywordResponse(topic);
                }
                else
                {
                    return $"I love that you are curious, {name}. Would you like to learn about passwords, scams, privacy, phishing, malware, 2FA, VPNs, or software updates?";
                }
            }

            if (input.Contains("happy") || input.Contains("feeling good") || input.Contains("great") || input.Contains("wonderful"))
            {
                return $"I am glad to hear you are feeling positive, {name}. Staying confident helps you make better security decisions. Would you like a cybersecurity tip today?";
            }

            string detectedTopic = DetectTopic(input);
            if (!string.IsNullOrEmpty(detectedTopic))
            {
                if (!_memory.ContainsKey("favoritetopic") && detectedTopic != "")
                {
                    string article = (detectedTopic == "2fa" || detectedTopic == "vpn") ? "an" : "a";
                    _memory["recenttopic"] = detectedTopic;
                    return $"Great question about {detectedTopic}. " + GetRandomKeywordResponse(detectedTopic);
                }
                else
                {
                    return GetRandomKeywordResponse(detectedTopic);
                }
            }

            if (input.Contains("thank"))
            {
                return $"You are very welcome, {name}. Stay safe online. Is there anything else I can help you with today?";
            }

            if (input.Contains("bye") || input.Contains("goodbye") || input.Contains("exit") || input.Contains("quit"))
            {
                return $"Goodbye, {name}. Remember to stay vigilant online. You can always come back if you have more questions.";
            }

            if (input.Contains("help") || input.Contains("what can you do") || input.Contains("what do you know"))
            {
                return $"I can help you with passwords, online scams, privacy protection, phishing attacks, malware, two-factor authentication, VPNs, and software updates. What would you like to know, {name}?";
            }

            return $"I am not sure I understand, {name}. Could you try rephrasing? You can ask me about passwords, scams, privacy, phishing, malware, 2FA, VPNs, or updates.";
        }

        private string DetectTopic(string input)
        {
            if (input.Contains("password") || input.Contains("passphrase") || input.Contains("login"))
                return "password";
            if (input.Contains("scam") || input.Contains("fraud") || input.Contains("fake"))
                return "scam";
            if (input.Contains("privacy") || input.Contains("private") || input.Contains("personal data") || input.Contains("personal information"))
                return "privacy";
            if (input.Contains("phish") || input.Contains("phishing"))
                return "phishing";
            if (input.Contains("malware") || input.Contains("virus") || input.Contains("trojan") || input.Contains("ransomware"))
                return "malware";
            if (input.Contains("2fa") || input.Contains("two factor") || input.Contains("two-factor") || input.Contains("authenticator") || input.Contains("mfa"))
                return "2fa";
            if (input.Contains("vpn") || input.Contains("virtual private network"))
                return "vpn";
            if (input.Contains("update") || input.Contains("patch") || input.Contains("software update"))
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

        public string GetRandomPhishingTip()
        {
            List<string> tips = _randomResponseSets["phishing_tips"];
            int index = _random.Next(tips.Count);
            return tips[index];
        }

        public string GetGreeting()
        {
            List<string> greetings = _randomResponseSets["greetings"];
            int index = _random.Next(greetings.Count);
            return greetings[index];
        }

        public void ClearMemory()
        {
            _memory.Clear();
        }

        public string RecallInfo(string key)
        {
            if (_memory.ContainsKey(key))
                return _memory[key];
            return "";
        }
    }
}