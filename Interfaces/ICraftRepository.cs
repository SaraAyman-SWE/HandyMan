using HandyMan.Models;

namespace HandyMan.Interfaces
{
    public interface ICraftRepository
    {

        Task<IEnumerable<Craft>> GetCraftAsync();
        Task<Craft> GetCraftByIdAsync(int id);
        void CreateCraft(Craft craft);
        void EditCraft(Craft craft);
        void DeleteCraftById(int id);
        Task<bool> SaveAllAsync();
    }
}
