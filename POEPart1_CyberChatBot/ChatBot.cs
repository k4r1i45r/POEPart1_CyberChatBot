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

            // Questions about the bot itself (check these FIRST)
            if (lowerInput.Contains("how is your response") ||
                lowerInput.Contains("how are your response") ||
                lowerInput.Contains("how do you respond"))
            {
                return $"I respond based on keywords, {userName}. Ask me about passwords, phishing, malware, or safe browsing.";
            }

            if (lowerInput.Contains("what is your purpose") ||
                lowerInput.Contains("what do you do"))
            {
                return $"My purpose is to help you stay safe online, {userName}. I can answer basic cybersecurity questions.";
            }

            // Greeting responses (check AFTER bot questions)
            if (lowerInput.Contains("how are you"))
            {
                return $"I am functioning well, {userName}. Ready to help you with cybersecurity.";
            }

            // Cybersecurity questions
            if (lowerInput.Contains("password"))
            {
                return $"A strong password should be at least 12 characters, {userName}. Use uppercase, lowercase, numbers, and symbols. Never reuse passwords.";
            }

            if (lowerInput.Contains("phishing"))
            {
                return $"Phishing is when scammers pretend to be legitimate companies to steal your info, {userName}. Never click suspicious links or share personal details via email.";
            }

            if (lowerInput.Contains("malware") || lowerInput.Contains("virus"))
            {
                return $"Malware is malicious software. Protect yourself by keeping your system updated, {userName}, and avoid downloading from untrusted sources.";
            }

            if (lowerInput.Contains("two factor") || lowerInput.Contains("2fa") || lowerInput.Contains("mfa"))
            {
                return $"Two-factor authentication adds an extra layer of security, {userName}. Always enable it when available to protect your accounts.";
            }

            if (lowerInput.Contains("vpn"))
            {
                return $"A VPN encrypts your internet traffic, {userName}. It helps protect your privacy, especially on public Wi-Fi.";
            }

            if (lowerInput.Contains("update") || lowerInput.Contains("patch"))
            {
                return $"Regular updates fix security vulnerabilities, {userName}. Always install updates promptly for your operating system and apps.";
            }

            // Default response for unknown questions
            return $"That's a good question, {userName}. I'm still learning. Can you ask me about passwords, phishing, malware, 2FA, or VPNs?";
        }
    }
}