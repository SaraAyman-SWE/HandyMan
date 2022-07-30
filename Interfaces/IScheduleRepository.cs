using HandyMan.Models;

namespace HandyMan.Interfaces
{
    public interface IScheduleRepository
    {
        Task<IEnumerable<Schedule>> GetScheduleAsync();//for admin use
        Task<IEnumerable<Schedule>> GetSchedulesByHandymanSsnAsync(int id);
        void CreateSchedule(Schedule schedule);
        void DeleteSchedule(Schedule schedule);
        Task<bool> SaveAllAsync();
    }
}
