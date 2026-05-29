using System;

namespace SypherUI
{
    public class ResponseHandler
    {
        private readonly ChatBot _chatBot;
        private Func<string, string, string, string> _responseProcessor;

        public ResponseHandler(ChatBot chatBot)
        {
            _chatBot = chatBot;
        }

        public void SetResponseProcessor(Func<string, string, string, string> processor)
        {
            _responseProcessor = processor;
        }

        public string GetFinalResponse(string userInput)
        {
            string rawResponse = _chatBot.GetResponse(userInput);
            string sentiment = DetectSentiment(userInput);

            return _responseProcessor?.Invoke(rawResponse, userInput, sentiment) ?? rawResponse;
        }

        private string DetectSentiment(string input)
        {
            string lower = input.ToLower();
            if (lower.Contains("worried") || lower.Contains("scared")) return "worried";
            if (lower.Contains("frustrated") || lower.Contains("annoyed")) return "frustrated";
            return "neutral";
        }
    }
}
