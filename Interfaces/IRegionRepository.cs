using HandyMan.Models;

namespace HandyMan.Interfaces
{
    public interface IRegionRepository
    {

        Task<IEnumerable<Region>> GetRegionAsync();
        Task<Region> GetRegionByIdAsync(int id);
        void CreateRegion(Region region);
        void EditRegion(Region region);
        void DeleteRegionById(int id);
        Task<bool> SaveAllAsync();
    }
}
