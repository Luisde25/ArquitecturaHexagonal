using Ardalis.Specification;

namespace AdminLibrary.Admin.Core.RolesModelAggregate.Specifications
{
    public class SeachRolNameSpec : Specification<Roles>
    {
        public SeachRolNameSpec(string rolName)
        {
            Query.Where(x => x.Name == rolName)
                .OrderByDescending(x => x.CreateDate);
        }
    }
}
