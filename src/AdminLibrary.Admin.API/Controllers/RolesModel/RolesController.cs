using AdminLibrary.Admin.API.Controllers.RolesModel;
using AdminLibrary.Core.Dtos;
using AdminLibrary.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdminLibrary.Controllers.RolesModel
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController(
        IRoleService rolesService
        ) : Controller
    {
      
        private readonly IRoleService _roles = rolesService;

        #region Obtener lista de roles
        [HttpGet("/GetRoles")]
        public async Task<List<RolesUpdateDto>> GetRoles()
        {
            return await _roles.ListRoles();
        }
        #endregion

        #region Creación de un nuevo rol
        [HttpPost("/Create")]
        public async Task<ResponseDto> CreateRole(
            RolesRequest roleControllerRequest
            )
        {
            var request = new RolesDto(
            
                roleControllerRequest.Name,
                roleControllerRequest.Description,
                roleControllerRequest.status
            );

            return await _roles.CreateNewRol(request);
        }

        #endregion

        #region Actual un rol existente
        [HttpPut("/Update/{id}")]
        public async Task<ResponseDto> UpdateMaterial(
          RolesRequestUpdate roleControllerRequest
          )
        {
            var request = new RolesUpdateDto(

               roleControllerRequest.Id,
               roleControllerRequest.Name,
               roleControllerRequest.Description,
               roleControllerRequest.status
           );

            return await _roles.UpdateCurrentRol(request);
        }
        #endregion

    }
}
