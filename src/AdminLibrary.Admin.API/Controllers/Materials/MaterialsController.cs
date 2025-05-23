using AdminLibrary.Admin.API.Controllers.Materials;
using AdminLibrary.Core.Dtos;
using AdminLibrary.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdminLibrary.Controllers.audioVisual
{

    [ApiController]
    [Route("api/[controller]")]
    public class MaterialsController(
            IMaterialService materialService
            ) : Controller
    {
        private readonly IMaterialService _materialService = materialService;

        #region Lista de materiales
        [HttpGet("GetMaterials")]
        public async Task<List<MaterialUpdateDto>> GetMaterials()
        {
            return await _materialService.ListMaterials();
        }
        #endregion

        #region Registrar un nuevo libro
        [HttpPost("Register")]
        public async Task<ResponseDto> CreateMaterial(
            MaterialsRequest materialControllerRequest
            )
        {
            var request = new MaterialDto(
                  materialControllerRequest.Identifier,
                  materialControllerRequest.Title,
                  materialControllerRequest.userId,
                  DateTime.UtcNow.AddHours(-5),
                  materialControllerRequest.RegisterQuantity,
                  materialControllerRequest.RegisterQuantity
                );


            return await _materialService.RegisterNewMaterial(request);
        }
        #endregion

        #region Actualizar libro existente
        [HttpPut("Update{id}")]
        public async Task<ResponseDto> UpdateMaterial(
            MaterialsRequestUpdate materialControllerRequest
            )
        {
            var request = new MaterialUpdateDto(
                 materialControllerRequest.Id,
                materialControllerRequest.Identifier,
                materialControllerRequest.Title,
                DateTime.UtcNow.AddHours(-5),
                materialControllerRequest.RegisterQuantity,
                materialControllerRequest.RegisterQuantity
              );

            return await _materialService.UpdateCurrentNewMaterial(request);
        }
        #endregion
    }
}
