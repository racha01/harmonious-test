using DBContext.SQLServer;
using DBContext.SQLServer.Models;
using Domain.EmployeeWage.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.EmployeeWage.Services
{
    public interface IEmployeeWageService
    {
        Task<List<EmployeeWageModel>> GetEmployeeWageAsync();
        Task<string> CreateEmployeeWageAsync(CreateEmployeeWageModel data);
        Task<List<WageOfEachEmployeeModel>> GetWageOfEachEmployeeAsync();

        Task<EmployeeWageModel> GetEmployeeWageByIdAsync(int id);
        Task UpdateEmployeeWageAsync(UpdateEmployeeWageModel data, int id);
        Task DeleteEmployeeWageAsync(int id);
    }
    public class EmployeeWageService: IEmployeeWageService
    {
        private readonly ApplicationDBContext _context;

        public EmployeeWageService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<EmployeeWageModel>> GetEmployeeWageAsync()
        {
            var datas = await _context.EmployeeWages.ToListAsync();

            var EmployeeWageDatas = datas.Select(s => new EmployeeWageModel()
            {
                Id = s.Id,
                Name = s.Name,
                Position = s.Position,
                Day = s.Day,
                Hour = s.Hour
            }).ToList<EmployeeWageModel>();

            return EmployeeWageDatas;
        }

        public async Task<EmployeeWageModel> GetEmployeeWageByIdAsync(int id)
        {
            var employeeWageDoc = await _context.EmployeeWages.SingleOrDefaultAsync(ep => ep.Id.Equals(id));

            var employeeWageModel = new EmployeeWageModel()
            {
                Id = employeeWageDoc.Id,
                Name = employeeWageDoc.Name,
                Position = employeeWageDoc.Position,
                Day = employeeWageDoc.Day,
                Hour = employeeWageDoc.Hour
            };

            return employeeWageModel;
        }

        public async Task<string> CreateEmployeeWageAsync(CreateEmployeeWageModel data)
        {
            var createEmployeeWage = new EmplayeeWages()
            {
                Name = data.Name,
                Position = data.Position,
                Day = data.Day,
                Hour = data.Hour
            };
            await _context.EmployeeWages.AddAsync(createEmployeeWage);
            await _context.SaveChangesAsync();

            return createEmployeeWage.Id.ToString();
        }

        public async Task<List<WageOfEachEmployeeModel>> GetWageOfEachEmployeeAsync()
        {
            var employeeWageDatas = await _context.EmployeeWages.ToListAsync();
            var wageDatas = await _context.Wages.ToListAsync();

            var wageOfEachEmployeeData = employeeWageDatas.GroupBy(g => g.Name).Select(s =>
            {
                double wageTotal = 0;
                foreach (var employee in s)
                {
                    wageTotal += employee.Hour * wageDatas.SingleOrDefault(wage => wage.Position.Equals(employee.Position) && wage.Day.Equals(employee.Day)).Wagerate;
                }

                return new WageOfEachEmployeeModel()
                {
                    Name = s.Key,
                    WageTotal = wageTotal
                };
            }).ToList<WageOfEachEmployeeModel>();

            return wageOfEachEmployeeData;
        }

        public async Task UpdateEmployeeWageAsync(UpdateEmployeeWageModel data, int id)
        {
            var employeeDoc = await _context.EmployeeWages.SingleOrDefaultAsync(wage => wage.Id.Equals(id));

            employeeDoc.Name = data.Name;
            employeeDoc.Position = data.Position;
            employeeDoc.Day = data.Day;
            employeeDoc.Hour = data.Hour;

            _context.EmployeeWages.Update(employeeDoc);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteEmployeeWageAsync(int id)
        {
            var emplaoyeeWagesDoc = await _context.EmployeeWages.SingleOrDefaultAsync(wage => wage.Id.Equals(id));

            _context.EmployeeWages.Remove(emplaoyeeWagesDoc);
            await _context.SaveChangesAsync();
        }
    }
}
