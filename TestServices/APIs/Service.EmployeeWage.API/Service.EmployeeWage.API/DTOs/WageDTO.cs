using System.ComponentModel.DataAnnotations;
using System;
using System.Text.Json.Serialization;

namespace Service.EmployeeWage.API.DTOs
{
    public class WageDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("position")]
        public string Position { get; set; }

        [JsonPropertyName("day")]
        public string Day { get; set; }

        [JsonPropertyName("wagerate")]
        public double Wagerate { get; set; }

    }

    public class CreateWageDTO
    {
        [JsonPropertyName("position")]
        [EnumDataType(typeof(PositionEnum))]
        public string Position { get; set; }

        [JsonPropertyName("day")]
        [EnumDataType(typeof(DayEnum))]
        public string Day { get; set; }

        [JsonPropertyName("wagerate")]
        public double Wagerate { get; set; }
    }

    public class UpdateWageDTO
    {
        [JsonPropertyName("position")]
        [EnumDataType(typeof(PositionEnum))]
        public string Position { get; set; }

        [JsonPropertyName("day")]
        [EnumDataType(typeof(DayEnum))]
        public string Day { get; set; }

        [JsonPropertyName("wagerate")]
        public double Wagerate { get; set; }
    }

    public enum DayEnum
    {
        Sun,
        Mon,
        Tue,
        Wed,
        Thu,
        Fri,
        Sat
    }
    public enum PositionEnum
    {
        ProjectLead,
        Employee
    }
}
