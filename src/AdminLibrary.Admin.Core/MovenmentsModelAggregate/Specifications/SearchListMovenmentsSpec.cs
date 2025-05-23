using Ardalis.Specification;

namespace AdminLibrary.Admin.Core.MovenmentsModelAggregate.Specifications
{
    public class SearchListMovenmentsSpec : Specification<Movements>
    {
        public SearchListMovenmentsSpec()
        {
            Query.Include(u => u.UserVirtual)
                .Include(m => m.MaterialsVirtual)
                .OrderByDescending(x => x.CreateDate);
        }
    }
}
