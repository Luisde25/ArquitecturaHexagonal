using AdminLibrary.Admin.Core;
using AdminLibrary.Admin.Core.MovenmentsModelAggregate.Specifications;
using AdminLibrary.Admin.Core.ResponseMessageModelAggregate.Specificaions;
using AdminLibrary.Admin.Core.UsersModelAggregate.Specification;
using AdminLibrary.Admin.SharedKernel.Interfaces;
using AdminLibrary.Constants;
using AdminLibrary.Core.Dtos;

namespace AdminLibrary.Core.Interfaces
{
    public class UserService : IUserService
    {
        private readonly IRepository<Users> _usersRepository;
        private readonly IRepository<Response> _responseRepository;
        public UserService(
            IRepository<Users> rolesRepository,
            IRepository<Response> responseRepository
            )
        {
            _usersRepository = rolesRepository;
            _responseRepository = responseRepository; 
        }

        public async Task<List<UsersUpdateDto>> CallingListUsers()
        {
            var listUsers = await _usersRepository.ListAsync(new SearchUsersSpec());

            if (listUsers.Count == 0)
                return [];

            var users = listUsers.Select(m => new UsersUpdateDto(
                                        m.Id,
                                        m.FirtsName,
                                        m.MiddleName,
                                        m.FirtsLastName,
                                        m.SecondLastName,
                                        m.TypeIdentification,
                                        m.NumberIdentification,
                                        m.Status,
                                        m.UserName,
                                        m.UserType,
                                        m.RolesVirtual?.FirstOrDefault()?.Name ?? string.Empty
                                    ));


            return users.Where(x => !string.IsNullOrEmpty(x.RolName)).ToList();
        }

        public async Task<ResponseDto> CreateNewUser(UsersDto userControllerRequest)
        {
            ResponseDto response = new();
            try
            {
                if (string.IsNullOrEmpty(userControllerRequest.FirtsName) ||
                string.IsNullOrEmpty(userControllerRequest.FirtsLastName) ||
                string.IsNullOrEmpty(userControllerRequest.TypeIdentification) ||
                    string.IsNullOrEmpty(userControllerRequest.NumberIdentification)
                    )
                {
                    var success1 = await _responseRepository.FirstOrDefaultAsync(new SearchCodesSpec(Codes.requestInvalid));

                    response.Code = success1?.Code ?? string.Empty;
                    response.Message = success1?.Message;
                    return response;
                }

                var isExist = await _usersRepository.FirstOrDefaultAsync(new SearchByNumberIdentificationSpec(userControllerRequest.NumberIdentification));

                if (isExist != null)
                {
                    var success2 = await _responseRepository.FirstOrDefaultAsync(new SearchCodesSpec(Codes.existsUser));

                    response.Code = success2?.Code ?? string.Empty;
                    response.Message = success2?.Message;
                    return response;
                }
                var users = new Users(userControllerRequest.FirtsName!,
                userControllerRequest.MiddleName,
                userControllerRequest.FirtsLastName,
                                      userControllerRequest.SecondLastName ?? string.Empty,
                                      userControllerRequest.TypeIdentification,
                                      userControllerRequest.NumberIdentification,
                                      userControllerRequest.Status,
                                      userControllerRequest.UserName,
                                      userControllerRequest.UserType!
                                      );

                await _usersRepository.AddAsync(users);

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


        public async Task<ResponseDto> UpdateCurrentUser(UsersUpdateDto userControllerUpdate)
        {
            ResponseDto response = new();
            try
            {
                DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(ConstantsApi.utcNow, ConstantsApi.timeZoneInfo);

                var isExist = await _usersRepository.FirstOrDefaultAsync( new SearchUserSpec(userControllerUpdate.Id));

                if (isExist != null )
                {

                    isExist.Id = userControllerUpdate.Id!;
                    isExist.FirtsName = userControllerUpdate.FirtsName!;
                    isExist.MiddleName = userControllerUpdate.MiddleName!;
                    isExist.FirtsLastName = userControllerUpdate.FirtsLastName!;
                    isExist.SecondLastName = userControllerUpdate.SecondLastName!;
                    isExist.TypeIdentification = userControllerUpdate.TypeIdentification!;
                    isExist.NumberIdentification = userControllerUpdate.NumberIdentification!;
                    isExist.Status = userControllerUpdate.Status;
                    isExist.UserName = userControllerUpdate.UserName!;
                    isExist.UpdateDate = localTime;
                    isExist.UpdateUser = "Admin";

                    await _usersRepository.UpdateAsync(isExist);

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
