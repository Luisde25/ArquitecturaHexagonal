using Ardalis.Specification;

namespace AdminLibrary.Admin.Core.MovenmentsModelAggregate.Specifications
{
    public class SearchMaterialSpec : Specification<MaterialsModel>
    {
        public SearchMaterialSpec(int materialId)
        {
            Query
                .Where(m => m.Id == materialId)
                .OrderByDescending(m => m.CreateDate);
        }
    }
}
