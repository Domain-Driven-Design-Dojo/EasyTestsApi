using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using DataTransferObjects.CustomExpressions;
using DataTransferObjects.CustomMapping;
using DataTransferObjects.DTOs.BaseDtos;
using DataTransferObjects.DTOs.V1.Persons;
using Entities.PersonModels;
using Entities.UserModels;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace DataTransferObjects.DTOs.Shared.Users
{
    public class UserDto : BaseDto<UserDto, ApplicationUser, long>, IValidatableObject
    {
        [DisplayName("شخص")]
        public long FPersonsId { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("نام کاربری")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("ایمیل")]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("رمز عبور")]
        public string Password { get; set; }

        public DateTime CreationDate { get; set; }
        [AllowNull]
        public DateTime ModificationDate { get; set; }
        [AllowNull]
        public DateTime? LastLoginDate { get; set; }


        public PersonDto Person { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //if (Person is null)
            //   yield return new ValidationResult(ApiResultStatusCode.CompanyOrIndividualIsRequired.ToDisplay());
            if (UserName.Equals("test", StringComparison.OrdinalIgnoreCase))
                yield return new ValidationResult("نام کاربری نمیتواند Test باشد", new[] { nameof(UserName) });
            if (Password.Equals("123456"))
                yield return new ValidationResult("رمز عبور نمیتواند 123456 باشد", new[] { nameof(Password) });
            //if (Gender == GenderType.Male && Age > 30)
            //    yield return new ValidationResult("آقایان بیشتر از 30 سال معتبر نیستند", new[] { nameof(Gender), nameof(Age) });
        }
    }
    public class UserSelectDto : BaseDto<UserSelectDto, ApplicationUser, long>
    {
        [DisplayName("شخص")]
        public long FPersonsId { get; set; }
        [DisplayName("نام کاربری")]
        public string UserName { get; set; }

        [DisplayName("ایمیل")]
        public string Email { get; set; }


        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public DateTime? LastLoginDate { get; set; }

        public PersonDto Person { get; set; }


    }

    public class UserCuDto : BaseDto<UserCuDto, ApplicationUser, long>, IValidatableObject
    {
        public bool IsActive { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public long? PersonId { get; set; }


        public override void CustomMappings(IMappingExpression<UserCuDto, ApplicationUser> mappingExpression)
        {
            mappingExpression
                .ForMember(dest => dest.FPersonsId, conf => conf.MapFrom(src => src.PersonId));
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(UserName))
                yield return new ValidationResult("وارد کردن نام کاربری الزامی است", new[] { nameof(UserName) });

            if (string.IsNullOrEmpty(Email))
                yield return new ValidationResult("وارد کردن ایمیل الزامی است", new[] { nameof(UserName) });

            if (string.IsNullOrEmpty(Password))
                yield return new ValidationResult("وارد کردن پسورد است", new[] { nameof(Password) });
        }
    }

    public class UserListDto : BaseNoActorListDto<UserListDto, ApplicationUser, long>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public long? PersonId { get; set; }
        public string FullName { get; set; }
        public string NationalId { get; set; }

        public override void CustomMappings(IMappingExpression<ApplicationUser, UserListDto> mappingExpression)
        {
            mappingExpression
                .ForMember(dest => dest.PersonId, conf => conf.MapFrom(src => src.FPersonsId));
            mappingExpression
                .ForMember(dest => dest.FullName, opt =>
                    opt.MapFrom(src =>
                        src.Person.IndividualPerson == null
                            ? src.Person.Company.Title
                            : string.Concat(src.Person.IndividualPerson.FirstName, " ",
                                src.Person.IndividualPerson.LastName)));
        }
    }

    public class UserSearchDto : BaseSearchDto, IHaveCustomExpression<ApplicationUser, UserSearchDto>
    {
        public int Id { get; set; }
        //public bool IsActive { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public long? PersonId { get; set; }
        public string FullName { get; set; }
        public Expression<Func<ApplicationUser, bool>> GenerateExpression(UserSearchDto dto)
        {
            List<Expression<Func<ApplicationUser, bool>>> expressions = new List<Expression<Func<ApplicationUser, bool>>>();
            if (Id > 0)
            {
                expressions.Add(src => src.Id.Equals(Id));
            }

            if (!string.IsNullOrEmpty(FullName))
            {
                expressions.Add(src => src.Person.IndividualPerson != null ? string.Concat(src.Person.IndividualPerson.FirstName, " ", src.Person.IndividualPerson.LastName).Contains(FullName) : src.Person.Company.Title.Contains(FullName));
            }


            if (!string.IsNullOrEmpty(Email))
            {
                expressions.Add(src => src.Email.Contains(Email));
            }

            if (!string.IsNullOrEmpty(UserName))
            {
                expressions.Add(src => src.UserName.Contains(UserName));
            }

            if (PersonId > 0)
            {
                expressions.Add(src => src.Person.Id == PersonId);
            }

            return ExpressionsHelper.AndAll(expressions);
        }
    }
}
