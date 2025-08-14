using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TABP.Domain.Entities;
using TABP.Domain.Entities.Common;
namespace TABP.Persistence.Interceptors
{
    public sealed class SoftDeleteInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            if (eventData.Context is null)
            {
                return base.SavingChangesAsync(
                    eventData, result, cancellationToken); //skip and let EF do its normal job
            }
            IEnumerable<EntityEntry<SoftDeletable>> entries =
             eventData
                 .Context
                 .ChangeTracker
                 .Entries<SoftDeletable>()
                 .Where(e => e.State == EntityState.Deleted);
            foreach (EntityEntry<SoftDeletable> softDeletable in entries)
            {
                softDeletable.State = EntityState.Modified;
                softDeletable.Entity.IsDeleted = true;
                softDeletable.Entity.DeletedOn = DateTime.UtcNow;
                // Handle cascading soft deletes
                switch (softDeletable.Entity)
                {
                    case RoomClass roomClass:
                        var relatedRooms = eventData.Context.Entry(roomClass)
                            .Collection(rc => rc.Rooms)
                            .Query()
                            .Where(r => !r.IsDeleted)
                            .ToList();

                        foreach (var room in relatedRooms)
                        {
                            room.IsDeleted = true;
                            room.DeletedOn = DateTime.UtcNow;
                            eventData.Context.Entry(room).State = EntityState.Modified;
                        }
                        break;

                        // You can add similar blocks for other cascade chains
                }
            }
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}