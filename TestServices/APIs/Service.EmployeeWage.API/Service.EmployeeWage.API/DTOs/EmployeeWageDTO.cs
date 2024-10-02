using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Service.EmployeeWage.API.DTOs
{
    public class EmployeeWageDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("position")]
        public string Position { get; set; }

        [JsonPropertyName("day")]
        public string Day { get; set; }

        [JsonPropertyName("hour")]
        public int Hour { get; set; }
    }
    public class CreateEmployeeWageDTO
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("position")]
        [EnumDataType(typeof(PositionEnum))]
        public string Position { get; set; }

        [JsonPropertyName("day")]
        [EnumDataType(typeof(DayEnum))]
        public string Day { get; set; }

        [JsonPropertyName("hour")]
        public int Hour { get; set; }
    }

    public class UpdateEmployeeWageDTO
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("position")]
        [EnumDataType(typeof(PositionEnum))]
        public string Position { get; set; }

        [JsonPropertyName("day")]
        [EnumDataType(typeof(DayEnum))]
        public string Day { get; set; }

        [JsonPropertyName("hour")]
        public int Hour { get; set; }
    }

    public class WageOfEachEmployeeDTO
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("wage_total")]
        public double WageTotal { get; set; }
    }
}
