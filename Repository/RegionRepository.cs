using HandyMan.Data;
using HandyMan.Interfaces;
using HandyMan.Models;
using Microsoft.EntityFrameworkCore;

namespace HandyMan.Repository
{
    public class RegionRepository : IRegionRepository
    {
        private readonly Handyman_DBContext context;
        public RegionRepository(Handyman_DBContext _context)
        {
            context = _context;
        }

        
        public async void CreateRegion(Region region)
        {
            await context.Regions.AddAsync(region);

        }

        public void DeleteRegionById(int id)
        {
            context.Regions.Remove(context.Regions.Find(id));
        }

        public void EditRegion(Region region)
        {
            context.Entry(region).State = EntityState.Modified;

        }

        public async Task<IEnumerable<Region>> GetRegionAsync()
        {
            return await context.Regions.ToListAsync();
        }

        public async Task<Region> GetRegionByIdAsync(int id)
        {
            return await context.Regions.FindAsync(id);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }
    }
}
