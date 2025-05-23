using AdminLibrary.Admin.Core.HistoryModelAggregate.Dto;
using AdminLibrary.Core.Dtos;

namespace AdminLibrary.Core.Interfaces
{
    public interface IHistoryService
    {
        Task<List<HistoryDto>> HistoryMovements();
    }
}
