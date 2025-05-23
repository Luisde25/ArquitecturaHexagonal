using AdminLibrary.Core.Dtos;

namespace AdminLibrary.Core.Interfaces
{
    public interface IUserService
    {
        Task<List<UsersUpdateDto>> CallingListUsers();
        Task<ResponseDto> CreateNewUser(UsersDto userControllerRequest);
        Task<ResponseDto> UpdateCurrentUser(UsersUpdateDto userControllerUpdate);
    }
}
