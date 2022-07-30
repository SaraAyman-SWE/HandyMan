using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HandyMan.Models;
using Microsoft.EntityFrameworkCore;

namespace HandyMan.Dtos
{
    public class HandymanDto
    {
        [Key]
        public int Handyman_SSN { get; set; } // is required ??
        [Required]
        [StringLength(50)]
        public string Handyman_Name { get; set; }
        
        [Required(ErrorMessage = "Mobile is required")]
        [RegularExpression(@"^([0-9]{11})$", ErrorMessage = "Invalid Mobile Number.")]
        [StringLength(11)]
        public string Handyman_Mobile { get; set; }

        public int CraftID { get; set; }

        [Required]
        public int Handyman_Fixed_Rate { get; set; }
        public bool? Approved { get; set; } = false;
        public bool? Open_For_Work { get; set; } = false;
        [Unicode(false)]
        public string? Handyman_Photo { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Handyman_ID_Image { get; set; }
        [Unicode(false)]
        public string? Handyman_Criminal_Record { get; set; }
        [StringLength(16,MinimumLength = 8)]
        [Unicode(false)]
        [Required]
        public string Password { get; set; }

        public int? Balance { get; set; } = 0;

        [Range(1, 5)]
        public double? Rating { get; set; }
        public virtual ICollection<RequestDto>? Requests { get; set; }

        public virtual ICollection<ScheduleDto>? Schedules { get; set; }

        
        public virtual ICollection<RegionDto>? Regions { get; set; }
    }
}
