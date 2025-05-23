using AdminLibrary.Core.Dtos;

namespace AdminLibrary.Core.Interfaces
{
    public interface IRoleService
    {
        Task<List<RolesUpdateDto>> ListRoles();
        Task<ResponseDto> CreateNewRol(RolesDto roleControllerRequest);
        Task<ResponseDto> UpdateCurrentRol(RolesUpdateDto roleControllerRequest);
    }
}
