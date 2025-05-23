using AdminLibrary.Admin.API.Controllers.UsersModel;
using AdminLibrary.Core.Dtos;
using AdminLibrary.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdminLibrary.Controllers.UsersModel
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(
         IUserService userService
        ) : Controller
    {
        
        private readonly IUserService _userService = userService;

        #region Lista de usuarios que tienen un rol asignado
        [HttpGet("GetUsers")]
        public async Task<List<UsersUpdateDto>> GetUsers()
        {

            return await _userService.CallingListUsers();
        }
        #endregion

        #region Creación de un nuevo usuario
        [HttpPost("Create")]
        public async Task<ResponseDto> CreateUser(
           UsersRequest userControllerRequest
           )
        {
            var request = new UsersDto(
                userControllerRequest.FirtsName,
                userControllerRequest.MiddleName,
                userControllerRequest.FirtsLastName,
                userControllerRequest.SecondLastName,
                userControllerRequest.TypeIdentification,
                userControllerRequest.NumberIdentification,
                userControllerRequest.Status,
                userControllerRequest.UserName,
                userControllerRequest.UserType,
                userControllerRequest.Rol
                );

            return await _userService.CreateNewUser(request);
        }

        #endregion

        #region Actualizar usuario existe
        [HttpPut("Update/{id}")]
        public async Task<ResponseDto> UpdateUser(
          UsersRequestUpdate userControllerUpdate
          )
        {
            var request = new UsersUpdateDto(
              userControllerUpdate.Id,
              userControllerUpdate.FirtsName,
              userControllerUpdate.MiddleName,
              userControllerUpdate.FirtsLastName,
              userControllerUpdate.SecondLastName,
              userControllerUpdate.TypeIdentification,
              userControllerUpdate.NumberIdentification,
              userControllerUpdate.Status,
              userControllerUpdate.UserName,
              userControllerUpdate.UserType,
              userControllerUpdate.Rol
          );

            return await _userService.UpdateCurrentUser(request);
        }
        #endregion 

     
    }
}
