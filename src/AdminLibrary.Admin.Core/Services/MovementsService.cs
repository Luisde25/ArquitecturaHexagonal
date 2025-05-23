using AdminLibrary.Admin.Core;
using AdminLibrary.Admin.Core.HistoryModelAggregate.Dto;
using AdminLibrary.Admin.Core.MovenmentsModelAggregate.Specifications;
using AdminLibrary.Admin.Core.ResponseMessageModelAggregate.Specificaions;
using AdminLibrary.Admin.SharedKernel.Interfaces;
using AdminLibrary.Constants;
using AdminLibrary.Core.Dtos;
using Microsoft.EntityFrameworkCore;

namespace AdminLibrary.Core.Interfaces
{
    public class MovementsService : IMovementsService
    {
        //Este repositorio es una libreria llamada Ardalis 
        private readonly IRepository<Movements> _movenmentsRepository;
        private readonly IRepository<History> _historyRepository;
        private readonly IRepository<Response> _responseRepository;
        private readonly IRepository<MaterialsModel> _materialsModelRepository;
        private readonly IRepository<Users> _userRepository;
        public MovementsService(
            IRepository<Movements> movenmentsRepository, 
            IRepository<Response> responseRepository,
            IRepository<MaterialsModel> materialsModelRepository,
            IRepository<Users> userRepository
            )
        {
            _movenmentsRepository = movenmentsRepository;
            _responseRepository = responseRepository;   
            _materialsModelRepository = materialsModelRepository;
            _userRepository = userRepository;
        }

        public async Task<List<HistoryDto>> ListMovements()
        {

            var listMovements = await _movenmentsRepository.ListAsync(new SearchListMovenmentsSpec());

            if (listMovements.Count == 0)
            {
                return [];
            }

            var movements = listMovements.Select(m => new HistoryDto
                {
                    Title = m.MaterialsVirtual?.Title,
                    UserName = m.UserVirtual?.UserName,
                    Observations = m.Observations,
                    MovenmentType = m.MovementType,
                    MovenmentDate = m.MovementDate
                }).ToList();


            return movements;
        }

        public async Task<ResponseDto> Loans(MovementsDto movementsDto)
        {
            ResponseDto response = new();
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(ConstantsApi.utcNow, ConstantsApi.timeZoneInfo);
            try
            {
                if (string.IsNullOrEmpty(movementsDto.MovementType))
                {
                    var success0 = await _responseRepository.FirstOrDefaultAsync(new SearchCodesSpec(Codes.requestInvalid));

                    response.Code = success0?.Code ?? string.Empty;
                    response.Message = success0?.Message;

                    return response;
                }

                var material = await _materialsModelRepository.FirstOrDefaultAsync(new SearchMaterialSpec(movementsDto.MaterialId));

                if (material == null)
                {
                    var success1 = await _responseRepository.FirstOrDefaultAsync(new SearchCodesSpec(Codes.MaterialNoFound));

                    response.Code = success1?.Code ?? string.Empty;
                    response.Message = success1?.Message;

                    return response;
                }

                var user = await _userRepository.FirstOrDefaultAsync(new SearchUserSpec(movementsDto.UserId));

                if (user == null)
                {
                    var success2 =  await _responseRepository.FirstOrDefaultAsync(new SearchCodesSpec(Codes.UserNoFound));

                    response.Code = success2?.Code ?? string.Empty;
                    response.Message = success2?.Message;

                    return response;
                }

                var listMovements = await _movenmentsRepository.ListAsync(new MovementsSpec(user.Id, ConstantsApi.Loans));

                if (listMovements.Select(x => x.MaterialsId).Count() > material.CurrentQuantity)
                {
                    var success3 = await  _responseRepository.FirstOrDefaultAsync(new SearchCodesSpec(Codes.LimitMaterial));

                    response.Code = success3?.Code ?? string.Empty;
                    response.Message = success3?.Message;

                    return response;
                }


                if (movementsDto.MovementType.ToLower() == ConstantsApi.Loans.ToLower() && 
                    user.UserType.ToLower() == ConstantsApi.Student.ToLower() && listMovements.Select(x => x.UserId).Count() > ConstantsApi.CantStudent)
                {
                    var success4 = await _responseRepository.FirstOrDefaultAsync(new SearchCodesSpec(Codes.MaxEstudents));
                    response.Code = success4?.Code ?? string.Empty;
                    response.Message = success4?.Message;

                    return response;

                }
                else if (movementsDto.MovementType.ToLower() == ConstantsApi.Loans.ToLower() &&
                    user.UserType.ToLower() == ConstantsApi.Teacher.ToLower() && listMovements.Select(x => x.UserId).Count() > ConstantsApi.CantTeacher)
                {
                    var success5 = await _responseRepository.FirstOrDefaultAsync(new SearchCodesSpec(Codes.MaxProf));
                    response.Code = success5?.Code ?? string.Empty;
                    response.Message = success5?.Message;

                    return response;
                }
                else if (movementsDto.MovementType.ToLower() == ConstantsApi.Loans.ToLower() && 
                    user.UserType.ToLower() == ConstantsApi.Admin.ToLower() && listMovements.Select(x => x.UserId).Count() > ConstantsApi.CantAdmin)
                {
                    var success6 = await _responseRepository.FirstOrDefaultAsync(new SearchCodesSpec(Codes.MaxAdmin));
                    response.Code = success6?.Code ?? string.Empty;
                    response.Message = success6?.Message;

                    return response;
                }

                var movimiento = new Movements
                {
                    MaterialsId = material.Id,
                    UserId = user.Id,
                    MovementType = movementsDto.MovementType,
                    MovementDate = localTime,
                    Observations = movementsDto.Observations
                };

                await _movenmentsRepository.AddAsync(movimiento);

                var historyMaterial = new History(material.Id,
                                                            user.Id,
                                                            "Prestamo de libro de " + material.Title,
                                                            ConstantsApi.Loans,
                                                            localTime
                                                        );

                await _historyRepository.AddAsync(historyMaterial);

                var success = await _responseRepository.FirstOrDefaultAsync(new SearchCodesSpec(Codes.success));

                response.Code = success?.Code ?? string.Empty;
                response.Message = success?.Message;

            }
            catch (Exception ex)
            {
                var failed = await _responseRepository.FirstOrDefaultAsync(new SearchCodesSpec(Codes.failed));
                response.Code = failed?.Code ?? string.Empty;
                response.Message = failed?.Message + ex.Message;
            }

            return response;
        }

