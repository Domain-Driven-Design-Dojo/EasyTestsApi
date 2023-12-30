using AutoMapper;
using Common;
using Common.Utilities;
using DataTransferObjects.CustomExpressions;
using DataTransferObjects.CustomMapping;
using DataTransferObjects.DTOs.BaseDtos;
using Entities.PersonModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace DataTransferObjects.DTOs.V1.Persons
{
    public class PersonDto : BaseDto<PersonDto, CrePerson, long>, IValidatableObject
    {
        [DisplayName("نوع شخصیت")]
        [JsonProperty("fPersonsTypesId")]
        public int FPersonsTypesId { get; set; }


        [DisplayName("شخص حقوقی")]
        [AllowNull]
        public CompanyDto Company { get; set; }

        [AllowNull]
        [DisplayName("شخص حقیقی")]
        [JsonProperty("individualPerson")]
        public IndividualDto IndividualPerson { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (IndividualPerson is null && Company is null)
                yield return new ValidationResult(ApiResultStatusCode.CompanyOrIndividualIsRequired.ToDisplay());

        }
    }

    public class PersonSelectDto : BaseDto<PersonSelectDto, CrePerson, long>
    {
        [DisplayName("نوع شخصیت")]
        [JsonProperty("fPersonsTypesId")]
        public int FPersonsTypesId { get; set; }

        [AllowNull]
        [DisplayName("شخص حقوقی")]
        [JsonProperty("company")]
        public CompanyDto Company { get; set; }

        [AllowNull]
        [DisplayName("شخص حقیقی")]
        [JsonProperty("individualPerson")]
        public IndividualDto IndividualPerson { get; set; }
    }

    public class PersonCuDto : BaseDto<PersonCuDto, CrePerson, long>, IValidatableObject
    {
        public int TypeId { get; set; }
        public IndividualCuDto IndividualPerson { get; set; }
        public CompanyCuDto Company { get; set; }

        public override void CustomMappings(IMappingExpression<PersonCuDto, CrePerson> mappingExpression)
        {
            mappingExpression.ForMember(dest => dest.FPersonsTypesId, conf => conf.MapFrom(src => src.TypeId));
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (TypeId == (int)GlobalEnums.PersonsType.Individual && IndividualPerson is null)
                yield return new ValidationResult("اطلاعات شخص حقیقی ناقص است", new[] { nameof(IndividualPerson) });

            if (TypeId == (int)GlobalEnums.PersonsType.Company && Company is null)
                yield return new ValidationResult("اطلاعات شخص حقوقی ناقص است", new[] { nameof(Company) });
        }
    }

    public class PersonParcelETLCuDto : BaseDto<PersonParcelETLCuDto, CrePerson, long>, IValidatableObject
    {
        public int TypeId { get; set; }
        public IndividualBriefETLCuDto IndividualPerson { get; set; }
        public CompanyBriefCuDto Company { get; set; }

        public override void CustomMappings(IMappingExpression<PersonParcelETLCuDto, CrePerson> mappingExpression)
        {
            mappingExpression.ForMember(dest => dest.FPersonsTypesId, conf => conf.MapFrom(src => src.TypeId));
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (TypeId == (int)GlobalEnums.PersonsType.Individual && IndividualPerson is null)
                yield return new ValidationResult("اطلاعات شخص حقیقی ناقص است", new[] { nameof(IndividualPerson) });

            if (TypeId == (int)GlobalEnums.PersonsType.Company && Company is null)
                yield return new ValidationResult("اطلاعات شخص حقوقی ناقص است", new[] { nameof(Company) });
        }
    }

    public class PersonParcelCuDto : BaseDto<PersonParcelETLCuDto, CrePerson, long>, IValidatableObject
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalId { get; set; }
        public string MobileNumber { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(MobileNumber))
            {
                yield return new ValidationResult("وارد کردن حداقل یک شماره موبایل الزامیست");
            }
            else
            {
                if (!MobileNumber.IsDigitsOnly())
                    yield return new ValidationResult("شماره موبایل " + MobileNumber + " شامل کارارکترهای غیر عددی است", new[] { nameof(MobileNumber) });
            }
        }
    }

    public class PersonListDto : BaseListDto<PersonListDto, CrePerson, long>
    {
        public string FullName { get; set; }
        public int PersonTypeId { get; set; }
        public string Type { get; set; }
        public string DefaultTelNumber { get; set; }
        public string FullName_NationalId { get; set; }
        public string Username { get; set; }

        public override void CustomMappings(IMappingExpression<CrePerson, PersonListDto> mappingExpression)
        {
            mappingExpression.ForMember(dest => dest.Type, conf => conf.MapFrom(src => src.PersonType.Title));
            mappingExpression
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.IndividualPerson == null ? src.Company.Title : string.Concat(src.IndividualPerson.FirstName, " ", src.IndividualPerson.LastName)));


            //mappingExpression.ForMember(dest => dest.Username, conf => conf.MapFrom(src => src.Users == null ? null : src.Users.FirstOrDefault().UserName));

            mappingExpression.AddActorMappings<CrePerson, PersonListDto, long>();
        }
    }

    public class PersonFullInfoListDto : BaseListDto<PersonFullInfoListDto, CrePerson, long>
    {
        public string FullName { get; set; }
        public int PersonTypeId { get; set; }
        public string Type { get; set; }
        public string PersianBirthday { get; set; }
        public string DefaultTelNumber { get; set; }
        public string Username { get; set; }
        public CompanyListDto Company { get; set; }
        public IndividualDto IndividualPerson { get; set; }


        public override void CustomMappings(IMappingExpression<CrePerson, PersonFullInfoListDto> mappingExpression)
        {
            mappingExpression.ForMember(dest => dest.Type, conf => conf.MapFrom(src => src.PersonType.Title));
            mappingExpression
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.IndividualPerson == null ? src.Company.Title : string.Concat(src.IndividualPerson.FirstName, " ", src.IndividualPerson.LastName)));

            mappingExpression.ForMember(dest => dest.Company, conf => conf.MapFrom(src => src.Company));
            mappingExpression.ForMember(dest => dest.IndividualPerson, conf => conf.MapFrom(src => src.IndividualPerson));
            //mappingExpression.ForMember(dest => dest.Username, conf => conf.MapFrom(src => src.Users == null ? null : src.Users.FirstOrDefault().UserName));

            mappingExpression.AddActorMappings<CrePerson, PersonFullInfoListDto, long>();

        }


    }

    public class PersonBriefListDto : BaseDto<PersonBriefListDto, CrePerson, long>
    {
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
        public string NationalId { get; set; }
        public override void CustomMappings(IMappingExpression<CrePerson, PersonBriefListDto> mappingExpression)
        {
            mappingExpression.ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.IndividualPerson == null ? src.Company.Title : string.Concat(src.IndividualPerson.FirstName, " ", src.IndividualPerson.LastName)));

        }

    }

    public class PersonSearchDto : BaseSearchDto, IHaveCustomExpression<CrePerson, PersonSearchDto, long>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string TelNumber { get; set; }
        public string PersonTypeId { get; set; }
        public long? Id { get; set; }

        public Expression<Func<CrePerson, bool>> GenerateExpression(PersonSearchDto dto)
        {
            List<Expression<Func<CrePerson, bool>>> expressions = ExpressionsHelper.GenerateActorsExpression<CrePerson, PersonSearchDto, long>(dto);

            if (Id is not null)
                expressions.Add(src => src.Id == Id);


            if (!string.IsNullOrEmpty(Name))
            {
                expressions.Add(src => src.FPersonsTypesId == (int)GlobalEnums.PersonsType.Individual ?
                    src.IndividualPerson.FirstName.Contains(Name) :
                    src.Company.Title.Contains(Name)
                    );
            }
            if (!string.IsNullOrEmpty(LastName))
            {
                expressions.Add(src => src.IndividualPerson.LastName.Contains(LastName));
            }
            if (!string.IsNullOrEmpty(FullName))
            {
                expressions.Add(src => src.FPersonsTypesId == (int)GlobalEnums.PersonsType.Individual ?
                   //string.Concat((string?)src.IndividualPerson.FirstName,(string?) " ", (string?)src.IndividualPerson.LastName).Contains(FullName) :
                   (src.IndividualPerson.FirstName + " " + src.IndividualPerson.LastName).Contains(FullName) :
                    src.Company.Title.Contains(FullName)
                    );
            }
            if (!string.IsNullOrEmpty(PersonTypeId))
            {
                expressions.Add(src => src.FPersonsTypesId == int.Parse(PersonTypeId));
            }

            return ExpressionsHelper.AndAll(expressions);
        }
    }

    //public class PersonFullDto : BaseListDto<PersonFullDto, CrePerson, long>
    //{
    //    public int TypeId { get; set; }
    //    public IndividualCuDto IndividualPerson { get; set; }
    //    public CompanyCuDto Company { get; set; }
    //    public List<PersonsAddressCuDto> PersonsAddresses { get; set; }

    //    public override void CustomMappings(IMappingExpression<CrePerson, PersonFullDto> mappingExpression)
    //    {
    //        mappingExpression
    //            .ForMember(dest => dest.TypeId, conf => conf.MapFrom(src => src.FPersonsTypesId));
    //    }

    //    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    //    {
    //        if (TypeId == 1 && IndividualPerson is null)//individual
    //            yield return new ValidationResult("اطلاعات شخص حقیقی ناقص است", new[] { nameof(IndividualPerson) });

    //        if (TypeId == 2 && Company is null)//individual
    //            yield return new ValidationResult("اطلاعات شخص حقوقی ناقص است", new[] { nameof(Company) });
    //    }
    //}
}
