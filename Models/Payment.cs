﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HandyMan.Models
{
    [Table("Payment")]
    public partial class Payment
    {
        [Key]
        public int Payment_ID { get; set; }
        [Key]
        public int Request_ID { get; set; }
        public bool? Payment_Status { get; set; }
        public bool Method { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Payment_Date { get; set; }
        public int Payment_Amount { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Transaction_ID { get; set; }

        [ForeignKey("Request_ID")]
        [InverseProperty("Payments")]
        public virtual Request Request { get; set; }
    }
}