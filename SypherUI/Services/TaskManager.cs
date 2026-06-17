using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections.Generic;
using SypherUI.Models;

namespace SypherUI.Services
{
    public class TaskManager
    {
        private readonly TaskStorageHelper _storage = new();
        private readonly ActivityLogger _logger = ActivityLogger.Instance;

        public string AddTask(string title, string description, string reminder)
        {
            var task = new SypherUI.Models.Task
            {
                Title = title,
                Description = description,
                Reminder = reminder
            };
            _storage.AddTask(task);
            string logMsg = $"Task added: '{title}'" +
                            (string.IsNullOrEmpty(reminder) ? "" : $" (Reminder set for {reminder})");
            _logger.Log(logMsg);
            return logMsg;
        }

        public List<SypherUI.Models.Task> GetAllTasks() => _storage.LoadTasks();

        public string MarkAsComplete(int id)
        {
            var task = _storage.LoadTasks().Find(t => t.Id == id);
            if (task == null) return "Task not found.";
            _storage.MarkAsComplete(id);
            string msg = $"Task '{task.Title}' marked as complete.";
            _logger.Log(msg);
            return msg;
        }

        public string DeleteTask(int id)
        {
            var task = _storage.LoadTasks().Find(t => t.Id == id);
            if (task == null) return "Task not found.";
            _storage.DeleteTask(id);
            string msg = $"Task '{task.Title}' deleted.";
            _logger.Log(msg);
            return msg;
        }
    }
}
