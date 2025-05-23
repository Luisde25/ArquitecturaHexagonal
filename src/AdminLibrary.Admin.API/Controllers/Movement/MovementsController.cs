using AdminLibrary.Admin.API.Controllers.Movement;
using AdminLibrary.Admin.Core.HistoryModelAggregate.Dto;
using AdminLibrary.Core.Dtos;
using AdminLibrary.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdminLibrary.Controllers.Movement
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovementsController(
         IHistoryService materialHistoryService,
         IMovementsService materialMovementsService
        ) : Controller
    {
       
        private readonly IHistoryService _materialHistoryService = materialHistoryService;
        private readonly IMovementsService _materialMovementsService = materialMovementsService;
    
     

        #region Lista de libros
        [HttpGet("/GetLoans")]
        public async Task<List<HistoryDto>> GetMovements()
        {
            return await _materialMovementsService.ListMovements();
        }
        #endregion

        #region Historial de materiales
        [HttpGet("/history")]
        public async Task<List<HistoryDto>> History()
        {
            return await _materialHistoryService.HistoryMovements();
        }
        #endregion

        #region Prestar libros
        [HttpPost("/Loans")]
        public async Task<ResponseDto> Movements(
            MovementsRequest movementsControllerRequest
            )
        {
            var request = new MovementsDto(
                movementsControllerRequest.MaterialId,
                movementsControllerRequest.UserId,
                movementsControllerRequest.Observations,
                movementsControllerRequest.MovementType ?? "PRESTAMO",
                DateTime.UtcNow.AddHours(-5)
                );

            return await _materialMovementsService.Loans(request);
        }
        #endregion

        #region Devolver libros
        [HttpDelete("/return")]
        public async Task<ResponseDto> DeleteLoans(
            MovementsRequest movementsControllerRequest
            )
        {
            var request = new MovementsDto(
             movementsControllerRequest.MaterialId,
             movementsControllerRequest.UserId,
             movementsControllerRequest.Observations,
             movementsControllerRequest.MovementType ?? "DEVOLUCIÓN",
             DateTime.UtcNow.AddHours(-5)
             );

            return await _materialMovementsService.Return(request);
        }
        #endregion

        
    }
}
