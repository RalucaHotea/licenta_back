using BusinessObjectLayer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjectLayer.Entities
{
    [Table("Orders")]
    public class OrderEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }

        [ForeignKey("PickupPoints")]
        public int PickupPointId { get; set; }
        public double TotalPrice { get; set; }

        public string ApprovalNumber { get; set; }

        public string BillNumber { get; set; }

        public DateTime? SubmittedAt { get; set; }
        
        public virtual UserEntity User { get; set; }
        public virtual List<OrderItemEntity> Items { get; set; }
    }
}
