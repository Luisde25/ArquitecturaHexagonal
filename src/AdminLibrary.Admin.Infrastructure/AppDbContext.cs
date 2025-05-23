using AdminLibrary.Admin.SharedKernel;
using AdminLibrary.Admin.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AdminLibrary.Models
{
    public class AppDbContext: DbContext
    {
        private readonly IDomainEventDispatcher _dispatcher;
        public AppDbContext(DbContextOptions<AppDbContext> options, IDomainEventDispatcher dispatcher)
        : base(options)
        {
            _dispatcher = dispatcher;
        }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            if(_dispatcher == null) 
                return result;

            var entitiesWithEvents = ChangeTracker.Entries<EntityBase<int>>()
                .Select(e => e.Entity)
                .Where(e => e.DomainEvents.Any())
                .ToArray();

            await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);

            return result;  
        }

        public override int SaveChanges()
        {
            return base.SaveChangesAsync().GetAwaiter().GetResult();
        }

      
    }
}
