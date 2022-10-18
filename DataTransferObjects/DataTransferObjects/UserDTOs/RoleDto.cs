using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Common;
using Common.Utilities;
using DataTransferObjects.BasicDTOs;
using DataTransferObjects.CustomExpressions;
using DataTransferObjects.CustomMapping;
using Entities.DatabaseModels.UserModels;

namespace DataTransferObjects.DataTransferObjects.UserDTOs
{
    public class RoleDto : BaseDto<RoleDto, AccRole, long>, IValidatableObject
    {


        [Required]
        [StringLength(100)]
        [DisplayName("نام نقش")]
        public virtual string Name { get; set; }
        public virtual string NormalizedName { get; set; }
        [Required]
        [StringLength(100)]
        [DisplayName("توضیحات")]
        public string Description { get; set; }
        [DisplayName("نام فارسی")]
        public string PersianName { get; set; }

        public IEnumerable<ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            if (String.IsNullOrEmpty(Name))
                yield return new ValidationResult(ApiResultStatusCode.NameIsRequired.ToDisplay());
        }
    }
    public class RoleSelectDto : BaseDto<RoleSelectDto, AccRole, long>
    {
        [DisplayName("نام نقش")]
        public virtual string Name { get; set; }
        public virtual string NormalizedName { get; set; }
        [DisplayName("ایمیل")]
        public string Email { get; set; }
        [Required]
        [StringLength(100)]
        [DisplayName("توضیحات")]
        public string Description { get; set; }
        [DisplayName("نام فارسی")]
        public string PersianName { get; set; }
    }

    public class RoleCuDto : BaseDto<RoleCuDto, AccRole, long>, IValidatableObject
    {
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string Description { get; set; }
        public string PersianName { get; set; }
        public IEnumerable<ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(PersianName) && PersianName is { Length: < 4 })
                yield return new ValidationResult(ApiResultStatusCode.RoleNameLenght.ToDisplay());
            if (string.IsNullOrEmpty(Name) && Name is { Length: < 4 })
                yield return new ValidationResult(ApiResultStatusCode.RoleNameLenght.ToDisplay());
            if (string.IsNullOrEmpty(Description) && Description is { Length: < 5 })
                yield return new ValidationResult(ApiResultStatusCode.DescriptionRoleLenght.ToDisplay());
            //persian Charecters 
        }
    }

    public class RoleListDto : BaseNoActorListDto<RoleListDto, AccRole, long>
    {
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string Description { get; set; }
        public string PersianName { get; set; }
    }

    public class RoleSearchDto : BaseSearchDto, IHaveCustomExpression<AccRole, RoleSearchDto>
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public string NormalizedName { get; set; }
        public string Description { get; set; }
        public string PersianName { get; set; }
        public long[] DesiredRoleIds { get; set; }
        public Expression<Func<AccRole, bool>> GenerateExpression(RoleSearchDto dto)
        {
            List<Expression<Func<AccRole, bool>>> expressions = new List<Expression<Func<AccRole, bool>>>();
            if (Id > 0)
            {
                expressions.Add(src => src.Id.Equals(Id));
            }

            if (!string.IsNullOrEmpty(dto.Name))
                expressions.Add(src => src.Name.Contains(dto.Name) || src.PersianName.Contains(dto.Name));

            if (!string.IsNullOrEmpty(NormalizedName))
            {
                expressions.Add(src => src.Name.Contains(Name));
            }

            if (!string.IsNullOrEmpty(Description))
            {
                expressions.Add(src => src.Name.Contains(Description));
            }

            if (!string.IsNullOrEmpty(PersianName))
            {
                expressions.Add(src => src.PersianName.Contains(PersianName));
            }
            if (DesiredRoleIds != null)
                if (DesiredRoleIds.Length > 0)
                {
                    expressions.Add(src => DesiredRoleIds.Contains(src.Id));
                }

            return ExpressionsHelper.AndAll(expressions);
        }
    }
}
