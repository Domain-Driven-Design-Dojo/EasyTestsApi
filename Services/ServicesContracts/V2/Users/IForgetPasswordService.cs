using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;
using DataTransferObjects.DTOs.Shared;
using DataTransferObjects.DTOs.V1.Persons;

namespace ServicesContracts.V2.Users
{
    public interface IForgetPasswordService:IScopedDependency
    {
        //Task<ApiResult<ForgetPasswordListDto>> GenerateCode(string nationalCode, CancellationToken cancellationToken);
        Task<ApiResult<PersonBriefListDto>>  ChangePassword(ForgetPasswordCuDto dto, CancellationToken cancellationToken);
    }
}
