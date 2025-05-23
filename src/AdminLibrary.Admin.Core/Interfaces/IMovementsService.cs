using AdminLibrary.Admin.Core.HistoryModelAggregate.Dto;
using AdminLibrary.Core.Dtos;

namespace AdminLibrary.Core.Interfaces
{
    public interface IMovementsService
    {
        Task<List<HistoryDto>> ListMovements();
        Task<ResponseDto> Loans(MovementsDto movementsDto);
        Task<ResponseDto> Return(MovementsDto movementsDto);
    }
}
