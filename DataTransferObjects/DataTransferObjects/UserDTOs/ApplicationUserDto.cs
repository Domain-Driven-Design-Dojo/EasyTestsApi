using AutoMapper;
using Common.Utilities;
using DataTransferObjects.BasicDTOs;
using DataTransferObjects.CustomExpressions;
using Entities.DatabaseModels.UserModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace DataTransferObjects.DataTransferObjects.UserDTOs
{
    public class ApplicationUserDto : BaseDto<ApplicationUserDto, ApplicationUser, long>, IValidatableObject
    {
        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        //[Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(500)]
        public string Password { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public long? FPeopleId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (UserName.Equals("test", StringComparison.OrdinalIgnoreCase))
                yield return new ValidationResult("نام کاربری نمیتواند Test باشد", new[] { nameof(UserName) });
            if (Password.Equals("123456"))
                yield return new ValidationResult("رمز عبور نمیتواند 123456 باشد", new[] { nameof(Password) });
            //if (Gender == GenderType.Male && Age > 30)
            //    yield return new ValidationResult("آقایان بیشتر از 30 سال معتبر نیستند", new[] { nameof(Gender), nameof(Age) });
        }

        public override void CustomMappings(IMappingExpression<ApplicationUser, ApplicationUserDto> mappingExpression)
        {
            mappingExpression.ForMember(
                dest => dest.Password,
                config => config.MapFrom(src => $"{src.PasswordHash}"));

        }
    }

    public class ApplicationUserCuDto : BaseDto<ApplicationUserCuDto, ApplicationUser, long>
    {
        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        //[Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(500)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public long? PersonId { get; set; }


        public override void CustomMappings(IMappingExpression<ApplicationUserCuDto, ApplicationUser> mappingExpression)
        {
            mappingExpression
                .ForMember(dest => dest.FPeopleId, conf => conf.MapFrom(src => src.PersonId));
        }
    }

    public class ApplicationUserListDto : BaseDto<ApplicationUserListDto, ApplicationUser, long>
    {
        //public PersonListDto Person { get; set; }
        public string UserName { get; set; }

        public string Email { get; set; }

        public string PersianLastLoginDate { get; set; }
        public string PersianCreationDate { get; set; }
        public string PersianModificationDate { get; set; }
        public bool IsActive { get; set; }

        public long? PersonId { get; set; }
        public string FullName { get; set; }

        public override void CustomMappings(IMappingExpression<ApplicationUser, ApplicationUserListDto> mappingExpression)
        {
            mappingExpression
                .ForMember(dest => dest.PersianLastLoginDate, conf => conf.MapFrom(src => src.LastLoginDate.ToPersianDate()));
            mappingExpression
                .ForMember(dest => dest.PersianCreationDate, conf => conf.MapFrom(src => src.CreationDate.ToPersianDate()));
            mappingExpression
                .ForMember(dest => dest.PersianModificationDate, conf => conf.MapFrom(src => src.ModificationDate.ToPersianDate()));
            mappingExpression
                .ForMember(dest => dest.PersonId, conf => conf.MapFrom(src => src.FPeopleId));
            mappingExpression
                .ForMember(dest => dest.FullName, opt =>
                    opt.MapFrom(src =>
                        src.Person.IndividualPerson == null
                            ? src.Person.Company.Title
                            : src.Person.IndividualPerson.FirstName + " " + src.Person.IndividualPerson.LastName));

        }
    }

    public class ApplicationUserSearchDto : BaseSearchDto, IHaveCustomExpression<ApplicationUser, ApplicationUserSearchDto>
    {
        public int RoleId { get; set; }
        public int GroupId { get; set; }
        public long Id { get; set; }
        public string NationalId { get; set; }
        public string UserName { get; set; }
        public long? PersonId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public Expression<Func<ApplicationUser, bool>> GenerateExpression(ApplicationUserSearchDto dto)
        {
            List<Expression<Func<ApplicationUser, bool>>>
                expressions = new List<Expression<Func<ApplicationUser, bool>>>();

            if (Id > 0)
            {
                expressions.Add(src => src.Id.Equals(Id));
            }

            if (!string.IsNullOrEmpty(FirstName))
            {
                expressions.Add(src => src.Person.IndividualPerson != null ? src.Person.IndividualPerson.FirstName.Contains(FirstName) : src.Person.Company.Title.Contains(FirstName));
            }

            if (!string.IsNullOrEmpty(LastName))
            {
                expressions.Add(src => src.Person.IndividualPerson != null ? src.Person.IndividualPerson.LastName.Contains(LastName) : src.Person.Company.Title.Contains(LastName));
            }

            if (!string.IsNullOrEmpty(UserName))
                expressions.Add(src => src.UserName.ToLower().Equals(UserName.ToLower()));


            if (!string.IsNullOrEmpty(Email))
            {
                expressions.Add(src => src.Email.ToLower().Contains(Email.ToLower()));
            }

            if (!string.IsNullOrEmpty(FullName))
            {
                expressions.Add(src => src.Person.IndividualPerson != null ?
                (src.Person.IndividualPerson.FirstName + " " + src.Person.IndividualPerson.LastName).Contains(FullName)
                : src.Person.Company.Title.Contains(FullName));
            }

            return ExpressionsHelper.AndAll(expressions);
        }

        public class ApplicationUserChangePasswordDto
        {
            [Required]
            public string UserName { get; set; }

            //[Required]
            //public long UserId { get; set; }

            [Required]
            [StringLength(100)]
            [MinLength(8)]
            public string NewPassword { get; set; }

            [AllowNull]
            [StringLength(100)]
            [MinLength(8)]
            public string CurrentPassword { get; set; }
        }

        public class AddRoleToUserDto
        {
            [Required]
            public long UserId { get; set; }
            [Required]
            public long RoleId { get; set; }
        }
    }

    public class ApplicationUserDetailedListDto : BaseDto<ApplicationUserDetailedListDto, ApplicationUser, long>
    {
        //public PersonListDto Person { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PersianLastLoginDate { get; set; }
        public string PersianCreationDate { get; set; }
        public string PersianModificationDate { get; set; }
        public bool IsActive { get; set; }
        public long? PersonId { get; set; }
        public string FullName { get; set; }
        public string NationalId { get; set; }


        public override void CustomMappings(IMappingExpression<ApplicationUser, ApplicationUserDetailedListDto> mappingExpression)
        {
            mappingExpression.ForMember(dest => dest.PersianLastLoginDate, conf => conf.MapFrom(src => src.LastLoginDate.ToPersianDate()));
            mappingExpression.ForMember(dest => dest.PersianCreationDate, conf => conf.MapFrom(src => src.CreationDate.ToPersianDate()));
            mappingExpression.ForMember(dest => dest.PersianModificationDate, conf => conf.MapFrom(src => src.ModificationDate.ToPersianDate()));
            mappingExpression.ForMember(dest => dest.PersonId, conf => conf.MapFrom(src => src.FPeopleId));
            mappingExpression.ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Person.IndividualPerson == null
                             ? src.Person.Company.Title
                             : src.Person.IndividualPerson.FirstName + " " + src.Person.IndividualPerson.LastName));



        }
    }


}
