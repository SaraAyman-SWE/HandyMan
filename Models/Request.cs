﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HandyMan.Models
{
    [Table("Request")]
    [Index("Request_ID", Name = "UQ__Request__E9C5B292D224AFDB", IsUnique = true)]
    public partial class Request
    {
        public Request()
        {
            Payments = new HashSet<Payment>();
        }

        [Key]
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

        [ForeignKey("Client_ID")]
        [InverseProperty("Requests")]
        public virtual Client Client { get; set; }
        [ForeignKey("Handyman_SSN")]
        [InverseProperty("Requests")]
        public virtual Handyman Handyman_SSNNavigation { get; set; }
        [InverseProperty("Request")]
        public virtual ICollection<Payment> Payments { get; set; }
    }
}