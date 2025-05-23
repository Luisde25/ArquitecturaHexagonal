using Ardalis.Specification;

namespace AdminLibrary.Admin.Core.MovenmentsModelAggregate.Specifications
{
    public class MovementsSpec : Specification<Movements>
    {
        public MovementsSpec(int userId, string movement)
        {
            Query.Where(x => x.UserVirtual.Id == userId && x.MovementType == movement)
                .Include(u => u.UserVirtual)
                .Include(m => m.MaterialsVirtual)
                .OrderByDescending(x => x.CreateDate);
        
        }
    }
}
