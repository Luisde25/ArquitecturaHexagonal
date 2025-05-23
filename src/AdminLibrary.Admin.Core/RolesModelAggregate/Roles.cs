using AdminLibrary.Admin.SharedKernel;
using AdminLibrary.Admin.SharedKernel.Interfaces;

namespace AdminLibrary.Admin.Core
{
    public class Roles : EntityBase<int>, IAggregateRoot
    {
        public Roles()
        {

        }
        public Roles(string name, string desciption, bool status)
        {
            Name = name;
            Description = desciption;
            Status = status;
        }

        public string Name { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }

       
    }
}
