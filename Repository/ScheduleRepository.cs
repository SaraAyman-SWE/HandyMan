using HandyMan.Data;
using HandyMan.Interfaces;
using HandyMan.Models;
using Microsoft.EntityFrameworkCore;

namespace HandyMan.Repository
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly Handyman_DBContext context;

        public ScheduleRepository(Handyman_DBContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Schedule>> GetScheduleAsync() //For Admin Usage
        {
            return await context.Schedules.ToListAsync();
        }

        public async Task<IEnumerable<Schedule>> GetSchedulesByHandymanSsnAsync(int id)
        {
            var handyman = context.Handymen.Find(id);
            if (handyman == null)
            {
                return null;
            }
            var schedules = await context.Schedules.Where(a => a.Handy_SSN == id && a.Schedule_Date.Day>=DateTime.Now.Day).ToListAsync();
            return schedules;
        }

        public async void CreateSchedule(Schedule schedule)
        {
            await context.Schedules.AddAsync(schedule);
            
        }
        
        public void DeleteSchedule(Schedule schedule)
        {
            context.Remove(schedule);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }
    }
}
