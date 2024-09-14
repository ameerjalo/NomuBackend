using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Task
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }

    public string TaskId { get; set; } = Guid.NewGuid().ToString();
    public string TaskName { get; set; } = string.Empty;
    public string AssignedTo { get; set; } = string.Empty;
    public DateTime DueDate { get; set; }
    public string TaskFrequency { get; set; } = string.Empty;
    public List<Task> DailyTasks { get; set; } = new List<Task>();
    public List<Task> WeeklyTasks { get; set; } = new List<Task>();
    public List<Task> MonthlyTasks { get; set; } = new List<Task>();
}
