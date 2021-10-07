using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace ApplicationServices.Interfaces
{
    public interface IAuditTrailService
    {
        Task<IEnumerable<ServiceAuditTrail>> GetMultipleAsync(string query);
        Task<ServiceAuditTrail> GetAsync(string id);
        Task AddAsync(ServiceAuditTrail item);
        Task UpdateAsync(string id, ServiceAuditTrail item);
        Task DeleteAsync(string id);
    }
}
