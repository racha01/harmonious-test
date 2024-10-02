using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Domain.EmployeeWage.Models
{
    public class WageModel
    {
        public int Id { get; set; }
        public string Position { get; set; }
        public string Day {  get; set; }
        public double Wagerate { get; set; }

    }

    public class CreateWageModel
    {
        public string Position { get; set; }
        public string Day { get; set; }
        public double Wagerate { get; set; }
    }

    public class UpdateWageModel
    {
        public string Position { get; set; }
        public string Day { get; set; }
        public double Wagerate { get; set; }
    }
}
