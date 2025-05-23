using Ardalis.Specification;

namespace AdminLibrary.Admin.Core.MaterialModelAggregate.Specifications
{
    public class SearchUsersSpec : Specification<Users>
    {
        public SearchUsersSpec(int userId)
        {
            Query.Where(u => u.Id == userId)
                .OrderByDescending(c => c.CreateDate);
        }
    }
}
