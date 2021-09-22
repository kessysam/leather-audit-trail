using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ApplicationServices.Interfaces
{
    public interface IAuditTrailDbContext
    {
        DbSet<Domain.ServiceAuditTrail> ServiceAuditTrails { get; set; }


        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
        int SaveChanges();
    }
}
