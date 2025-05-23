using AdminLibrary.Admin.SharedKernel;
using AdminLibrary.Admin.SharedKernel.Interfaces;

namespace AdminLibrary.Admin.Core
{
    public class Movements : EntityBase<int>, IAggregateRoot
    {
        public int MaterialsId { get; set; }
        public int UserId { get; set; }
        public string? Observations { get; set; }
        public string MovementType { get; set; } = string.Empty;
        public DateTime? MovementDate { get; set; }
        public virtual MaterialsModel MaterialsVirtual { get; set; } = null!;
        public virtual Users UserVirtual { get; set; } = null!;
    
    }
}
