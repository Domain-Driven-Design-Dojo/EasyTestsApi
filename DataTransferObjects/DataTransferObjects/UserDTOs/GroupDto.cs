using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using AutoMapper;
using DataTransferObjects.BasicDTOs;
using DataTransferObjects.CustomExpressions;
using Entities.DatabaseModels.UserModels;

namespace DataTransferObjects.DataTransferObjects.UserDTOs
{
    public class GroupDto : BaseDto<GroupDto, AccGroup, int>
    {
        [Required]
        [DisplayName("نام گروه")]
        public string Title { get; set; }
        [Required]
        [DisplayName("نام لاتین گروه")]
        public string LatinTitle { get; set; }
        [DisplayName("توضیحات")]
        public string Description { get; set; }
    }

    /// <inheritdoc />
    public class GroupSelectDto : BaseDto<GroupSelectDto, AccGroup, int>
    {
        public string Title { get; set; }
        public string CreatorName { get; set; }
        public string LastModifierName { get; set; }

    }

    public class GroupCuDto : BaseDto<GroupCuDto, AccGroup, int>, IValidatableObject
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string LatinTitle { get; set; }
        public string Description { get; set; }

        public IEnumerable<ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Title))
                yield return new ValidationResult("وارد کردن نام الزامی است", new[] { nameof(Title) });

            if (string.IsNullOrEmpty(LatinTitle))
                yield return new ValidationResult("وارد کردن نام لاتین است", new[] { nameof(LatinTitle) });

            if (LatinTitle != null && LatinTitle.Contains(" "))
                yield return new ValidationResult("در نام لاتین فاصله (Space) مجاز نیست", new[] { nameof(LatinTitle) });
        }
    }

    public class GroupListDto : BaseListDto<GroupListDto, AccGroup, int>
    {
        public string Title { get; set; }
        public string LatinTitle { get; set; }
        public string Description { get; set; }
    }

    public class GroupSearchDto : BaseSearchDto, IHaveCustomExpression<AccGroup, GroupSearchDto, int>
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string LatinTitle { get; set; }
        public string Description { get; set; }
        public Expression<Func<AccGroup, bool>> GenerateExpression(GroupSearchDto dto)
        {
            List<Expression<Func<AccGroup, bool>>> expressions = ExpressionsHelper.GenerateActorsExpression<AccGroup, GroupSearchDto, int>(dto);
            if (Id > 0)
            {
                expressions.Add(src => src.Id.Equals(Id));
            }

            if (!string.IsNullOrEmpty(Title))
            {
                expressions.Add(src => src.Title.Contains(Title));
            }

            if (!string.IsNullOrEmpty(LatinTitle))
            {
                expressions.Add(src => src.LatinTitle.Contains(LatinTitle));
            }

            if (!string.IsNullOrEmpty(Description))
            {
                expressions.Add(src => src.Description.Contains(Description));
            }

            return ExpressionsHelper.AndAll(expressions);
        }

        public class AddRoleToGroupDto : BaseDto<AddRoleToGroupDto, AccGroupRole, long>, IValidatableObject
        {
            public long RoleId { get; set; }
            public int GroupId { get; set; }

            public override void CustomMappings(IMappingExpression<AddRoleToGroupDto, AccGroupRole> mappingExpression)
            {
                mappingExpression
                    .ForMember(dest => dest.FGroupsId, conf => conf.MapFrom(src => src.GroupId));
                mappingExpression
                    .ForMember(dest => dest.FRolesId, conf => conf.MapFrom(src => src.RoleId));
            }

            public IEnumerable<ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
            {
                if (RoleId == 0)
                    yield return new ValidationResult("وارد کردن شناسه نقش الزامیست", new[] { nameof(RoleId) });

                if (GroupId == 0)
                    yield return new ValidationResult("وارد کردن شناسه گروه الزامیست", new[] { nameof(GroupId) });
            }
        }  
        public class AddUserToGroupDto : BaseDto<AddUserToGroupDto, AccGroupUser, long>, IValidatableObject
        {
            public long UserId { get; set; }
            public int GroupId { get; set; }

            public override void CustomMappings(IMappingExpression<AddUserToGroupDto, AccGroupUser> mappingExpression)
            {
                mappingExpression
                    .ForMember(dest => dest.FGroupsId, conf => conf.MapFrom(src => src.GroupId));
                mappingExpression
                    .ForMember(dest => dest.UserId, conf => conf.MapFrom(src => src.UserId));
            }

            public IEnumerable<ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
            {
                if (UserId == 0)
                    yield return new ValidationResult("وارد کردن شناسه کاربر الزامیست", new[] { nameof(UserId) });
                if (GroupId == 0)
                    yield return new ValidationResult("وارد کردن شناسه گروه الزامیست", new[] { nameof(GroupId) });
            }
        }
    }
    }

