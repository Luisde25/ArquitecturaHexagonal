using AdminLibrary.Core.Dtos;

namespace AdminLibrary.Core.Interfaces
{
    public interface IMaterialService
    {
        Task<List<MaterialUpdateDto>> ListMaterials();
        Task<ResponseDto> RegisterNewMaterial(MaterialDto materialControllerRequest);
        Task<ResponseDto> UpdateCurrentNewMaterial(MaterialUpdateDto materialControllerRequest);
    }
}
