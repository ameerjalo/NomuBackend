namespace NomuBackend.Services
{
    public interface ISalaryService
    {
        Task<List<Salary>> GetSalariesAsync();
        Task<Salary> GetSalaryByIdAsync(string id);
        Task<bool> CreateSalaryAsync(Salary salary); // Return bool to indicate success/failure
        Task<bool> UpdateSalaryAsync(string id, Salary salaryIn); // Return bool to indicate success/failure
        Task<bool> RemoveSalaryAsync(string id); // Return bool to indicate success/failure
        void CalculateProfit(Salary salary);
        void DisplayProfitDetails(Salary salary);
    }
}
