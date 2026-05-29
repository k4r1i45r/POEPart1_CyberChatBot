using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace SypherUI
{
    public static class UIAssist
    {
        private static ItemsControl _itemsControl;
        private static ScrollViewer _scrollViewer;
        private static ObservableCollection<ChatMessage> _messages = new ObservableCollection<ChatMessage>();

        public static void Initialize(ItemsControl itemsControl, ScrollViewer scrollViewer)
        {
            _itemsControl = itemsControl;
            _scrollViewer = scrollViewer;
            _itemsControl.ItemsSource = _messages;
        }

        public static void AddUserMessage(string text)
        {
            _messages.Add(new ChatMessage { Text = text ?? "", IsUser = true });
            ScrollToBottom();
        }

        public static void AddBotMessage(string text)
        {
            _messages.Add(new ChatMessage { Text = text ?? "", IsUser = false });
            ScrollToBottom();
        }

        public static void ClearChat()
        {
            _messages.Clear();
        }

        private static void ScrollToBottom()
        {
            _scrollViewer?.ScrollToEnd();
        }
    }

    public class ChatMessage
    {
        public string Text { get; set; } = string.Empty;
        public bool IsUser { get; set; }
    }
}
