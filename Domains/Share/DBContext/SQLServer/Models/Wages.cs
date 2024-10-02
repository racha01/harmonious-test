using System.ComponentModel.DataAnnotations;

namespace DBContext.SQLServer.Models
{
    public class Wages
    {
        public int Id { get; set; }
        public string Position { get; set; }
        public string Day { get; set; }
        public double Wagerate { get; set; }
    }
}
