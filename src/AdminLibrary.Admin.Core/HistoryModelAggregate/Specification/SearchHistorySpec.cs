using Ardalis.Specification;

namespace AdminLibrary.Admin.Core.HistoryModelAggregate.Specification
{
    public class SearchHistorySpec : Specification<History>
    {
        public SearchHistorySpec()
        {
            Query
                .Include(u => u.UserVirtual)
                .Include(m => m.MaterialsVirtual)
            .OrderByDescending(c => c.CreateDate);
        }
    }
}
