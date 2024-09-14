

namespace NomuBackend.Services
{
    public interface ITaskService
    {
        Task CreateTask(string name, string description, DateTime dueDate, TaskFrequency frequency);
        bool UpdateTask(Guid taskId, string name, string description, DateTime dueDate, TaskFrequency frequency);
        bool DeleteTask(Guid taskId);
        Task GetTaskById(Guid taskId);
        List<Task> GetAllTasks();
        List<Task> GetTasksByFrequency(TaskFrequency frequency);
        List<Task> GetTasksDueToday();
        List<Task> GetTasksDueThisWeek();
        List<Task> GetTasksDueThisMonth();
        DateTime CalculateNextDueDate(Task task);
    }
}
