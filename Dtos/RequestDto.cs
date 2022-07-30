using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HandyMan.Models;
using Microsoft.EntityFrameworkCore;

namespace HandyMan.Dtos
{
    public class RequestDto
    {
        [Key]
        public int Request_ID { get; set; }

        [Required]
        public int Handyman_SSN { get; set; }

        [Required]
        public int Client_ID { get; set; }

        [Range(0, 4)] 
        public int Request_Status { get; set; } = 1;

        [Required]
        [Column(TypeName = "date")]
        public DateTime Request_Date { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime Request_Order_Date { get; set; } = DateTime.Now;

        [Range(1, 5)]
        public int? Client_Rate { get; set; }

        [StringLength(100)]
        public string? Client_Review { get; set; }

        [Range(1, 5)]
        public int? Handy_Rate { get; set; }

        [StringLength(100)]
        public string? Handy_Review { get; set; }

        public virtual ICollection<PaymentDto>? Payments { get; set; }
    }
}
