using AdminLibrary.Admin.Core;
using AdminLibrary.Admin.Core.HistoryModelAggregate.Dto;
using AdminLibrary.Admin.Core.HistoryModelAggregate.Specification;
using AdminLibrary.Admin.SharedKernel.Interfaces;
using AdminLibrary.Core.Interfaces;

namespace AdminLibrary.Core.Services
{
    public class HistoryService : IHistoryService
    {
        //Este repositorio es una libreria llamada Ardalis 
        private readonly IRepository<History> _historyRepository;
        public HistoryService(IRepository<History> historyRepository)
        {
            _historyRepository = historyRepository;
        }

        public async Task<List<HistoryDto>> HistoryMovements()
        {

            var listHistory = await _historyRepository.ListAsync(new SearchHistorySpec());

            if (listHistory.Count == 0)
            {
                return [];
            }

            var movements = listHistory.Select(m => new HistoryDto
            {
                Title = m.MaterialsVirtual?.Title,
                UserName = m.UserVirtual?.UserName,
                Observations = m.Observations,
                MovenmentType = m.MovementType,
                MovenmentDate = m.MovementDate
            }).ToList();


            return movements;
        }
    }
}
