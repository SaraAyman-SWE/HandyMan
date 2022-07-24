using HandyMan.Models;

namespace HandyMan.Interfaces
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetClientsAsync();
        Task<Client> GetClientByIdAsync(int id);
        Task<Client> GetClientByEmail(string email);
        void CreateClient(Client client);
        void EditClient(Client client);
        void DeleteClientById(int id);
        Task<bool> SaveAllAsync();
    }
}
