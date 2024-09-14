using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Salary
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }

    public double DailyProfit { get; set; }
    public double WeeklyProfit { get; set; }
    public double MonthlyProfit { get; set; }
    public double YearlyProfit { get; set; }
    public double PendingProfit { get; set; }


}
