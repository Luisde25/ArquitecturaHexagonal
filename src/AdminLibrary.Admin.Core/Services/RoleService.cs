using AdminLibrary.Admin.Core;
using AdminLibrary.Admin.Core.ResponseMessageModelAggregate.Specificaions;
using AdminLibrary.Admin.Core.RolesModelAggregate.Specifications;
using AdminLibrary.Admin.SharedKernel.Interfaces;
using AdminLibrary.Constants;
using AdminLibrary.Core.Dtos;
using Microsoft.EntityFrameworkCore;

namespace AdminLibrary.Core.Interfaces
{
    public class RoleService : IRoleService
    {
        private readonly IRepository<Roles> _rolesRepository;
        private readonly IRepository<Response> _responseRepository;
        public RoleService(
            IRepository<Roles> rolesRepository,
            IRepository<Response> responseRepository
            )
        {
            _rolesRepository = rolesRepository;
            _responseRepository = responseRepository;
        }
        public async Task<List<RolesUpdateDto>> ListRoles()
        {
            var listRoles = await _rolesRepository.ListAsync();

            if (listRoles.Count == 0)
                return [];


            return listRoles.Select(m => new RolesUpdateDto(
                                        m.Id,
                                        m.Name,
                                        m.Description,
                                        m.Status
                                    )).ToList();
        }

        public async Task<ResponseDto> CreateNewRol(RolesDto roleControllerRequest)
        {
            ResponseDto response = new();
            try
            {
                if (string.IsNullOrEmpty(roleControllerRequest.Name) ||
                    string.IsNullOrEmpty(roleControllerRequest.Description))
                {
                    var success2 = await _responseRepository.FirstOrDefaultAsync(new SearchCodesSpec(Codes.requestInvalid));

                    response.Code = success2?.Code ?? string.Empty;
                    response.Message = success2?.Message;

                    return response;
                }

                var isExist = await _rolesRepository.FirstOrDefaultAsync(new SeachRolNameSpec(roleControllerRequest.Name));

                if (isExist != null)
                {
                    var success1 = await _responseRepository.FirstOrDefaultAsync(new SearchCodesSpec(Codes.existsRol));

                    response.Code = success1?.Code ?? string.Empty;
                    response.Message = success1?.Message;

                    return response;

                }

                var roles = new Roles(roleControllerRequest.Name,
                                      roleControllerRequest.Description,
                                      roleControllerRequest.Status);

                await _rolesRepository.AddAsync(roles);
                
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

     
        public async Task<ResponseDto> UpdateCurrentRol(RolesUpdateDto roleControllerRequest)
        {
            ResponseDto response = new();
            try
            {
                DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(ConstantsApi.utcNow, ConstantsApi.timeZoneInfo);

                var isExist = await _rolesRepository.FirstOrDefaultAsync(new SearchRolById(roleControllerRequest.Id));

                if (isExist != null)
                {
                    isExist.Name = roleControllerRequest.Name;
                    isExist.Description = roleControllerRequest.Description;
                    isExist.Status = roleControllerRequest.Status;
                    isExist.UpdateDate = localTime;
                    isExist.UpdateUser = "Admin";

                    await _rolesRepository.UpdateAsync(isExist);
                    
                        var success = await _responseRepository.FirstOrDefaultAsync(new SearchCodesSpec(Codes.success));
                        response.Code = success?.Code ?? string.Empty;
                        response.Message = success?.Message;
                   
                }
                else
                {
                    var failed = await _responseRepository.FirstOrDefaultAsync(new SearchCodesSpec(Codes.updateFailed));
                    response.Code = failed?.Code ?? string.Empty;
                    response.Message = failed?.Message;
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
