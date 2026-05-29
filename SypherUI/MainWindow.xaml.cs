using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SypherUI
{
    public partial class MainWindow : Window
    {
        private ChatBot _chatBot;
        private ResponseHandler _responseHandler;

        public MainWindow()
        {
            InitializeComponent();
            _chatBot = new ChatBot();
            _responseHandler = new ResponseHandler(_chatBot);
            UIAssist.Initialize(ChatItemsControl, ChatScrollViewer);

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            AudioPlayer.PlayGreeting();

            UIAssist.AddBotMessage(
                "Hello. I am Sypher AI, your cybersecurity awareness chatbot. " +
                "I can help you with passwords, scams, privacy, phishing, malware, 2FA, VPNs, and updates. What is your name?");
        }

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
            string response = _responseHandler.GetFinalResponse(message);
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
    }
}