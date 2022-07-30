using HandyMan.Data;
using HandyMan.Interfaces;
using HandyMan.Models;
using Microsoft.EntityFrameworkCore;

namespace HandyMan.Repository
{
    public class CraftRepository : ICraftRepository
    {
        private readonly Handyman_DBContext context;

        public CraftRepository(Handyman_DBContext _context)
        {
            context = _context;
        }
        public async void CreateCraft(Craft craft)
        {
            await context.Crafts.AddAsync(craft);
        }

        public void DeleteCraftById(int id)
        {
            context.Crafts.Remove(context.Crafts.Find(id));
        }

        public void EditCraft(Craft craft)
        {
            context.Entry(craft).State = EntityState.Modified;
        }

        public async Task<IEnumerable<Craft>> GetCraftAsync()
        {
            return await context.Crafts.ToListAsync();
        }

        public async Task<Craft> GetCraftByIdAsync(int id)
        {
            return await context.Crafts.FindAsync(id);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }
    }
}
