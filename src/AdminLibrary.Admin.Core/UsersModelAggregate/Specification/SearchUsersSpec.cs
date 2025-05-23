using Ardalis.Specification;

namespace AdminLibrary.Admin.Core.UsersModelAggregate.Specification
{
    public class SearchUsersSpec : Specification<Users>
    {
        public SearchUsersSpec()
        {
            Query.Include(x => x.RolesVirtual)
                .OrderByDescending(x => x.CreateDate);
        }
    }
}
