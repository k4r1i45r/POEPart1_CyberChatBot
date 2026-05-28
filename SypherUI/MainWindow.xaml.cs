using System.Media;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SypherUI
{
 
    /// Interaction logic for MainWindow.xaml
    
    public partial class MainWindow : Window
    {
        private string userName = "User";

        private List<string> responses = new List<string>
        {
            "I can help with that! What specific cybersecurity concern do you have?",
            "Great question. Make sure your firewall and antivirus are up to date.",
            "I recommend enabling two-factor authentication on all accounts."
        };

        private int index = 0;

        public MainWindow()
        {
            InitializeComponent();
            SoundPlayer sound = new SoundPlayer("greeting.wav");
            //sound.Play();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button clicked = sender as Button;

            if (clicked == nameSendBtn)
            {
                HandleNameSubmit();
            }
            else if (clicked == txtSendBtn)
            {
                HandleMessageSend();
            }
        }

        private void HandleNameSubmit()
        {
            string name = nameinput.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter your name.", "Sypher AI");
                return;
            }

            userName = name;

           
            usernameLabel.Content = name;
            usernameLabelBlur.Content = name;

            // Sypher AI greets the user by name
            Response1.Content = $"Hello {name}, welcome to Sypher AI! How can I assist you today?";

            // Lock name input after submission
            nameinput.IsEnabled = false;
            nameSendBtn.IsEnabled = false;
        }

        private void HandleMessageSend()
        {
            string message = txtInput.Text.Trim();

            if (string.IsNullOrEmpty(message) || message == "Type your message...")
            {
                MessageBox.Show("Please type a message first.", "Sypher AI");
                return;
            }

            // Show user's message
            Response2.Content = message;

            // Cycle through AI responses
            Response3.Content = responses[index % responses.Count];
            index++;

            // Clear input
            txtInput.Clear();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
    }
}