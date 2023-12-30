using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataTransferObjects.CustomExpressions;
using DataTransferObjects.CustomMapping;
using DataTransferObjects.DTOs.BaseDtos;
using Entities.PersonModels;

namespace DataTransferObjects.DTOs.V1.Persons
{

    public class ForgetPasswordListDto : BaseDto<ForgetPasswordListDto, HrForgetPassword, long>
    {
        public int Code { get; set; }

    }

    public class ForgetPasswordCuDto : BaseDto<ForgetPasswordCuDto, HrForgetPassword, long>
    {
        [Required]
        public int Code { get; set; }
        [Required]
        public string NationalCode { get; set; }
        [Required]
        [StringLength(100)]
        [MinLength(8)]
        public string NewPassword { get; set; }

        [StringLength(100)]
        [MinLength(8)]
        public string CurrentPassword { get; set; }
    }

}
