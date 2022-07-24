using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HandyMan.Models;
using Microsoft.EntityFrameworkCore;

namespace HandyMan.Dtos
{
    public class ClientDto
    {
        public int Client_ID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name can't be longer than 50 characters")]
        public string Client_name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Client_Email { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(100, ErrorMessage = "Address cannot be longer than 100 characters")]
        public string Client_Address { get; set; }

        [Required(ErrorMessage = "Mobile is required")]
        [RegularExpression(@"^([0-9]{11})$", ErrorMessage = "Invalid Mobile Number.")]
        [StringLength(11)]
        public string Client_Mobile { get; set; }

        public string Password { get; set; }

        public int Region_ID { get; set; }

        public virtual ICollection<RequestDto>? Requests { get; set; }

        [Range(-99, 99)]
        public double? Balance { get; set; }

        [Range(1, 5)]
        public double? Rating { get; set; }

    }
}
