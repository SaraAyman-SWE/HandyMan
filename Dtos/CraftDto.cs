using HandyMan.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HandyMan.Dtos
{
    public class CraftDto
    {

        [Key]
        public int Craft_ID { get; set; }
        [Required]
        [StringLength(20)]
        [Unicode(false)]
        public string Craft_Name { get; set; }

    }
}
