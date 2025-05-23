using Ardalis.Specification;

namespace AdminLibrary.Admin.Core.RolesModelAggregate.Specifications
{
    public class SearchRolById : Specification<Roles>
    {
        public SearchRolById(int id)
        {
            Query.Where(X => X.Id == id)
                .OrderByDescending(x => x.CreateDate);
        }
    }
}
