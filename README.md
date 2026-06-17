# 📖 README.md – SypherAI Cybersecurity Chatbot

## Project Overview

**SypherAI** is a WPF desktop application built as a **Cybersecurity Awareness Chatbot** for the PROG6221 POE. This project is divided into three parts, each building upon the previous to create a fully functional cybersecurity awareness tool with task management, interactive learning, and intelligent conversation capabilities.

The application combines a conversational AI with productivity tools to help users learn about cybersecurity and manage their security tasks effectively.

**Key functionalities:**
- Interactive chatbot with keyword recognition, sentiment detection, and memory.
- **Task Assistant** with reminders (stored in SQLite via Entity Framework Core).
- **Cybersecurity Quiz** with 11 questions, immediate feedback, and final score.
- **Natural Language Processing (NLP) simulation** for flexible command recognition.
- **Activity Log** that records all actions and displays recent entries.

---

## Author

- **Name** - Karlia Robson
- **Student Number** - ST10485655

---

## Part 1 – Base Chatbot Implementation

Part 1 established the foundation of the cybersecurity chatbot with core conversational capabilities.

### Features Implemented:

1. **Voice Greeting**
   - A .wav audio file plays automatically when the application launches.
   - Creates an engaging and welcoming user experience.

2. **ASCII Art Display**
   - A Sypher logo is displayed in the application header.
   - Provides visual branding and a professional appearance.

3. **User Interaction**
   - The chatbot asks for the user's name and personalises the conversation.
   - Basic input/output functionality for text-based interaction.

4. **Keyword Recognition**
   - The chatbot identifies key cybersecurity terms (phishing, malware, password, 2FA, privacy, scam, VPN, update, ransomware, social engineering).
   - Provides helpful tips and information based on detected keywords.

5. **Randomised Responses**
   - Each keyword has multiple response variations to make conversations feel natural and non-repetitive.
   - Prevents the chatbot from sounding robotic.

6. **Error Handling and Edge Cases**
   - Handles empty inputs gracefully.
   - Provides appropriate responses when keywords are not recognised.
   - Robust exception handling throughout the application.

---

## Part 2 – GUI Enhancement and Advanced Features

Part 2 transformed the console-based application into a fully featured WPF graphical user interface with advanced conversational capabilities.

### Features Implemented:

1. **WPF GUI Design and Implementation**
   - Modern, professional user interface with dark theme sidebar.
   - Chat message display with proper alignment (user messages on right, bot messages on left).
   - Colour-coded messages for visual clarity.
   - Responsive layout that adapts to window resizing.

2. **Voice Greeting (Enhanced)**
   - Integrated with the WPF interface.
   - Audio playback synchronised with application startup.

3. **Image Display**
   - Visual elements including ASCII art and profile images.
   - Background imagery for visual appeal.

4. **Keyword Recognition (Extended)**
   - Expanded keyword database with more cybersecurity terms.
   - Enhanced response generation with context awareness.

5. **Random Responses (Enhanced)**
   - Multiple response variations per keyword.
   - Response selection based on conversation context.

6. **Conversation Flow and Memory Recall**
   - The chatbot remembers user information (name, preferences).
   - Follow-up phrases like "tell me more" continue the previous topic.
   - Context retention throughout the session.

7. **Sentiment Detection**
   - Detects worry, urgency, or concern in user messages.
   - Auto-provides reassuring guidance when negative sentiment is detected.
   - Tailors responses based on emotional tone.

8. **Code Optimisation**
   - Clean, maintainable code structure.
   - Separation of concerns (UI, business logic, data layer).
   - Efficient event handling and resource management.

9. **Error Handling and Edge Cases (Enhanced)**
   - Graceful handling of all user inputs.
   - Placeholder text behaviour in input fields.
   - Validation and user feedback for all actions.

---

## Part 3 – Final Integration and Advanced Features

