using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Common.Utilities;
using DataTransferObjects.BasicDTOs;
using DataTransferObjects.CustomExpressions;
using Entities.DatabaseModels.HrModels;
using Newtonsoft.Json;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace DataTransferObjects.DataTransferObjects.HrDTOs
{
    public class IndividualDto : BaseDto<IndividualDto, CreIndividual, long>//, IValidatableObject
    {
        [DisplayName("نام")]
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [DisplayName("نام خانوادگی")]
        [JsonProperty("lastName")]
        public string LastName { get; set; }

        public long PersonId { get; set; }

    }

    public class IndividualCuDto : BaseDto<IndividualCuDto, CreIndividual, long>
    {
        public long PersonId { get; set; }
        public string FirstName { get; set; }
        [AllowNull]
        public string LastName { get; set; }



        public override void CustomMappings(IMappingExpression<IndividualCuDto, CreIndividual> mappingExpression)
        {
            mappingExpression.ForMember(dest => dest.FPeopleId,
                conf => conf.MapFrom(src => src.PersonId));
        }
    }

    public class IndividualBriefETLCuDto : BaseDto<IndividualBriefETLCuDto, CreIndividual, long>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalId { get; set; }
        public string MobileNumber { get; set; }
    }


    public class IndividualListDto : BaseDto<IndividualListDto, CreIndividual, long>
    {
        public long PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }



        public override void CustomMappings(IMappingExpression<CreIndividual, IndividualListDto> mappingExpression)
        {
            mappingExpression.ForMember(dest => dest.PersonId,
                conf => conf.MapFrom(src => src.FPeopleId));
        }
    }

    //public class IndividualFullDto : BaseDto<IndividualFullDto, CreIndividual, long>
    //{
    //    public long PersonId { get; set; }
    //    public string FirstName { get; set; }
    //    [AllowNull]
    //    public string LastName { get; set; }
    //    [AllowNull]
    //    public string FatherName { get; set; }
    //    [AllowNull]
    //    public string NationalId { get; set; }
    //    //public DateTime? Birthday { get; set; }
    //    public string PersianBirthday { get; set; }
    //    public string IdNo { get; set; }
    //    public int? GenderTypeId { get; set; }
    //    public int? HomeTownCityId { get; set; }
    //    public int? CountryId { get; set; }
    //    public int? MaritalStatusId { get; set; }
    //    public int? MilitaryStatusId { get; set; }
    //    public int? ReligionTypesId { get; set; }

    //    public List<PersonMobileNumberCuDto> MobileNumbers { get; set; }
    //    public List<IndividualEducationDegreeCuDto> EducationDegrees { get; set; }

    //    public override void CustomMappings(IMappingExpression<CreIndividual,IndividualFullDto> mappingExpression)
    //    {
    //        mappingExpression.ForMember(dest => dest.CountryId,
    //            conf => conf.MapFrom(src => src.FCountriesId));
    //        mappingExpression.ForMember(dest => dest.GenderTypeId,
    //            conf => conf.MapFrom(src => src.FGenderTypesId));
    //        mappingExpression.ForMember(dest => dest.HomeTownCityId,
    //            conf => conf.MapFrom(src => src.FHomeTownCitiesId));
    //        mappingExpression.ForMember(dest => dest.MaritalStatusId,
    //            conf => conf.MapFrom(src => src.FMaritalStatusId));
    //        mappingExpression.ForMember(dest => dest.MilitaryStatusId,
    //            conf => conf.MapFrom(src => src.FMilitaryStatusId));
    //        mappingExpression.ForMember(dest => dest.PersonId,
    //            conf => conf.MapFrom(src => src.FPeopleId));
    //        mappingExpression.ForMember(dest => dest.ReligionTypesId,
    //            conf => conf.MapFrom(src => src.FReligionTypesId));
    //        mappingExpression.ForMember(dest => dest.PersianBirthday,
    //            conf => conf.MapFrom(src => PersianDateExtensions.ToPersianDate((DateTime?)src.Birthday)));
    //        //mappingExpression.ForMember(dest => dest.Country,
    //        //    conf => conf.MapFrom(src => src.Nationality.Title));
    //        //mappingExpression.ForMember(dest => dest.GenderType,
    //        //    conf => conf.MapFrom(src => src.GenderType.Title));
    //        //mappingExpression.ForMember(dest => dest.HomeTownCity,
    //        //    conf => conf.MapFrom(src => src.HomeTownCity.Title));
    //        //mappingExpression.ForMember(dest => dest.MaritalStatus,
    //        //    conf => conf.MapFrom(src => src.MaritalStatus.Title));
    //        //mappingExpression.ForMember(dest => dest.MilitaryStatus,
    //        //    conf => conf.MapFrom(src => src.MilitaryStatus.Title));
    //        //mappingExpression.ForMember(dest => dest.ReligionType,
    //        //    conf => conf.MapFrom(src => src.Religion.Title));
    //    }

    //    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    //    {
    //        if (string.IsNullOrEmpty(NationalId) || NationalId.Length != 10 || !NationalId.IsDigitsOnly())//individual
    //            yield return new ValidationResult("کد ملی یک عدد 10 رقمی و الزامیست", new[] { nameof(NationalId) });

    //        if (MobileNumbers is { Count: > 0 })
    //            foreach (var mobile in MobileNumbers)
    //            {
    //                if (!mobile.MobileNumber.IsDigitsOnly())
    //                    yield return new ValidationResult("شماره موبایل " + mobile.MobileNumber + " شامل کارارکترهای غیر عددی است", new[] { nameof(MobileNumbers) });
    //            }
    //        else
    //        {
    //            yield return new ValidationResult("وارد کردن حداقل یک شماره موبایل الزامیست", new[] { nameof(NationalId) });
    //        }
    //    }
    //}
}
