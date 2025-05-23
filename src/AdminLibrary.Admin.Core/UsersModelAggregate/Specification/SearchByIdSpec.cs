using Ardalis.Specification;

namespace AdminLibrary.Admin.Core.UsersModelAggregate.Specification
{
    public class SearchByIdSpec : Specification<Users>
    {
        public SearchByIdSpec(int id)
        {
            Query.Where(x => x.Id == id)
                .OrderByDescending(x => x.CreateDate);
        }
    }
}
