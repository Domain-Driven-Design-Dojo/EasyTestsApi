//using AutoMapper;
//using AutoMapper.QueryableExtensions;
//using Common;
//using Common.Exceptions;
//using Data.ApplicationUtilities;
//using Data.Contracts;
//using DataTransferObjects.DataTransferObjects.ConfigDTOs;
//using Entities.DatabaseModels.SystemModels;
//using Entities.DatabaseModels.UserModels;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Services.IServices.V2;
//using System;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Services.Services.V2
//{
//    public class ConfigService : IConfigService
//    {
//        private readonly IRepository<SysConfig> _sysConfigRepository;
//        private readonly IMapper _mapper;
//        private readonly UserManager<ApplicationUser> userManager;

//        public ConfigService(IRepository<SysConfig> sysConfigRepository,
//            IMapper mapper, UserManager<ApplicationUser> userManager)
//        {
//            this._sysConfigRepository = sysConfigRepository;
//            this._mapper = mapper;
//            this.userManager = userManager;
//        }

//        public ConfigListDto getActiveSysConfig()
//        {
//            var sysConfig = _sysConfigRepository.TableNoTracking.Where(x => x.IsActive == true).FirstOrDefault();
//            var SysConfigExpretion = new ConfigSearchDto()
//            {
//                IsActive = true
//            };
//            var expression = SysConfigExpretion.GenerateExpression(SysConfigExpretion);
//            var SysConfigListDto = _sysConfigRepository.TableNoTracking
//                .Where(expression).ProjectTo<ConfigListDto>(_mapper.ConfigurationProvider).FirstOrDefault();
//            StroreConfigDto(SysConfigListDto);
//            return SysConfigListDto;
//        }

//        public async Task<ConfigListDto> updateActiveSysConfigAsync(CancellationToken cancellationToken)
//        {
//            var supervisorUser = userManager.FindByNameAsync("superAdmin");

//            var resultConfig = await _sysConfigRepository.TableNoTracking
//                .ProjectTo<ConfigListDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(p => p.IsActive == true, cancellationToken);

//            if (resultConfig is null)
//            {
//                resultConfig = await addNewSysConfig(cancellationToken, supervisorUser.Result.Id);
//            }
//            else
//            {
//                await updateExistConfig(cancellationToken, supervisorUser.Result.Id);
//                resultConfig = await addNewSysConfig(cancellationToken, supervisorUser.Result.Id);
//            }

//            StroreConfigDto(resultConfig);
//            return resultConfig;

//        }

//        private async Task updateExistConfig(CancellationToken cancellationToken, long supervisorId)
//        {

//            var updateSysCuConfig = await _sysConfigRepository.TableNoTracking
//                  .ProjectTo<ConfigCuDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(p => p.IsActive == true, cancellationToken);

//            var updateSysConfig = updateSysCuConfig.ToEntity(_mapper);

//            updateSysConfig.AddCreator<SysConfig, int>(supervisorId);

//            updateSysConfig.IsActive = false;

//            await _sysConfigRepository.UpdateAsync(updateSysConfig, cancellationToken);

//        }

//        private async Task<ConfigListDto> addNewSysConfig(CancellationToken cancellationToken, long supervisorId)
//        {
//            var newConfig = new SysConfig()
//            {
//                IsActive = true,
//                ErrorLoggingEnabled = false,
//                ImagesPath = DataTransferObjects.GlobalDtos.Configs.ImagesPath,
//                DocsPath = DataTransferObjects.GlobalDtos.Configs.DocsPath,
//                JwtSecret = "Test"
//            };

//            //Get SuperAdmin User ID and Pass it as the creator
//            newConfig.AddCreator<SysConfig, int>(supervisorId);

//            await _sysConfigRepository.AddAsync(newConfig, cancellationToken);

//            if (newConfig.Id != 0)
//            {
//                var existingConfig = await _sysConfigRepository.TableNoTracking
//            .ProjectTo<ConfigListDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(p => p.IsActive == true, cancellationToken);
//                return existingConfig;// input;
//            }
//            else
//                throw new AppException(ApiResultStatusCode.InsertConfigFailed);

//        }

//        public void StroreConfigDto(ConfigListDto config)
//        {
//            DataTransferObjects.GlobalDtos.Configs.ActiveConfig = config;
//        }
//    }
//}
