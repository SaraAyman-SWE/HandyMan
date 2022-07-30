using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using HandyMan.Data;
using HandyMan.Interfaces;
using HandyMan.Models;
using Microsoft.EntityFrameworkCore;

namespace HandyMan.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly Handyman_DBContext _context;
        public ClientRepository(Handyman_DBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Client>> GetClientsAsync()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            var request = client.Requests.Where(a => a.Request_Status == 2).OrderByDescending(a => a.Request_Date).FirstOrDefault();
            if (request != null && request.Request_Date < DateTime.Now)
                client.Balance = 0;
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task<Client> GetClientByEmail(string email)
        {

            var client = await _context.Clients.SingleOrDefaultAsync(c => c.Client_Email == email);
            return client;
        }

        public void CalculateClientRate(Client client)
        {
            var requests = client.Requests;
            if(requests != null)
            {
                double sum = 0, count = 0;
                foreach (var req in requests)
                {
                    if (req.Client_Rate != null)
                    {
                        sum += (int)req.Client_Rate;
                        count++;
                    }
                }
                client.Rating = sum / count;
            }
        }

        public async void CreateClient(Client client)
        {
            await _context.Clients.AddAsync(client);
        }
        public void EditClient(Client client)
        {
            _context.Entry(client).State = EntityState.Modified;
        }

        public void DeleteClient(Client client)
        {
            _context.Clients.Remove(client);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