        public async Task<ResponseDto> Return(MovementsDto movementsDto)
        {
            ResponseDto response = new();
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(ConstantsApi.utcNow, ConstantsApi.timeZoneInfo);
            try
            {
                if (movementsDto.UserId == 0 && movementsDto.MaterialId == 0)
                {
                    var success = await _responseRepository.FirstOrDefaultAsync(new SearchCodesSpec(Codes.requestInvalid));

                    response.Code = success?.Code ?? string.Empty;
                    response.Message = success?.Message;

                    return response;
                }

                var user = await _userRepository.FirstOrDefaultAsync(new SearchUserSpec(movementsDto.UserId)); 
                var material = await _materialsModelRepository.FirstOrDefaultAsync(new SearchMaterialSpec(movementsDto.MaterialId));

                if (user != null && material != null)
                {
                    var deleteMovement = await _movenmentsRepository.FirstOrDefaultAsync(new DeleteMovementsSpec(user.Id, material.Id));

                    if (deleteMovement != null)
                    {
                       await _movenmentsRepository.DeleteAsync(deleteMovement);

                        var historyMaterial = new History(material.Id,
                                                         user.Id,
                                                         "Se devuelve el libro de " + material.Title,
                                                         ConstantsApi.Return,
                                                         localTime
                                                     );

                        await _historyRepository.AddAsync(historyMaterial);

                        var success = await _responseRepository.FirstOrDefaultAsync(new SearchCodesSpec(Codes.MateriaReturn));

                        response.Code = success?.Code ?? string.Empty;
                        response.Message = success?.Message;
                    }
                    else
                    {
                        var success = await _responseRepository.FirstOrDefaultAsync(new SearchCodesSpec(Codes.MaterialNoFound));

                        response.Code = success?.Code ?? string.Empty;
                        response.Message = success?.Message;
                    }

                }
                else
                {
                    var success = await _responseRepository.FirstOrDefaultAsync(new SearchCodesSpec(Codes.MaterialNoFound));

                    response.Code = success?.Code ?? string.Empty;
                    response.Message = success?.Message;
                }

            }
            catch (Exception ex)
            {
                var failed = await _responseRepository.FirstOrDefaultAsync(new SearchCodesSpec(Codes.failed));
                response.Code = failed?.Code ?? string.Empty;
                response.Message = failed?.Message + ex.Message;
            }
            return response;
        }
    }
}
