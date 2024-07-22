using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PracticeTestFoursys.Domain.Entities
{
    public class Position
    {
        [JsonPropertyName("positionId")]
        [Column("positionid")]
        public required string PositionId { get; set; }
        [JsonPropertyName("date")]
        [Column("date")]
        public required DateTime Date { get; set; }
        [JsonPropertyName("productId")]
        [Column("productid")]
        public required string ProductId { get; set; }
        [JsonPropertyName("clientId")]
        [Column("clientid")]
        public required string ClientId { get; set; }
       
        [JsonPropertyName("value")]
        [Column("value")]
        public required decimal Value { get; set; }
        [JsonPropertyName("quantity")]
        [Column("quantity")]
        public required decimal Quantity { get; set; }
    }
}
