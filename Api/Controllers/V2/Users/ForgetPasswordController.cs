using System.Threading;
using System.Threading.Tasks;
using DataTransferObjects.DTOs.Shared;
using DataTransferObjects.DTOs.V1.Persons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.V2.Users;
using WebFramework.Api;

namespace Api.Controllers.V2.Users
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
