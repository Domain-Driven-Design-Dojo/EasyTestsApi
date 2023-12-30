using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Common.Utilities;
using DataTransferObjects.DTOs.BaseDtos;
using Entities.PersonModels;
using Newtonsoft.Json;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace DataTransferObjects.DTOs.V1.Persons
{
    public class CompanyDto : BaseDto<CompanyDto, CreCompany, long> //, IValidatableObject
    {
        public long PersonsId { get; set; }

        [DisplayName("عنوان")]
        public string Title { get; set; }

        [DisplayName("کد / شناسه ملی")]
        public string NationalId { get; set; }

        [DisplayName("تاریخ ثبت")]
        public DateTime? RegistrationDate { get; set; }

        [DisplayName("شماره ثبت")]
        public string RegistrationNo { get; set; }

        [DisplayName("کد اقتصادی")]
        public string EconomicNo { get; set; }
    }

    public class CompanyCuDto : BaseDto<CompanyCuDto, CreCompany, long>, IValidatableObject
    {
        public long PersonsId { get; set; }
        [AllowNull]
        public string Title { get; set; }
        [AllowNull]
        public string NationalId { get; set; }
        public DateTime? PersianRegistrationDate { get; set; }
        public string RegistrationNo { get; set; }
        public string EconomicNo { get; set; }
        [MaxLength(200)]
        public string TelNumbers { get; set; }

        public override void CustomMappings(IMappingExpression<CompanyCuDto, CreCompany> mappingExpression)
        {
            mappingExpression.ForMember(dest => dest.FPersonsId,
                conf => conf.MapFrom(src => src.PersonsId));
            mappingExpression.ForMember(dest => dest.RegistrationDate,
                conf => conf.MapFrom(src => src.PersianRegistrationDate.ToGregorianDate()));
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(NationalId) || NationalId.Length != 11 || !NationalId.IsDigitsOnly())//individual
                yield return new ValidationResult("شناسه ملی حقوقی یک عدد 11 رقمی و الزامیست", new[] { nameof(NationalId) });

            if (string.IsNullOrEmpty(Title))
                yield return new ValidationResult("وارد کردن نام شخص الزامی است", new[] { nameof(Title) });
        }
    }


    public class CompanyBriefCuDto : BaseDto<CompanyBriefCuDto, CreCompany, long>
    {
        public long PersonsId { get; set; }
        [AllowNull]
        public string Title { get; set; }
        [AllowNull]
        public string NationalId { get; set; }
        public DateTime? PersianRegistrationDate { get; set; }
        public string RegistrationNo { get; set; }
        public string EconomicNo { get; set; }
        [MaxLength(200)]
        public string TelNumbers { get; set; }
        public override void CustomMappings(IMappingExpression<CompanyBriefCuDto, CreCompany> mappingExpression)
        {
            mappingExpression.ForMember(dest => dest.FPersonsId,
                conf => conf.MapFrom(src => src.PersonsId));
            mappingExpression.ForMember(dest => dest.RegistrationDate,
                conf => conf.MapFrom(src => src.PersianRegistrationDate.ToGregorianDate()));
        }

    }


    public class CompanyListDto : BaseDto<CompanyListDto, CreCompany, long>
    {
        public long PersonsId { get; set; }

        [DisplayName("عنوان")]
        public string Title { get; set; }

        [DisplayName("کد / شناسه ملی")]
        public string NationalId { get; set; }

        [DisplayName("تاریخ ثبت")]
        public string RegistrationDate { get; set; }

        [DisplayName("شماره ثبت")]
        public string RegistrationNo { get; set; }

        [DisplayName("کد اقتصادی")]
        public string EconomicNo { get; set; }


        public override void CustomMappings(IMappingExpression<CreCompany, CompanyListDto> mappingExpression)
        {
            mappingExpression.ForMember(dest => dest.PersonsId,
                conf => conf.MapFrom(src => src.FPersonsId));
            mappingExpression.ForMember(dest => dest.RegistrationDate,
                conf => conf.MapFrom(src => src.RegistrationDate.ToPersianDate()));
        }
    }
}
