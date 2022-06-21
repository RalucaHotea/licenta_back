using BusinessObjectLayer.Entities;
using BusinessObjectLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectLayer.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public OrderStatus Status { get; set; }
        public string ApprovalNumber { get; set; }
        public string BillNumber { get; set; }
        public DateTime? SubmittedAt { get; set; }
        public DateTime? ShippingDate { get; set; }
        public DateTime? ReceivingDate { get; set; }
        public DateTime? PickupDate { get; set; }
        public int PickupPointId { get; set; }
        public double? TotalPrice { get; set; }
        public virtual UserDto User { get; set; }
        public virtual PickupPointDto PickupPoint { get; set; }
        public virtual List<OrderItemEntity> Items { get; set; }
    }
}
