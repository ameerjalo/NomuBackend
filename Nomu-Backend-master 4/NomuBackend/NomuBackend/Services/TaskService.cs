using System;
using System.Collections.Generic;
using System.Linq;

namespace NomuBackend.Services
{
    public enum TaskFrequency { Daily, Weekly, Monthly }

    public class Task
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public TaskFrequency Frequency { get; set; }
    }

    public class TaskService
    {
        private readonly List<Task> _tasks = new();

        public Task CreateOrUpdateTask(Guid? taskId, string name, string description, DateTime dueDate, TaskFrequency frequency)
        {
            var task = taskId == null ? new Task() : _tasks.FirstOrDefault(t => t.Id == taskId) ?? new Task();
            task.Name = name; task.Description = description; task.DueDate = dueDate; task.Frequency = frequency;
            if (taskId == null) _tasks.Add(task);
            return task;
        }

        public bool DeleteTask(Guid taskId) => _tasks.RemoveAll(t => t.Id == taskId) > 0;
        public Task GetTaskById(Guid taskId) => _tasks.FirstOrDefault(t => t.Id == taskId);
        public List<Task> GetTasksByFrequency(TaskFrequency frequency) => _tasks.Where(t => t.Frequency == frequency).ToList();
        public List<Task> GetTasksByDueDate(DateTime startDate, DateTime endDate) => _tasks.Where(t => t.DueDate >= startDate && t.DueDate <= endDate).ToList();
        public List<Task> GetTasksDueToday() => GetTasksByDueDate(DateTime.Today, DateTime.Today);
        public List<Task> GetTasksDueThisWeek() => GetTasksByDueDate(DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek), DateTime.Today.AddDays(7));
        public List<Task> GetTasksDueThisMonth() => GetTasksByDueDate(new(DateTime.Today.Year, DateTime.Today.Month, 1), DateTime.Today.AddMonths(1));
        public DateTime CalculateNextDueDate(Task task) => task.Frequency switch
        {
            TaskFrequency.Daily => task.DueDate.AddDays(1),
            TaskFrequency.Weekly => task.DueDate.AddDays(7),
            TaskFrequency.Monthly => task.DueDate.AddMonths(1),
            _ => task.DueDate
        };
    }
}