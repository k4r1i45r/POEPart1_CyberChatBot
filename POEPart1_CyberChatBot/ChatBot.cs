using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POEPart1_CyberChatBot
{
    
        internal class ChatBot
        {
            public string GetResponse(string userInput, string userName)
            {
                string lowerInput = userInput.ToLower();

                // Exit / thank you
                if (lowerInput.Contains("thank you") || lowerInput.Contains("thanks"))
                {
                    return $"You are very welcome, {userName}! Stay curious about cybersecurity. If you ever have more questions, I'll be here.";
                }
                if (lowerInput.Contains("bye") || lowerInput.Contains("goodbye") || lowerInput.Contains("quit"))
                {
                    return $"EXIT|Take care, {userName}! Remember: think before you click, and keep your passwords secret. Goodbye!";
                }

                // About the bot itself
                if (lowerInput.Contains("how is your response") || lowerInput.Contains("how are your response") || lowerInput.Contains("how do you respond"))
                {
                    return $"Great question, {userName}. I listen for keywords like 'password', 'phishing', 'malware', '2FA', or 'VPN'. Try asking me: 'What is a strong password?' or 'How do I spot phishing?'";
                }
                if (lowerInput.Contains("what is your purpose") || lowerInput.Contains("what do you do"))
                {
                    return $"I'm Sypher, your cybersecurity awareness assistant, {userName}. My job is to help you understand online threats and how to avoid them. You can ask me about passwords, phishing, malware, two-factor authentication, VPNs, and software updates.";
                }

                // Greeting
                if (lowerInput.Contains("how are you"))
                {
                    return $"I'm fully secure and ready to help, {userName}! How can I boost your cybersecurity knowledge today?";
                }

                // Cybersecurity topics 
                if (lowerInput.Contains("password"))
                {
                    return $"Passwords are like keys to your digital life, {userName}. A strong password has at least 12 characters, mixing uppercase, lowercase, numbers, and symbols. Never reuse passwords across sites, and consider using a password manager. Want tips on creating one?";
                }
                if (lowerInput.Contains("phishing"))
                {
                    return $"Phishing is when scammers pretend to be a trusted company (like your bank) to steal your info, {userName}. Never click suspicious links or download attachments from unknown emails. Always check the sender's address. Would you like an example?";
                }
                if (lowerInput.Contains("malware") || lowerInput.Contains("virus"))
                {
                    return $"Malware includes viruses, ransomware, and spyware, {userName}. Protect yourself by keeping your system updated, using antivirus software, and avoiding downloads from untrusted websites. I can explain each type if you like.";
                }
                if (lowerInput.Contains("two factor") || lowerInput.Contains("2fa") || lowerInput.Contains("mfa"))
                {
                    return $"Two-factor authentication (2FA) adds a second layer of security, {userName}. Even if someone steals your password, they'd need your phone or a special code to log in. Always enable 2FA when available – it's one of the best protections!";
                }
                if (lowerInput.Contains("vpn"))
                {
                    return $"A VPN (Virtual Private Network) encrypts your internet connection, {userName}. It hides your online activity from hackers, especially on public Wi-Fi at coffee shops or airports. Not all VPNs are equal – look for a no-log policy.";
                }
                if (lowerInput.Contains("update") || lowerInput.Contains("patch"))
                {
                    return $"Updates aren't just for new features, {userName}. They fix security holes that hackers exploit. Turn on automatic updates for your operating system, browser, and apps. It's one of the easiest ways to stay safe.";
                }

                // Default – helpful and friendly
                return $"I'm still learning, {userName}. Could you ask me about passwords, phishing, malware, 2FA, VPNs, or software updates? For example: 'What is phishing?' or 'How do I create a strong password?'";
            }
        }
    }
