using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DBContext.MongoDB.Models
{
    public class tb_wage
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; }

        [BsonElement("position")]
        public string Position { get; set; }

        [BsonElement("wages")]
        public List<WageRate> Wages { get; set; }

    }

    public class WageRate
    {
        [BsonElement("day")]
        public string Day { get; set; }

        [BsonElement("hourly_wage")]
        public int HourlyWage { get; set; }
    }
}
