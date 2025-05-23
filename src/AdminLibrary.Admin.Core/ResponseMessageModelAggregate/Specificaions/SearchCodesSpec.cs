using Ardalis.Specification;

namespace AdminLibrary.Admin.Core.ResponseMessageModelAggregate.Specificaions
{
    public class SearchCodesSpec : Specification<Response>
    {
        public SearchCodesSpec(string code)
        {
            Query.Where(r => r.Code == code);
        }
    }
}
