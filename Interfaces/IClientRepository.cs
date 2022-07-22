using HandyMan.Models;

namespace HandyMan.Interfaces
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetClientsAsync();
        Task<Client> GetClientByIdAsync(int id);
        void CreateClient(Client client);
        void EditClient(Client client);
        void DeleteClientById(int id);
        Task<bool> SaveAllAsync();
    }
}
