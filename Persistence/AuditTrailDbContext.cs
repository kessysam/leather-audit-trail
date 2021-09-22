using ApplicationServices.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class AuditTrailDbContext : DbContext, IAuditTrailDbContext
    {
        public AuditTrailDbContext(DbContextOptions<AuditTrailDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuditTrailDbContext).Assembly);
        }

        public DbSet<ServiceAuditTrail> ServiceAuditTrails { get; set; }
        //public DbSet<TransactionAuditTrail> TransactionAuditTrails { get; set; }
        //public DbSet<PaymentGatewayAuditTrail> PaymentGatewayAuditTrails { get; set; }
        //public DbSet<PermissionAuditTrail> PermissionAuditTrails { get; set; }
    }
}
