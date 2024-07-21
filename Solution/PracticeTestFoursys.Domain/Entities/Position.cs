using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTestFoursys.Domain.Entities
{
    public class Position
    {
        public Position(string practiceTestItemId, string positionId, string productId, string clientId, DateTime date, decimal value, decimal quantity)
        {
            PositionId = positionId;
            ProductId = productId;
            ClientId = clientId;
            Date = date;
            Value = value;
            Quantity = quantity;
        }

        public required string PositionId { get; set; }
        public required string ProductId { get; set; }
        public required string ClientId { get; set; }
        public required DateTime Date { get; set; }
        public required decimal Value { get; set; }
        public required decimal Quantity { get; set; }
    }
}
