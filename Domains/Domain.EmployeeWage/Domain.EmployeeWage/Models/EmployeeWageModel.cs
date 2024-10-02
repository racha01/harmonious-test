
namespace Domain.EmployeeWage.Models
{
    public class EmployeeWageModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }

        public string Day { get; set; }
        public int Hour { get; set; }
    }
    public class CreateEmployeeWageModel
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public string Day { get; set; }
        public int Hour { get; set; }
    }

    public class UpdateEmployeeWageModel
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public string Day { get; set; }
        public int Hour { get; set; }
    }

    public class WageOfEachEmployeeModel
    {
        public string Name { get; set; }
        public double WageTotal { get; set; }
    }
}
