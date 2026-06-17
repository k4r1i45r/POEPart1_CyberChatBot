using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using SypherUI.Data;
using SypherUI.Models;


namespace SypherUI.Services
{
    public class ActivityLogger
    {
        private static ActivityLogger _instance;
        private readonly ApplicationDbContext _db = new();

        public static ActivityLogger Instance => _instance ??= new ActivityLogger();

        private ActivityLogger() { }

        public void Log(string action)
        {
            var log = new Log { Description = action, CreatedAt = DateTime.Now };
            _db.Logs.Add(log);
            _db.SaveChanges();
        }

        public List<Log> GetRecentLogs(int count = 10)
        {
            return _db.Logs.OrderByDescending(l => l.CreatedAt).Take(count).ToList();
        }

        public List<Log> GetAllLogs()
        {
            return _db.Logs.OrderByDescending(l => l.CreatedAt).ToList();
        }

        public string FormatLogs(List<Log> logs)
        {
            if (logs.Count == 0) return "No actions logged yet.";
            int i = 1;
            return string.Join("\n", logs.Select(l => $"{i++}. {l.Description} (at {l.CreatedAt:HH:mm on dd MMM yyyy})"));
        }
    }
}