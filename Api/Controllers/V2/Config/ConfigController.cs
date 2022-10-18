//using Api.Models.GlobalVariables;
//using AutoMapper;
//using Common;
//using Common.Exceptions;
//using DataTransferObjects.DataTransferObjects.ConfigDTOs;
//using DataTransferObjects.SharedModels;
//using Microsoft.AspNetCore.Mvc;
//using Services.IServices.V2;
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
//        private readonly IPeopleTypeService _PeopleTypeService;
//        private readonly IConfigService _SysConfigService;
//        public ConfigController(
//            IPeopleTypeService peopleTypeService,
//            IConfigService sysConfigService
//            )
//        {
//            _PeopleTypeService = peopleTypeService;
//            _SysConfigService = sysConfigService;
//        }
//        [HttpPost("UpdateBasicInformations")]
//        public async Task<ApiResult<ConfigListDto>> UpdateBasicInformations(CancellationToken cancellationToken)
//        {
//            var peopleTypes = await GlobalVariables.GetPeopleTypesAsync(_PeopleTypeService, cancellationToken, true);
//            var sysConfig = await _SysConfigService.updateActiveSysConfigAsync(cancellationToken);
            
//            return sysConfig;
//        }

//    }
//}
