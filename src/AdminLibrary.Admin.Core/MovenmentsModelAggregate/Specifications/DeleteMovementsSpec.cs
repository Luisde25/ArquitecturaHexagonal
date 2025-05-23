using Ardalis.Specification;

namespace AdminLibrary.Admin.Core.MovenmentsModelAggregate.Specifications
{
    public class DeleteMovementsSpec : Specification<Movements>
    {

        public DeleteMovementsSpec(int userId, int materialId)
        {
            Query.Where(x => x.UserVirtual.Id == userId && x.MaterialsVirtual.Id == materialId)
                .Include(u => u.UserVirtual)
                .Include(m => m.MaterialsVirtual)
             .OrderByDescending(x => x.CreateDate);
        }
    }
}
