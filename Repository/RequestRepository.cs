using AutoMapper;
using HandyMan.Data;
using HandyMan.Dtos;
using HandyMan.Interfaces;
using HandyMan.Models;
using Microsoft.EntityFrameworkCore;

namespace HandyMan.Repository
{
    public class RequestRepository : IRequestRepository
    {
        private readonly Handyman_DBContext _context;

        public RequestRepository(Handyman_DBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Request>> GetRequestsAsync()
        {
            return await _context.Requests.ToListAsync();
        }

        public async Task<Request> GetRequestByIdAsync(int id)
        {
            return await _context.Requests.FindAsync(id);
        }

        public async Task<Request> GetPrevRequest(int id, int role)
        {
            Request request;
            //if role == 0 role is client if role ==1 then role is Handyman
            if (role == 0)
            {
                 request = await _context.Requests.Where(c => c.Client_ID == id && c.Request_Status == 2 && c.Handy_Rate==null && c.Request_Date < DateTime.Now).OrderByDescending(a=>a.Request_Order_Date).FirstOrDefaultAsync();
            }
            else
            {
                 request = await _context.Requests.Where(c => c.Handyman_SSN == id && c.Request_Status == 2 && c.Client_Rate == null && c.Request_Date < DateTime.Now).OrderByDescending(a => a.Request_Order_Date).FirstOrDefaultAsync();
            }
            return request;
        }


        public async void CreateRequest(Request request)
        {
            
            await _context.Requests.AddAsync(request);
        }

        public async void CreatePaymentByRequestId(Request request)
        {
            var requestPayment = request.Payments.FirstOrDefault();
            var handyman = _context.Handymen.Find(request.Handyman_SSN);
            var fixedRate = handyman.Handyman_Fixed_Rate;
            var client = _context.Clients.Find(request.Client_ID);
            var clientBalance = client.Balance;
            requestPayment.Payment_Amount = (int) (fixedRate - clientBalance);
            if (!requestPayment.Method)
            {
                handyman.Balance -= requestPayment.Payment_Amount;
            }
            else
            {
                client.Balance = 0;
            }
        }

        public void CancelByRequestId(int id, int role)//role =0 client , role==1 handyman , role==2 admin
        {
            var request = _context.Requests.Find(id);
            var handyman = _context.Handymen.Find(request.Handyman_SSN);
            var client = _context.Clients.Find(request.Client_ID);
            var payment = request.Payments.FirstOrDefault();
            //in case of cash
            if (!payment.Method)
            {
                handyman.Balance+=payment.Payment_Amount;
                request.Payments.Remove(payment);
            }
            //in case of credit
            else
            {
                var difference = payment.Payment_Amount - handyman.Handyman_Fixed_Rate;
                client.Balance += difference>0 ? 0 : -difference;
                request.Payments.Remove(payment);
            }
            //in case of penalty
            if (request.Request_Status == 2 && request.Request_Date.AddHours(-1) <= DateTime.Now && role!=2)
            {
                if (role == 0)
                {
                    client.Balance -= (int)(handyman.Handyman_Fixed_Rate * 0.1);
                    handyman.Balance += (int)(handyman.Handyman_Fixed_Rate * 0.1);

                }
                else if (role == 1)
                {
                    handyman.Balance -= (int)(handyman.Handyman_Fixed_Rate * 0.1);
                }
            }
            request.Request_Status = role == 1 ? 4 : 3; //if canceled by client then status is 3 , by handyman is 4
            _context.SaveChanges();
        }

        public async Task<bool> CheckRequestTimeDuplicate(Request request)
        {
            var req = await _context.Requests.Where(a => a.Handyman_SSN == request.Handyman_SSN && a.Request_Date == request.Request_Date && a.Request_Status==2).FirstOrDefaultAsync();
            if (req != null)
                return false;
            return true;
        }

        public void EditRequest(Request request)
        {
            _context.Entry(request).State = EntityState.Modified;
        }

        public void DeleteRequestById(int id)
        {
            _context.Requests.Remove(_context.Requests.Find(id));
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }




        ///Handyman Functions ///

        public async Task<IEnumerable<Request>> GetRequestsByHandymanSsnAsync(int handymanSsn)
        {
            return await _context.Requests.Where(c => c.Handyman_SSN == handymanSsn).ToListAsync();
        }

        public async Task<IEnumerable<Request>> GetActiveRequestsByHandymanSsnAsync(int handymanSsn)
        {
            return await _context.Requests.Where(c => c.Handyman_SSN == handymanSsn && c.Request_Status == 1).ToListAsync();
        }






        //Client Functions


        public async Task<IEnumerable<Request>> GetRequestsByClientIdAsync(int id)
        {
            var requests = await _context.Requests.Where(c => c.Client_ID == id).ToListAsync();
            foreach (var requ in requests)
            {
                if (requ.Request_Status == 1 && requ.Request_Order_Date.AddMinutes(30) < DateTime.Now)
                {
                    requ.Request_Status = 4;
                    EditRequest(requ);

                }
            }
            await SaveAllAsync();

            return requests;
        }



        public async Task<bool> CheckRequestsByClienttoHandyman(int clientid, int handymanSsn)
        {
            var check = await _context.Requests.Where(c => c.Client_ID == clientid && c.Handyman_SSN == handymanSsn && c.Request_Status == 1).ToListAsync();
            if (check.Count == 0)
                return true;
            return false;
        }

        //get client from request not to break validation of ClientController
        public async Task<Client> GetClientFromRequestByIdAsync(int id)
        {
            return await _context.Clients.FindAsync(id);
        }
        
        
        //get Handyman from request not to break validation of ClientController
        public async Task<Handyman> GetHandymanFromRequestByIdAsync(int id)
        {
            return await _context.Handymen.FindAsync(id);
        }


    }
}
