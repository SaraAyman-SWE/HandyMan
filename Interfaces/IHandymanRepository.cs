using HandyMan.Models;

namespace HandyMan.Interfaces
{
    public interface IHandymanRepository
    {
        Task<IEnumerable<Handyman>> GetHandyManAsync();
        Task<IEnumerable<Handyman>> GetVerifiedHandyManAsync(int sortType);
        Task<Handyman> GetHandymanByIdAsync(int id);
        void CalculateHandymanRate(Handyman handyman);
        void CreateHandyman(Handyman handyman);
        void EditHandyman(Handyman handyman);
        void DeleteHandyman(Handyman handyman);
        bool ApproveHandymanById(Handyman handyman);
        bool AddRegionToHandyman(int handymanssn , int regionId);
        bool RemoveRegionFromHandyman(int handymanssn, int regionId);
        Task<IEnumerable<Handyman>> GetHandymenByCraftId(int craftId);
        Task<bool> SaveAllAsync();

    }
}
