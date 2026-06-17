using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using SypherUI.Models;
using SypherUI.Services;


namespace SypherUI
{
    public partial class MainWindow : Window
    {
        // Existing Part 1 & 2 objects
        private ChatBot _chatBot;
        private ResponseHandler _responseHandler;

        // New Part 3 objects
        private TaskManager _taskManager;
        private QuizManager _quizManager;
        private ActivityLogger _logger;
        private int _selectedTaskId = -1;

        // Placeholder constants for Task Assistant
        private const string TaskTitlePlaceholder = "Enter task title...";
        private const string TaskDescPlaceholder = "Enter description...";
        private const string TaskReminderPlaceholder = "Enter reminder (e.g., in 3 days)";

        public MainWindow()
        {
            InitializeComponent();

            // Existing initialisations
            _chatBot = new ChatBot();
            _responseHandler = new ResponseHandler(_chatBot);
            UIAssist.Initialize(ChatItemsControl, ChatScrollViewer);

            // New Part 3 initialisations
            _taskManager = new TaskManager();
            _quizManager = new QuizManager();
            _logger = ActivityLogger.Instance;

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            AudioPlayer.PlayGreeting();

            UIAssist.AddBotMessage(
                "Hello. I am Sypher AI, your cybersecurity awareness chatbot. " +
                "I can help you with passwords, scams, privacy, phishing, malware, 2FA, VPNs, and updates. What is your name?");

            // Load tasks from database on startup
            UpdateTaskList();
        }