Part 3 added four major features on top of the existing application, transforming it into a comprehensive cybersecurity awareness tool.

### Features Implemented:

1. **Task Assistant with Reminders**
   - Users can add cybersecurity-related tasks with titles, descriptions, and optional reminders.
   - Tasks are stored in an SQLite database using Entity Framework Core.
   - Users can view all tasks, mark them as complete, or delete them.
   - The task list updates in real-time and persists across application sessions.
   - Reminder functionality allows users to set timeframes (e.g., "in 3 days", "tomorrow").

2. **Cybersecurity Mini-Game (Quiz)**
   - 11 questions covering phishing, password safety, safe browsing, social engineering, 2FA, malware, and privacy.
   - Mix of multiple-choice and true/false question formats.
   - Questions are displayed one at a time for focused learning.
   - Immediate feedback after each answer with explanations to reinforce learning.
   - Score tracking throughout the quiz.
   - Final score display with motivational feedback ("Great job! You're a cybersecurity pro!" or "Keep learning to stay safe online!").

3. **Natural Language Processing (NLP) Simulation**
   - Uses keyword detection with `string.Contains()` to understand varied phrasings.
   - Recognises intents for: adding tasks, setting reminders, starting the quiz, and viewing the activity log.
   - Flexible phrasing support (e.g., "Add task", "Add a task", "Create task", "Enable" all trigger task creation).
   - Extracts task titles and reminder times from natural language input.
   - Falls back to existing Part 2 logic when no intent is detected.

4. **Activity Log Feature**
   - Records every significant action the chatbot takes (tasks added, completed, deleted, quiz started/completed).
   - Stores logs in the SQLite database with timestamps.
   - Users can view recent activity by typing "Show activity log" or "What have you done for me?".
   - Displays the last 10 actions with formatting and timestamps.
   - "Show More" option to view the complete log history.

5. **Database Integration**
   - SQLite database with Entity Framework Core.
   - Two tables: Tasks and Logs.
   - Automatic database creation on first run.
   - All CRUD operations sync correctly between the GUI and database.
   - Data persistence across application sessions.

6. **Full Integration with Parts 1 and 2**
   - All features from Parts 1 and 2 continue to work seamlessly.
   - The chatbot's keyword recognition, sentiment detection, and memory are preserved.
   - NLP intent detection runs before the existing logic, allowing graceful fallback.
   - Consistent user experience across all features.

---

## Technical Implementation Summary

### Part 1 Implementation
- Console-based application with basic input/output.
- Dictionary-based keyword and response storage.
- Simple string matching for keyword detection.
- Random response selection for natural conversation.
- Basic error handling for user inputs.

### Part 2 Implementation
- WPF with XAML for modern UI design.
- MVVM-inspired architecture (UI separated from business logic).
- Audio playback using System.Media.SoundPlayer.
- Sentiment detection using simple keyword analysis.
- Context memory through conversation state management.
- Placeholder text behaviour in input fields.

### Part 3 Implementation
- Entity Framework Core with SQLite for database operations.
- Three-layer architecture: Models, Data, Services.
- Singleton pattern for ActivityLogger.
- NLP intent detection using string.Contains and simple extraction methods.
- Dynamic UI controls for quiz options (radio buttons).
- Database EnsureCreated for automatic setup.

---

## Prerequisites

- **Visual Studio 2022** (or later)
- **.NET 8.0** (or compatible version)
- **NuGet Packages** (automatically restored on build):
  - Microsoft.EntityFrameworkCore.Sqlite
  - Microsoft.EntityFrameworkCore.Proxies

---

## Setup Instructions

1. **Clone the repository**
   ```
   (https://github.com/k4r1i45r/POEPart1_CyberChatBot.git)
   ```

2. **Open the solution**
   Double-click SypherUI.sln in Visual Studio.

3. **Restore NuGet packages**
   Right-click the solution -> Restore NuGet Packages (or build – packages will restore automatically).

