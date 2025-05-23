using AdminLibrary.Admin.SharedKernel;
using AdminLibrary.Admin.SharedKernel.Interfaces;

namespace AdminLibrary.Admin.Core
{
    public class MaterialsModel : EntityBase<int>, IAggregateRoot
    {
        public MaterialsModel()
        {

        }
        public MaterialsModel(
            string identifier,
            string title,
            DateTime registerDate,
            int registerQuantity,
            int currentQuantity
            )
        {
            Identifier = identifier;
            Title = title;
            RegisterDate = registerDate;
            RegisterQuantity = registerQuantity;
            CurrentQuantity = currentQuantity;
        }
        public string Identifier { get; set; }
        public string Title { get; set; }
        public DateTime RegisterDate { get; set; }
        public int RegisterQuantity { get; set; }
        public int CurrentQuantity { get; set; }
        public virtual ICollection<Movements> MovementsVirtual { get; set; } = null!;
        public virtual ICollection<History> HistoryVirtual { get; set; } = null!;

        
    }
}
