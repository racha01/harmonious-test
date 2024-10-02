using DBContext.SQLServer;
using DBContext.SQLServer.Models;
using Domain.EmployeeWage.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.EmployeeWage.Services
{
    public interface IWageService
    {
        Task<List<WageModel>> GetWageAsync();
        Task<WageModel> GetWageByIdAsync(int id);
        Task<string?> CreateWageAsync(CreateWageModel data);
        Task UpdateWageAsync(UpdateWageModel data, int id);
        Task DeleteWageAsync(int id);
    }
    public class WageService : IWageService
    {
        private readonly ApplicationDBContext _context;

        public WageService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<WageModel>> GetWageAsync()
        {
            var wageDatas = await _context.Wages.ToListAsync();

            var wageModelDatas = wageDatas.Select(s => new WageModel()
            {
                Id = s.Id,
                Position = s.Position,
                Day = s.Day,
                Wagerate = s.Wagerate
            }).ToList<WageModel>();

            return wageModelDatas;
        }

        public async Task<WageModel> GetWageByIdAsync(int id)
        {
            var wageDoc = await _context.Wages.SingleAsync(wage => wage.Id.Equals(id));

            var wageModel =  new WageModel()
            {
                Id = wageDoc.Id,
                Position = wageDoc.Position,
                Day = wageDoc.Day,
                Wagerate = wageDoc.Wagerate
            };

            return wageModel;
        }
        public async Task<string?> CreateWageAsync(CreateWageModel data)
        {
            var createWageDoc = new Wages
            {
                Position = data.Position,
                Day = data.Day,
                Wagerate = data.Wagerate,
            };

            await _context.Wages.AddAsync(createWageDoc);
            await _context.SaveChangesAsync();

            return createWageDoc.Id.ToString();
        }

        public async Task UpdateWageAsync(UpdateWageModel data, int id)
        {
            var wageDoc = await _context.Wages.SingleAsync(wage => wage.Id.Equals(id));

            wageDoc.Position = data.Position;
            wageDoc.Day = data.Day;
            wageDoc.Wagerate = data.Wagerate;

            _context.Wages.Update(wageDoc);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteWageAsync(int id)
        {
            var wageDoc = await _context.Wages.SingleOrDefaultAsync(wage => wage.Id.Equals(id));

            _context.Wages.Remove(wageDoc);
            await _context.SaveChangesAsync();
        }

    }
}
