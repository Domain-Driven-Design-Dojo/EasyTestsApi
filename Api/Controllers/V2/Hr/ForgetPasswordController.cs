using System.Threading;
using System.Threading.Tasks;
using DataTransferObjects.DataTransferObjects.HrDTOs;
using DataTransferObjects.SharedModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.IServices.V2.Hr;
using WebFramework.Api;

namespace Api.Controllers.V2.Hr
{
    [ApiVersion("2")]
    public class ForgetPasswordController : BaseController
    {
        private readonly IForgetPasswordService _ForgetPasswordService;

        public ForgetPasswordController(IForgetPasswordService forgetPasswordService)
        {
            _ForgetPasswordService = forgetPasswordService;
        }

        //[AllowAnonymous]
        //[HttpPost("GenerateCode")]
        //public async Task<ApiResult<ForgetPasswordListDto>> GenerateCode(string nationalCode, CancellationToken cancellationToken)
        //{
        //    return await _ForgetPasswordService.GenerateCode(nationalCode, cancellationToken);
        //}


        [AllowAnonymous]
        [HttpPost("ChangePassword")]
        public async Task<ApiResult<PersonBriefListDto>> ChangePassword(ForgetPasswordCuDto dto, CancellationToken cancellationToken)
        {
            return await _ForgetPasswordService.ChangePassword(dto, cancellationToken);
        }
    }
}
