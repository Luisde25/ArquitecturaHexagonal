using AdminLibrary.Admin.Core;
using AdminLibrary.Admin.Core.MaterialModelAggregate.Specifications;
using AdminLibrary.Admin.Core.ResponseMessageModelAggregate.Specificaions;
using AdminLibrary.Admin.SharedKernel.Interfaces;
using AdminLibrary.Constants;
using AdminLibrary.Core.Dtos;

namespace AdminLibrary.Core.Interfaces
{
    public class MaterialService : IMaterialService
    {
        private readonly IRepository<MaterialsModel> _materialsModelRepository;
        private readonly IRepository<Response> _responseRepository;
        private readonly IRepository<Users> _userRepository;
        private readonly IRepository<History> _historyRepository;
        public MaterialService(
            IRepository<MaterialsModel> materialsModelRepository,
            IRepository<Response> responseRepository,
            IRepository<Users> userRepository,
            IRepository<History> historyRepository
            )
        {
            _materialsModelRepository = materialsModelRepository;
            _responseRepository = responseRepository;
            _userRepository = userRepository;
            _historyRepository = historyRepository;
        }

        public async Task<List<MaterialUpdateDto>> ListMaterials()
        {
            var listMaterials = await _materialsModelRepository.ListAsync();

            if (listMaterials.Count == 0)
            {
                return [];
            }

            return listMaterials.Select(m => new MaterialUpdateDto(
                                        m.Id,
                                        m.Identifier,
                                        m.Title,
                                        m.RegisterDate,
                                        m.RegisterQuantity,
                                        m.CurrentQuantity
                                    )).ToList();
        }

        public async Task<ResponseDto> RegisterNewMaterial(MaterialDto materialControllerRequest)
        {
            ResponseDto response = new();
            try
            {
                if (string.IsNullOrEmpty(materialControllerRequest.Identifier) ||
                    string.IsNullOrEmpty(materialControllerRequest.Title) ||
                    materialControllerRequest.RegisterQuantity <= 0
                    )
                {
                    var result = await _responseRepository.FirstOrDefaultAsync(new SearchCodesSpec(Codes.requestInvalid));

                    response.Code = result?.Code ?? string.Empty;
                    response.Message = result?.Message;

                    return response;
                }

                var isExist = await _materialsModelRepository.FirstOrDefaultAsync(new SearchMaterialSpec(materialControllerRequest.Identifier));

                if (isExist != null)
                {
                    var resp = await _responseRepository.FirstOrDefaultAsync(new SearchCodesSpec(Codes.ExistMaterial));

                    response.Code = resp?.Code ?? string.Empty;
                    response.Message = resp?.Message;

                    return response;

                }

                DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(ConstantsApi.utcNow, ConstantsApi.timeZoneInfo);
                var user = await _userRepository.FirstOrDefaultAsync(new SearchUsersSpec(materialControllerRequest.UserId));

                if (user == null)
                {
                    var result = await _responseRepository.FirstOrDefaultAsync(new SearchCodesSpec(Codes.UserNoFound));

                    response.Code = result?.Code ?? string.Empty;
                    response.Message = result?.Message;

                    return response;
                }


                var material = new MaterialsModel(
                    materialControllerRequest.Identifier,
                    materialControllerRequest.Title,
                    localTime,
                    materialControllerRequest.RegisterQuantity ?? 0,
                    materialControllerRequest.RegisterQuantity ?? 0
                    );

                await _materialsModelRepository.AddAsync(material);

                var historyMaterial = new History(material.Id,
                                                        materialControllerRequest.UserId,
                                                        ConstantsApi.Message,
                                                         ConstantsApi.Register,
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

        public async Task<ResponseDto> UpdateCurrentNewMaterial(MaterialUpdateDto materialControllerRequest)
        {
            ResponseDto response = new();
            try
            {
                DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(ConstantsApi.utcNow, ConstantsApi.timeZoneInfo);

                var isExist = await _materialsModelRepository.FirstOrDefaultAsync(new SearchMaterialByIdSpec(materialControllerRequest.Id));

                if (isExist != null)
                {
                    isExist.Title = materialControllerRequest.Title ?? string.Empty;
                    isExist.RegisterQuantity = materialControllerRequest.RegisterQuantity ?? 0;
                    isExist.CurrentQuantity = materialControllerRequest.CurrentQuantity ?? 0;
                    isExist.UpdateDate = localTime;
                    isExist.UpdateUser = "Admin";

                   await  _materialsModelRepository.UpdateAsync(isExist);
                    
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
