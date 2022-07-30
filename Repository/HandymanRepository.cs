using HandyMan.Data;
using HandyMan.Interfaces;
using HandyMan.Models;
using Microsoft.EntityFrameworkCore;

namespace HandyMan.Repository
{
    public class HandymanRepository : IHandymanRepository
    {
        private readonly Handyman_DBContext context;

        public HandymanRepository(Handyman_DBContext _context)
        { 
            context = _context;
        }
        public async void CreateHandyman(Handyman handyman)
        {
            await context.Handymen.AddAsync(handyman);
        }

        public void DeleteHandyman(Handyman handyman)
        {
            context.Handymen.Remove(handyman);
        }

        public void EditHandyman(Handyman handyman)
        {
            context.Entry(handyman).State = EntityState.Modified;
        }

        public async Task<IEnumerable<Handyman>> GetHandyManAsync()
        {
            return await context.Handymen.ToListAsync();
        }

        public async Task<IEnumerable<Handyman>> GetHandymenByCraftId(int craftId)
        {
            return await context.Handymen.Where(a => a.CraftID == craftId && a.Approved == true).ToListAsync();
        }

        public async Task<IEnumerable<Handyman>> GetVerifiedHandyManAsync(int sortType)
        {
            List<Handyman> handymen = new List<Handyman>();
            if (sortType == 0)
                handymen = await context.Handymen.Where(a => a.Approved == true).ToListAsync();
            // Sort by Open for work
            else if (sortType == 1)
                handymen = await context.Handymen.Where(a=>a.Approved==true).OrderByDescending(s => s.Open_For_Work).ToListAsync();
            // Sort by fixed_rate
            else if (sortType == 2)
                handymen = await context.Handymen.Where(a => a.Approved == true).OrderBy(s => s.Handyman_Fixed_Rate).ToListAsync();
            return handymen;
        }

        public async Task<Handyman> GetHandymanByIdAsync(int id)
        {
            return await context.Handymen.FindAsync(id);
        }

        public void CalculateHandymanRate(Handyman handyman)
        {
            var requests = handyman.Requests;
            if (requests != null)
            {
                double sum = 0, count = 0;
                foreach (var req in requests)
                {
                    if (req.Handy_Rate != null)
                    {
                        sum += (int)req.Handy_Rate;
                        count++;
                    }
                }
                handyman.Rating = sum / count;
            }
        }

        public bool ApproveHandymanById(Handyman handyman)
        {
            if (handyman.Handyman_Photo != null && handyman.Handyman_ID_Image != null &&
                handyman.Handyman_Criminal_Record != null)
            {
                handyman.Approved = true;
                return true;
            }
            return false;
        }

        public bool AddRegionToHandyman(int handymanssn, int regionId)
        {
            var handyman = context.Handymen.Find(handymanssn);
            var region = context.Regions.Find(regionId);
            if(region == null)
            {
                return false;
            }
            handyman.Regions.Add(region);
            return true;

        }

        public bool RemoveRegionFromHandyman(int handymanssn, int regionId)
        {
            var handyman = context.Handymen.Find(handymanssn);
            var region = context.Regions.Find(regionId);
            if (region == null)
            {
                return false;
            }
            handyman.Regions.Remove(region);
            return true;

        }

        public async Task<bool> SaveAllAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }
    }
}
