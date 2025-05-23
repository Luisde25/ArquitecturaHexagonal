using Ardalis.Specification;

namespace AdminLibrary.Admin.Core.MovenmentsModelAggregate.Specifications
{
    public class SearchUserSpec : Specification<Users>
    {
        public SearchUserSpec(int userId)
        {
            Query.Where(u => u.Id == userId)
                .Include(c => c.RolesVirtual)
                .OrderByDescending(c => c.CreateDate);
        }
    }
}
