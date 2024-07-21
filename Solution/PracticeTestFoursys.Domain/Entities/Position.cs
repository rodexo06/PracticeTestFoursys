using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PracticeTestFoursys.Domain.Entities
{
    public class Position
    {
        public Position(string positionId, string productId, string clientId, DateTime date, decimal value, decimal quantity)
        {
            PositionId = positionId;
            ProductId = productId;
            ClientId = clientId;
            Date = date;
            Value = value;
            Quantity = quantity;
        }
        [JsonPropertyName("positionId")]
        [Column("positionid")]
        public required string PositionId { get; set; }
        [JsonPropertyName("productId")]
        [Column("productid")]
        public required string ProductId { get; set; }
        [JsonPropertyName("clientId")]
        [Column("clientid")]
        public required string ClientId { get; set; }
        [JsonPropertyName("date")]
        [Column("date")]
        public required DateTime Date { get; set; }
        [JsonPropertyName("value")]
        [Column("value")]
        public required decimal Value { get; set; }
        [JsonPropertyName("quantity")]
        [Column("quantity")]
        public required decimal Quantity { get; set; }
    }
}
