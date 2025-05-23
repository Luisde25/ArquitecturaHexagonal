using Ardalis.Specification;

namespace AdminLibrary.Admin.Core.UsersModelAggregate.Specification
{
    public class SearchByNumberIdentificationSpec : Specification<Users>
    {
        public SearchByNumberIdentificationSpec(string number)
        {
            Query.Where(x => x.NumberIdentification == number)
                .OrderByDescending(x => x.CreateDate);
        }
    }
}
