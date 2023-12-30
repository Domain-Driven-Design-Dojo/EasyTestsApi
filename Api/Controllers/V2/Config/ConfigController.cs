//using Api.Models.GlobalVariables;
//using AutoMapper;
//using Common;
//using Common.Exceptions;
//using DataTransferObjects.DataTransferObjects.ConfigDTOs;
//using DataTransferObjects.DTOs.Shared;
//using Microsoft.AspNetCore.Mvc;
//using ServicesContracts.V2;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using WebFramework.Api;

//namespace Api.Controllers.V2.Config
//{
//    [ApiVersion("2")]
//    public class ConfigController : BaseController
//    {
//        private readonly IPersonsTypeService _PersonsTypeService;
//        private readonly IConfigService _SysConfigService;
//        public ConfigController(
//            IPersonsTypeService PersonsTypeService,
//            IConfigService sysConfigService
//            )
//        {
//            _PersonsTypeService = PersonsTypeService;
//            _SysConfigService = sysConfigService;
//        }
//        [HttpPost("UpdateBasicInformations")]
//        public async Task<ApiResult<ConfigListDto>> UpdateBasicInformations(CancellationToken cancellationToken)
//        {
//            var PersonsTypes = await GlobalVariables.GetPersonsTypesAsync(_PersonsTypeService, cancellationToken, true);
//            var sysConfig = await _SysConfigService.updateActiveSysConfigAsync(cancellationToken);
            
//            return sysConfig;
//        }

//    }
//}
