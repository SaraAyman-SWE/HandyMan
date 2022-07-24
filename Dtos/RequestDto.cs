using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HandyMan.Models;
using Microsoft.EntityFrameworkCore;

namespace HandyMan.Dtos
{
    public class RequestDto
    {
        // NOT FINISHED
        public int Request_ID { get; set; }
        public int Handyman_SSN { get; set; }
        public int Client_ID { get; set; }
        public int? Request_Status { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Request_Date { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Request_Order_Date { get; set; }
        public int? Client_Rate { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string Client_Review { get; set; }
        public int? Handy_Rate { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string Handy_Review { get; set; }

        public virtual Client Client { get; set; }
        
        public virtual Handyman Handyman_SSNNavigation { get; set; }
        
        //public virtual ICollection<Payment> Payments { get; set; }
    }
}
