using MongoDB.Bson;
using MongoDB.Driver;
using NomuBackend.Settings;

namespace NomuBackend.Services
{
    public class SalaryService : ISalaryService
    {
        private readonly IMongoCollection<Salary> _salaries;

        public SalaryService(IMongoClient client, IMongoDbSettings settings)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (settings == null) throw new ArgumentNullException(nameof(settings));

            var database = client.GetDatabase(settings.DatabaseName);
            _salaries = database.GetCollection<Salary>("Salaries");
        }

        // Get all salaries asynchronously, returns Task<List<Salary>>
        public async Task<List<Salary>> GetSalariesAsync()
        {
            try
            {
                return await _salaries.Find(salary => true).ToListAsync();
            }
            catch
            {
                // Handle or log the exception as needed
                return new List<Salary>(); // Return an empty list if there's an exception
            }
        }

        // Get a salary by ID asynchronously, returns Task<Salary>
        public async Task<Salary> GetSalaryByIdAsync(string id)
        {
            try
            {
                return await _salaries.Find(salary => salary.Id == ObjectId.Parse(id)).FirstOrDefaultAsync();
            }
            catch
            {
                // Handle or log the exception as needed
                return null; // Return null if there's an exception
            }
        }

        // Create a salary asynchronously, returns Task<bool> to indicate success/failure
        public async Task<bool> CreateSalaryAsync(Salary salary)
        {
            try
            {
                await _salaries.InsertOneAsync(salary);
                return true; // return true if the salary is successfully created
            }
            catch
            {
                return false; // return false if there's an exception
            }
        }

        // Update a salary asynchronously, returns Task<bool> to indicate success/failure
        public async Task<bool> UpdateSalaryAsync(string id, Salary salaryIn)
        {
            try
            {
                var result = await _salaries.ReplaceOneAsync(salary => salary.Id == ObjectId.Parse(id), salaryIn);
                return result.IsAcknowledged && result.ModifiedCount > 0; // return true if update was successful
            }
            catch
            {
                return false; // return false if there's an exception
            }
        }

        // Remove a salary asynchronously, returns Task<bool> to indicate success/failure
        public async Task<bool> RemoveSalaryAsync(string id)
        {
            try
            {
                var result = await _salaries.DeleteOneAsync(salary => salary.Id == ObjectId.Parse(id));
                return result.IsAcknowledged && result.DeletedCount > 0; // return true if removal was successful
            }
            catch
            {
                return false; // return false if there's an exception
            }
        }

        // Calculate profit
        public void CalculateProfit(Salary salary)
        {
            // Implementing simple logic for profit calculation
            const double dailyGrowthRate = 1.02; // 2% daily growth
            const double weeklyGrowthRate = 1.05; // 5% weekly growth
            const double monthlyGrowthRate = 1.10; // 10% monthly growth
            const double yearlyGrowthRate = 1.20; // 20% yearly growth

            salary.DailyProfit *= dailyGrowthRate;
            salary.WeeklyProfit *= weeklyGrowthRate;
            salary.MonthlyProfit *= monthlyGrowthRate;
            salary.YearlyProfit *= yearlyGrowthRate;

            // Pending profit is calculated as 10% of monthly profit
            salary.PendingProfit = salary.MonthlyProfit * 0.10;
        }

        // Display profit details
        public void DisplayProfitDetails(Salary salary)
        {
            // Console output to display profit details
            Console.WriteLine($"Salary ID: {salary.Id}");
            Console.WriteLine($"Daily Profit: {salary.DailyProfit:C}");
            Console.WriteLine($"Weekly Profit: {salary.WeeklyProfit:C}");
            Console.WriteLine($"Monthly Profit: {salary.MonthlyProfit:C}");
            Console.WriteLine($"Yearly Profit: {salary.YearlyProfit:C}");
            Console.WriteLine($"Pending Profit: {salary.PendingProfit:C}");
        }
    }
}
