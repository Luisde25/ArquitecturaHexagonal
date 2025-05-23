using Ardalis.Specification;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace AdminLibrary.Admin.Core.MaterialModelAggregate.Specifications
{
    public class SearchMaterialSpec:Specification<MaterialsModel>
    {
        public SearchMaterialSpec(string identifier)
        {
            Query.Where(x => x.Identifier == identifier)
            .OrderByDescending(c => c.CreateDate);
        }
    }
}

