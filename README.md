# Sypher - Cybersecurity Chatbot

A C# console chatbot that teaches cybersecurity basics. Features voice greeting, ASCII art, personalized responses, and a typing effect.

## Topics Covered
- Strong passwords
- Phishing emails
- Malware / viruses
- Two-factor authentication (2FA)
- VPNs
- Software updates

## File Structure
- `Program.cs` – Main loop and UI
- `ChatBot.cs` – Response logic
- `User.cs` – User name storage
- `AudioPlayer.cs` – Voice greeting

## CI
Approved. Green tick. Screenshot will be provided. 

## Author
Karlia Robson ST10485655


# Sypher AI - Cybersecurity Awareness Chatbot

## Project Overview
Sypher AI is a Windows Presentation Foundation (WPF) application designed to educate users about cybersecurity through an interactive chatbot.

---

## Part 1 → Part 2 Changes

### What was added / improved in **Part 2**:

- **Full GUI Implementation** using WPF with modern, clean design (sidebar, chat area, name input, ASCII art banner).
- **Dynamic Chat Interface** with message bubbles using `ItemsControl` and data binding.
- **AudioPlayer.cs** - Carried over from Part 1 with improvements for automatic greeting playback on startup.
- **ChatBot.cs** - Completely rebuilt with:
  - Advanced keyword recognition (password, scam, phishing, privacy, malware, 2FA, VPN, updates)
  - Sentiment detection (worried, frustrated, curious)
  - Memory system (remembers user's name and favorite topic)
  - More natural and engaging conversation flow
- **UIAssist.cs** - New helper class to manage chat messages and scrolling.
- **Converters.cs** - Custom converters for proper message alignment and styling in the chat.
- **ResponseHandler.cs** - Separates response logic for better code organization.
- Improved error handling and default responses.
- Voice greeting now plays automatically when the application starts.

### Key Features Implemented (Part 2)
- Modern, user-friendly GUI with proper spacing and color contrast
- Real-time sentiment detection + empathetic responses
- Memory & personalization (name + favorite topic)
- Random varied responses for each topic
- ASCII art displayed in GUI
- Voice greeting (greeting.wav)

---

## Files Added / Modified

- `MainWindow.xaml` & `MainWindow.xaml.cs` → Full GUI
- `ChatBot.cs` → Main chatbot logic
- `AudioPlayer.cs` → Voice greeting
- `UIAssist.cs` → Chat management
- `Converters.cs` → Message styling
- `ResponseHandler.cs` → Response processing
- `User.cs` → User model

---

## How to Run
1. Open the solution in Visual Studio
2. Make sure `greeting.wav` and `sypher ascii.png` are in the project with "Copy to Output Directory" set to `Copy always`
3. Build and run

---

## Releases

- **v1.0** - Console-based chatbot (Part 1)
- **v2.0** - Full WPF GUI with memory, sentiment detection, and improved responses (Part 2)

