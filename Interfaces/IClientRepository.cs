using HandyMan.Models;

namespace HandyMan.Interfaces
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetClientsAsync();
        Task<Client> GetClientByIdAsync(int id);
        Task<Client> GetClientByEmail(string email);
        void CalculateClientRate(Client client);
        void CreateClient(Client client);
        void EditClient(Client client);
        void DeleteClient(Client client);
        Task<bool> SaveAllAsync();
    }
}