        // Event Handlers for existing UI
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender == nameSendBtn)
                HandleNameSubmit();
            else if (sender == txtSendBtn)
                SendUserMessage();
        }

        private void HandleNameSubmit()
        {
            string name = nameinput.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter your name.", "Sypher AI");
                return;
            }

            _chatBot.RememberInfo("name", name);
            usernameLabel.Text = name;

            UIAssist.AddBotMessage($"Thank you, {name}! How can I help you with cybersecurity today?");

            nameinput.IsEnabled = false;
            nameSendBtn.IsEnabled = false;
        }

        private void SendUserMessage()
        {
            string message = txtInput.Text.Trim();
            if (string.IsNullOrEmpty(message) || message == "Type your message...")
                return;

            UIAssist.AddUserMessage(message);

            // Process input with NLP integration (Part 3)
            string response = ProcessUserInput(message);
            UIAssist.AddBotMessage(response);

            txtInput.Clear();
        }

        private void MessageInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendUserMessage();
                e.Handled = true;
            }
        }

        private void NewChat_Click(object sender, RoutedEventArgs e)
        {
            _chatBot.ClearMemory();
            UIAssist.ClearChat();
            nameinput.IsEnabled = true;
            nameSendBtn.IsEnabled = true;
            nameinput.Clear();
            usernameLabel.Text = "Username";
            UIAssist.AddBotMessage("New conversation started. What is your name?");
        }

        private void RemovePlaceholder(object sender, RoutedEventArgs e)
        {
            if (txtInput.Text == "Type your message...")
            {
                txtInput.Text = "";
                txtInput.Foreground = new SolidColorBrush(Color.FromRgb(26, 32, 44));
            }
        }

        private void AddPlaceholder(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtInput.Text))
            {
                txtInput.Text = "Type your message...";
                txtInput.Foreground = new SolidColorBrush(Color.FromRgb(160, 174, 192));
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            AudioPlayer.StopGreeting();
            base.OnClosed(e);
        }

        // Task Assistant Placeholder Handlers
        private void TaskTitle_GotFocus(object sender, RoutedEventArgs e)
        {
            var tb = sender as TextBox;
            if (tb != null && tb.Text == TaskTitlePlaceholder)
            {
                tb.Text = "";
                tb.Foreground = new SolidColorBrush(Color.FromRgb(26, 32, 44));
            }
        }

        private void TaskTitle_LostFocus(object sender, RoutedEventArgs e)
        {
            var tb = sender as TextBox;
            if (tb != null && string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = TaskTitlePlaceholder;
                tb.Foreground = new SolidColorBrush(Color.FromRgb(160, 174, 192));
            }
        }

        private void TaskDesc_GotFocus(object sender, RoutedEventArgs e)
        {
            var tb = sender as TextBox;
            if (tb != null && tb.Text == TaskDescPlaceholder)
            {
                tb.Text = "";
                tb.Foreground = new SolidColorBrush(Color.FromRgb(26, 32, 44));
            }
        }

        private void TaskDesc_LostFocus(object sender, RoutedEventArgs e)
        {
            var tb = sender as TextBox;
            if (tb != null && string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = TaskDescPlaceholder;
                tb.Foreground = new SolidColorBrush(Color.FromRgb(160, 174, 192));
            }
        }

        private void TaskReminder_GotFocus(object sender, RoutedEventArgs e)
        {
            var tb = sender as TextBox;
            if (tb != null && tb.Text == TaskReminderPlaceholder)
            {
                tb.Text = "";
                tb.Foreground = new SolidColorBrush(Color.FromRgb(26, 32, 44));
            }
        }

        private void TaskReminder_LostFocus(object sender, RoutedEventArgs e)
        {
            var tb = sender as TextBox;
            if (tb != null && string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = TaskReminderPlaceholder;
                tb.Foreground = new SolidColorBrush(Color.FromRgb(160, 174, 192));
            }
        }

        // NEW PART 3: Task Assistant
        private void AddTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            string title = TaskTitleBox.Text.Trim();
            if (title == TaskTitlePlaceholder || string.IsNullOrEmpty(title))
            {
                MessageBox.Show("Please enter a task title.", "Sypher AI");
                return;
            }
            string desc = TaskDescBox.Text.Trim();
            if (desc == TaskDescPlaceholder) desc = "";
            string reminder = TaskReminderBox.Text.Trim();
            if (reminder == TaskReminderPlaceholder) reminder = "";

            string msg = _taskManager.AddTask(title, desc, reminder);
            UpdateTaskList();
            UIAssist.AddBotMessage($"Bot: {msg}");

            // placeholders
            TaskTitleBox.Text = TaskTitlePlaceholder;
            TaskTitleBox.Foreground = new SolidColorBrush(Color.FromRgb(160, 174, 192));
            TaskDescBox.Text = TaskDescPlaceholder;
            TaskDescBox.Foreground = new SolidColorBrush(Color.FromRgb(160, 174, 192));
            TaskReminderBox.Text = TaskReminderPlaceholder;
            TaskReminderBox.Foreground = new SolidColorBrush(Color.FromRgb(160, 174, 192));
        }

        private void UpdateTaskList()
        {
            var tasks = _taskManager.GetAllTasks();
            TaskListView.ItemsSource = tasks;
        }

        private void TaskListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TaskListView.SelectedItem is Models.Task selected)
                _selectedTaskId = selected.Id;
        }

        private void CompleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedTaskId == -1)
            {
                MessageBox.Show("Select a task first.");
                return;
            }
            string msg = _taskManager.MarkAsComplete(_selectedTaskId);
            UIAssist.AddBotMessage($"Bot: {msg}");
            UpdateTaskList();
            _selectedTaskId = -1;
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedTaskId == -1)
            {
                MessageBox.Show("Select a task first.");
                return;
            }
            if (MessageBox.Show("Delete this task?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                string msg = _taskManager.DeleteTask(_selectedTaskId);
                UIAssist.AddBotMessage($"Bot: {msg}");
                UpdateTaskList();
                _selectedTaskId = -1;
            }
        }

        // NEW PART 3: Quiz
        private void StartQuiz()
        {
            _quizManager.ResetQuiz();
            _quizManager.LogQuizStart();
            ShowCurrentQuizQuestion();
            QuizStartBtn.IsEnabled = false;
            QuizSubmitBtn.IsEnabled = true;
            QuizNextBtn.IsEnabled = false;
            QuizFeedbackText.Text = "";
        }

        private void QuizStartBtn_Click(object sender, RoutedEventArgs e)
        {
            StartQuiz();
        }

        private void ShowCurrentQuizQuestion()
        {
            var q = _quizManager.GetCurrentQuestion();
            if (q == null)
            {
                // Quiz finished
                QuizSubmitBtn.IsEnabled = false;
                QuizNextBtn.IsEnabled = false;
                int score = _quizManager.GetScore();
                int total = _quizManager.GetTotal();
                QuizQuestionText.Text = $"🏁 Quiz finished! Your score: {score}/{total}";
                QuizFeedbackText.Text = _quizManager.GetFinalMessage();
                QuizScoreText.Text = $"Final Score: {score}/{total}";
                _quizManager.LogQuizEnd();
                return;
            }

            QuizQuestionText.Text = q.Question;
            // Build option buttons dynamically
            var panel = QuizOptionsControl;
            panel.ItemsSource = null;
            List<RadioButton> options = new();
            for (int i = 0; i < q.Options.Count; i++)
            {
                var rb = new RadioButton
                {
                    Content = q.Options[i],
                    Tag = ((char)('A' + i)).ToString(),
                    Margin = new Thickness(0, 2, 0, 2),
                    Foreground = new SolidColorBrush(Color.FromRgb(26, 32, 44))
                };
                options.Add(rb);
            }
            panel.ItemsSource = options;
            QuizSubmitBtn.IsEnabled = true;
            QuizNextBtn.IsEnabled = false;
            QuizFeedbackText.Text = "";
            QuizScoreText.Text = $"Score: {_quizManager.GetScore()} / {_quizManager.GetTotal()}";
        }

        private void QuizSubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            var panel = QuizOptionsControl;
            string selected = null;
            foreach (RadioButton rb in panel.Items)
            {
                if (rb.IsChecked == true)
                {
                    selected = rb.Tag.ToString();
                    break;
                }
            }
            if (string.IsNullOrEmpty(selected))
            {
                MessageBox.Show("Please select an answer first.");
                return;
            }

            bool correct = _quizManager.SubmitAnswer(selected);
            string feedback = _quizManager.GetFeedback(correct);
            QuizFeedbackText.Text = feedback;
            QuizSubmitBtn.IsEnabled = false;
            QuizNextBtn.IsEnabled = true;
            QuizScoreText.Text = $"Score: {_quizManager.GetScore()} / {_quizManager.GetTotal()}";
        }

        private void QuizNextBtn_Click(object sender, RoutedEventArgs e)
        {
            ShowCurrentQuizQuestion();
            QuizNextBtn.IsEnabled = false;
        }

        // NEW PART 3: NLP Processing
        private string ProcessUserInput(string input)
        {
            string lower = input.ToLower();

            // 1. Activity Log
            if (lower.Contains("show activity log") || lower.Contains("what have you done") ||
                lower.Contains("show log") || lower.Contains("recent actions"))
            {
                var logs = ActivityLogger.Instance.GetRecentLogs(10);
                string formatted = ActivityLogger.Instance.FormatLogs(logs);
                if (ActivityLogger.Instance.GetAllLogs().Count > 10)
                    formatted += "\n\n(There are more entries. Type 'show more' to see all.)";
                return "Here's a summary of recent actions:\n" + formatted;
            }
            if (lower.Contains("show more") && !lower.Contains("task") && !lower.Contains("quiz"))
            {
                var logs = ActivityLogger.Instance.GetAllLogs();
                return "Full activity log:\n" + ActivityLogger.Instance.FormatLogs(logs);
            }

            // 2. Start Quiz
            if (lower.Contains("start quiz") || lower.Contains("take quiz") ||
                lower.Contains("quiz me") || lower.Contains("test my knowledge"))
            {
                Dispatcher.Invoke(() =>
                {
                    if (this.FindName("MainTabControl") is TabControl tabControl)
                    {
                        tabControl.SelectedIndex = 2; // Quiz tab (index 2)
                    }
                    StartQuiz();
                });
                return "Alright! Starting the cybersecurity quiz. Go to the Quiz tab to answer.";
            }

            // 3. Add Task
            if (lower.Contains("add task") || lower.Contains("add a task") || lower.Contains("create task") ||
                lower.Contains("enable") || lower.Contains("set up"))
            {
                string title = ExtractTaskTitle(input);
                if (string.IsNullOrEmpty(title))
                    return "I couldn't understand the task. Please use format: 'Add task - <title>'";
                string msg = _taskManager.AddTask(title, "", "");
                UpdateTaskList();
                return msg + " Would you like to set a reminder? (e.g., 'remind me in 3 days')";
            }

            // 4. Set Reminder (standalone)
            if (lower.Contains("remind me") || lower.Contains("set reminder") || lower.Contains("remind in") ||
                lower.Contains("don't forget"))
            {
                string reminderTopic = ExtractReminderTopic(input);
                string time = ExtractTime(input);
                if (!string.IsNullOrEmpty(reminderTopic))
                {
                    string msg = _taskManager.AddTask(reminderTopic, "", time);
                    UpdateTaskList();
                    return msg;
                }
                else
                {
                    return "I'll set a reminder. Please tell me what to remind you about and when.";
                }
            }

            // 5. Fall back to existing Part 2 logic (keyword, sentiment, memory)
            return _responseHandler.GetFinalResponse(input);
        }

        // Helper extraction methods for NLP
        private string ExtractTaskTitle(string input)
        {
            string lower = input.ToLower();
            string result = input;
            foreach (var prefix in new[] { "add task ", "add a task ", "create task ", "enable ", "set up " })
            {
                if (lower.Contains(prefix))
                {
                    result = input.Substring(input.IndexOf(prefix) + prefix.Length);
                    break;
                }
            }
            return result.Trim(' ', '-', '.');
        }

        private string ExtractReminderTopic(string input)
        {
            string lower = input.ToLower();
            if (lower.Contains("remind me to"))
                return input.Substring(input.IndexOf("remind me to") + "remind me to".Length).Trim();
            if (lower.Contains("remind me about"))
                return input.Substring(input.IndexOf("remind me about") + "remind me about".Length).Trim();
            if (lower.Contains("set reminder for"))
                return input.Substring(input.IndexOf("set reminder for") + "set reminder for".Length).Trim();
            if (lower.Contains("don't forget to"))
                return input.Substring(input.IndexOf("don't forget to") + "don't forget to".Length).Trim();
            return input;
        }

        private string ExtractTime(string input)
        {
            string lower = input.ToLower();
            if (lower.Contains("tomorrow")) return "tomorrow";
            if (lower.Contains("in "))
            {
                var parts = lower.Split(new[] { " in " }, StringSplitOptions.None);
                if (parts.Length > 1)
                {
                    string[] words = parts[1].Split(' ');
                    if (words.Length >= 2 && words[1] == "days")
                        return $"in {words[0]} days";
                    else if (words.Length >= 2 && words[1] == "day")
                        return "tomorrow";
                }
            }
            return "soon";
        }
    }
}