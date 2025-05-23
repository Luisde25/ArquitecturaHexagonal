using AdminLibrary.Admin.SharedKernel;
using AdminLibrary.Admin.SharedKernel.Interfaces;

namespace AdminLibrary.Admin.Core
{
    public class Response : EntityBase<int>, IAggregateRoot
    {
        public Response() { }
        public Response(
            string code,
            string message,
            string status)
        {
            Code = code;
            Message = message;
            Status = status;
        }
        public string? Code { get; set; }
        public string? Message { get; set; }
        public string? Status { get; set; }

    }
}
