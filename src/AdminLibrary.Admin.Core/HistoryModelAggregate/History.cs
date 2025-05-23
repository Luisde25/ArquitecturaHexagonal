using AdminLibrary.Admin.SharedKernel;
using AdminLibrary.Admin.SharedKernel.Interfaces;

namespace AdminLibrary.Admin.Core
{
    public class History : EntityBase<int>, IAggregateRoot
    {
        public History()
        {

        }
        public History(int materialId, int userId, string? observations, string movementType, DateTime? movementDate)
        {
            MaterialsId = materialId;
            UserId = userId;
            Observations = observations;
            MovementType = movementType;
            MovementDate = movementDate;
        }

        public int MaterialsId { get; set; }
        public int UserId { get; set; }
        public string? Observations { get; set; }
        public string MovementType { get; set; }
        public DateTime? MovementDate { get; set; }
        public virtual MaterialsModel MaterialsVirtual { get; set; } = new();
        public virtual Users UserVirtual { get; set; } = new();


    }
}
