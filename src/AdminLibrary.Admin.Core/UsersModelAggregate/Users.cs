using AdminLibrary.Admin.SharedKernel;
using AdminLibrary.Admin.SharedKernel.Interfaces;

namespace AdminLibrary.Admin.Core
{
    public class Users : EntityBase<int>, IAggregateRoot
    {
        public Users()
        {
            
        }
        public Users(
            string firtsName,
            string? middleName,
            string firtsLastName,
            string secondLastName,
            string typeIdentification,
            string numberIdentification,
            bool status,
            string? userName,
            string userType
            )
        {
            FirtsName = firtsName;
            MiddleName = middleName;
            FirtsLastName = firtsLastName;
            SecondLastName = secondLastName;
            TypeIdentification = typeIdentification;
            NumberIdentification = numberIdentification;
            Status = status;
            UserName = userName;
            UserType = userType;
        }

        public string FirtsName { get; set; } 
        public string? MiddleName { get; set; } 
        public string FirtsLastName { get; set; }
        public string? SecondLastName { get; set; }
        public string TypeIdentification { get; set; } 
        public string NumberIdentification { get; set; } 
        public bool Status { get; set; }
        public string? UserName { get; set; }
        public string UserType { get; set; } 
        public virtual ICollection<Movements> MovementsVirtual { get; set; } = null!;
        public virtual ICollection<History> HistoryVirtual { get; set; } = null!;
        public virtual ICollection<Roles> RolesVirtual { get; set; } = null!;

    }
}
