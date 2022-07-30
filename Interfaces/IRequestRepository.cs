using HandyMan.Models;

namespace HandyMan.Interfaces
{
    public interface IRequestRepository
    {
        Task<IEnumerable<Request>> GetRequestsAsync();
        Task<Request> GetRequestByIdAsync(int id);
        Task<IEnumerable<Request>> GetRequestsByHandymanSsnAsync(int handymanSsn);
        Task<IEnumerable<Request>> GetActiveRequestsByHandymanSsnAsync(int handymanSsn);
        Task<IEnumerable<Request>> GetRequestsByClientIdAsync(int id);
        Task<Client> GetClientFromRequestByIdAsync(int id);
        Task<Handyman> GetHandymanFromRequestByIdAsync(int id);
        Task<bool> CheckRequestsByClienttoHandyman(int clientid, int handymanSsn);
        Task<bool> CheckRequestTimeDuplicate(Request request);
        Task<Request> GetPrevRequest(int id, int role);
        void CreateRequest(Request request);
        void CreatePaymentByRequestId(Request request);
        void CancelByRequestId(int id , int role);
        void EditRequest(Request request);
        void DeleteRequestById(int id);
        Task<bool> SaveAllAsync();
    }
}