4. **Place the audio file**
   Ensure greeting.wav is in the output folder (bin\Debug\net8.0-windows\) or set Build Action = Content, Copy to Output = Copy if newer.
   (If you don't have a custom greeting, you can use any short .wav file named greeting.wav.)

5. **Image resources**
   The project expects:
   - sypher ascii.png (logo)
   - Screenshot 2026-05-15 095847.png (background pattern)
   - icons8-profile-48.png (profile icon)
   You can replace these with your own images or adjust the XAML paths.

6. **Build and run**
   Press F5 or click Start.
   The database file (database.db) will be created automatically in the output folder when you first add a task or log an action.

---

## Testing the Application – Quick Commands

**| Action | Type in Chat |**
| 1. Add a task >> Add task - Review privacy settings |
| 2. Set a reminder >> Remind me to update password tomorrow |
| 3. Start the quiz >> Start quiz |
| 4. Show activity log >> Show activity log |
| 4. Show more log entries >> Show more |

You can also use the **Tasks** and **Quiz** tabs for the same functionality.

---

## Video Presentations

### Part 1 Video
- **Link:** [https://youtu.be/3GFtZxb8ucM?si=-nRGubh5VLdx5_n_]
- **Content:** Console-based chatbot demonstrating keyword recognition, random responses, and basic user interaction.

### Part 2 Video
- **Link:** [https://youtu.be/-dO7Z4f5VqA?si=UVEOXg4CDFwIkVqF]
- **Content:** WPF GUI implementation, voice greeting, sentiment detection, memory recall, and conversation flow.

### Part 3 Video (Final Submission)
- **Link:** 
- **Content:** Complete application walkthrough covering task assistant, quiz, NLP simulation, activity log, and integration with Parts 1 and 2.

**The Part 3 video covers:**
- Launch and voice greeting.
- Name input and personalised interaction.
- Keyword detection, sentiment analysis, and follow-up.
- Adding tasks via chat and via the task panel.
- Completing and deleting tasks.
- Starting and completing the quiz with 11 questions.
- Immediate feedback and final score display.
- Viewing the activity log and using "Show More".
- Explanation of the code structure (models, services, database, NLP).

---

## Releases

Three tagged releases are available on GitHub, each corresponding to a POE part:

- **v1.0** – Initial chatbot with keyword recognition and basic responses (Part 1).
- **v2.0** – GUI, sentiment analysis, and memory (Part 2).
- **v3.0** – Full integration of Task Assistant, Quiz, NLP, and Activity Log (Part 3 – final).

---

## Technologies and Libraries

- **Framework:** WPF (.NET 8) with XAML
- **ORM:** Entity Framework Core 8 (SQLite)
- **Audio:** System.Media.SoundPlayer
- **Database:** SQLite (file-based)
- **Language:** C# 12

---

## Project Structure (Core Files)

```
SypherUI/
├── Models/
│   ├── Task.cs              # Task data model
│   ├── Log.cs               # Activity log data model
│   └── QuizQuestion.cs      # Quiz question model
├── Data/
│   └── ApplicationDbContext.cs  # Entity Framework Core context
├── Services/
│   ├── TaskStorageHelper.cs # Database CRUD operations
│   ├── TaskManager.cs       # Task business logic
│   ├── QuizManager.cs       # Quiz logic and questions
│   └── ActivityLogger.cs    # Activity logging service
├── MainWindow.xaml          # Main GUI layout
├── MainWindow.xaml.cs       # Main window code-behind
└── (existing files from Parts 1 and 2)
```

---

## What's Included in This Repository

- Complete source code for all three parts.
- greeting.wav (placeholder – you must provide your own or use a test file).
- All images used in the UI.
- This README.md.
- A .gitignore for Visual Studio.
- SQLite database file (database.db) is not tracked – it is created locally.

---

**Built with by [Your Name]**
*Last updated: June 2026*
