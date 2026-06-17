using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using SypherUI.Models;

namespace SypherUI.Services
{
    public class QuizManager
    {
        private List<QuizQuestion> _questions;
        private int _currentIndex = 0;
        private int _score = 0;
        private readonly ActivityLogger _logger = ActivityLogger.Instance;

        public QuizManager()
        {
            _questions = LoadQuestions();
        }

        private List<QuizQuestion> LoadQuestions()
        {
            return new List<QuizQuestion>
            {
                new QuizQuestion
                {
                    Question = "What should you do if you receive an email asking for your password?",
                    Options = new List<string> { "Reply with your password", "Delete the email", "Report the email as phishing", "Ignore it" },
                    CorrectAnswer = "C",
                    Explanation = "Reporting phishing emails helps prevent scams and protects your account.",
                    IsTrueFalse = false
                },
                new QuizQuestion
                {
                    Question = "Is it safe to use the same password for multiple accounts?",
                    Options = new List<string> { "True", "False" },
                    CorrectAnswer = "False",
                    Explanation = "Using the same password across multiple accounts increases risk; if one is breached, all are compromised.",
                    IsTrueFalse = true
                },
                new QuizQuestion
                {
                    Question = "Which of the following indicates a secure website connection?",
                    Options = new List<string> { "HTTP in the URL", "HTTPS and a padlock icon", "A green address bar", "A pop-up saying 'Secure'" },
                    CorrectAnswer = "B",
                    Explanation = "HTTPS and the padlock indicate the connection is encrypted and secure.",
                    IsTrueFalse = false
                },
                new QuizQuestion
                {
                    Question = "A stranger calls pretending to be from your bank and asks for your account number. This is an example of:",
                    Options = new List<string> { "Phishing", "Vishing", "Smishing", "Whaling" },
                    CorrectAnswer = "B",
                    Explanation = "Vishing is voice phishing over the phone. Never share sensitive info over unsolicited calls.",
                    IsTrueFalse = false
                },
                new QuizQuestion
                {
                    Question = "Two-factor authentication adds an extra layer of security by requiring:",
                    Options = new List<string> { "A password only", "A password and a code from your phone", "A fingerprint only", "A security question" },
                    CorrectAnswer = "B",
                    Explanation = "2FA combines something you know (password) with something you have (phone) for stronger security.",
                    IsTrueFalse = false
                },
                new QuizQuestion
                {
                    Question = "Ransomware is a type of malware that:",
                    Options = new List<string> { "Steals your passwords", "Encrypts your files and demands payment", "Deletes your browser history", "Slows down your computer" },
                    CorrectAnswer = "B",
                    Explanation = "Ransomware encrypts files and demands a ransom for the decryption key.",
                    IsTrueFalse = false
                },
                new QuizQuestion
                {
                    Question = "Is it safe to do online banking on public Wi-Fi without a VPN?",
                    Options = new List<string> { "True", "False" },
                    CorrectAnswer = "False",
                    Explanation = "Public Wi-Fi can be insecure; use a VPN or your mobile data for sensitive transactions.",
                    IsTrueFalse = true
                },
                new QuizQuestion
                {
                    Question = "What is the best practice for app privacy settings?",
                    Options = new List<string> { "Accept all permissions", "Review and limit permissions to only what's necessary", "Never use apps", "Share location always" },
                    CorrectAnswer = "B",
                    Explanation = "Reviewing permissions minimises data exposure and protects your privacy.",
                    IsTrueFalse = false
                },
                new QuizQuestion
                {
                    Question = "How often should you back up important files?",
                    Options = new List<string> { "Once a year", "Every few months", "Regularly (e.g., weekly) and before major updates", "Never" },
                    CorrectAnswer = "C",
                    Explanation = "Regular backups prevent data loss from ransomware, hardware failure, or accidents.",
                    IsTrueFalse = false
                },
                new QuizQuestion
                {
                    Question = "Which password is the most secure?",
                    Options = new List<string> { "123456", "Password1", "Tr0ub4dor&3", "qwerty" },
                    CorrectAnswer = "C",
                    Explanation = "A long, complex password with mixed characters is much harder to crack.",
                    IsTrueFalse = false
                },
                new QuizQuestion
                {
                    Question = "Social engineering attacks rely on technical hacking skills.",
                    Options = new List<string> { "True", "False" },
                    CorrectAnswer = "False",
                    Explanation = "Social engineering manipulates people into revealing information; it's psychological, not technical.",
                    IsTrueFalse = true
                }
            };
        }

        public QuizQuestion GetCurrentQuestion()
        {
            return _currentIndex < _questions.Count ? _questions[_currentIndex] : null;
        }

        public bool SubmitAnswer(string answer)
        {
            var q = GetCurrentQuestion();
            if (q == null) return false;
            bool correct = answer.Equals(q.CorrectAnswer, StringComparison.OrdinalIgnoreCase);
            if (correct) _score++;
            _currentIndex++;
            return correct;
        }

        public string GetFeedback(bool correct)
        {
            var q = _questions[_currentIndex - 1];
            return (correct ? "Correct! " : "Incorrect. ") + q.Explanation;
        }

        public bool IsFinished() => _currentIndex >= _questions.Count;

        public int GetScore() => _score;
        public int GetTotal() => _questions.Count;

        public string GetFinalMessage()
        {
            double pct = (double)_score / _questions.Count;
            if (pct >= 0.8) return "Great job! You're a cybersecurity pro!";
            if (pct >= 0.5) return "Good effort! Keep learning to stay safe online.";
            return "Keep learning! Cybersecurity is crucial for everyone.";
        }

        public void ResetQuiz()
        {
            _currentIndex = 0;
            _score = 0;
        }

        public void LogQuizStart() => _logger.Log("Quiz started.");
        public void LogQuizEnd() => _logger.Log($"Quiz completed - score: {_score} out of {_questions.Count}");
    }
}