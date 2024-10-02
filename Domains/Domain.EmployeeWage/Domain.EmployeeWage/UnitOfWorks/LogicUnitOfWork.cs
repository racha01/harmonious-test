
using DBContext.MongoDB;
using DBContext.SQLServer;
using Domain.EmployeeWage.Services;

namespace Domain.EmployeeWage.UnitOfWorks
{
    public interface ILogicUnitOfWork
    {
        IWageService WageService { get; set; }
        IEmployeeWageService EmployeeWageService { get; set; }
    }
    public class LogicUnitOfWork: ILogicUnitOfWork
    {
        private readonly ApplicationDBContext _context;

        public LogicUnitOfWork(
            ApplicationDBContext context
        )
        {
            _context = context;
        }

        private IWageService wageService;
        public IWageService WageService
        {
            get { return wageService ?? (wageService = new WageService(_context)); }
            set { wageService = value; }
        }

        private IEmployeeWageService employeeWageService;
        public IEmployeeWageService EmployeeWageService
        {
            get { return employeeWageService ?? (employeeWageService = new EmployeeWageService(_context)); }
            set { employeeWageService = value; }
        }

    }
}
