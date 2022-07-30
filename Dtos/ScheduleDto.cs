

using System.ComponentModel.DataAnnotations;

namespace HandyMan.Dtos
{
    public class ScheduleDto
    {
        [Key]
        public int Handy_SSN { get; set; }

        [Key]
        public DateTime Schedule_Date { get; set; }
        [Key]
        public TimeSpan Time_From { get; set; }
        public TimeSpan Time_To { get; set; }
    }
}
