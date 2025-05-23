using Ardalis.Specification;

namespace AdminLibrary.Admin.Core.MaterialModelAggregate.Specifications
{
    public  class SearchMaterialByIdSpec : Specification<MaterialsModel>
    {
        public SearchMaterialByIdSpec(int id)
        {
            Query.Where(x => x.Id == id)
                .OrderByDescending(x => x.CreateDate);

        }
    }
}
