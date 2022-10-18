using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;
using DataTransferObjects.DataTransferObjects.HrDTOs;
using DataTransferObjects.SharedModels;

namespace Services.IServices.V2.Hr
{
    public interface IForgetPasswordService:IScopedDependency
    {
        //Task<ApiResult<ForgetPasswordListDto>> GenerateCode(string nationalCode, CancellationToken cancellationToken);
        Task<ApiResult<PersonBriefListDto>>  ChangePassword(ForgetPasswordCuDto dto, CancellationToken cancellationToken);
    }
}
