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
    public class PersonsTypeDto : BaseDto<PersonsTypeDto, CrePersonType, int>
    {
        [Required]
        public string Title { get; set; }
    }

    /// <inheritdoc />
    public class PersonsTypeSelectDto : BaseDto<PersonsTypeSelectDto, CrePersonType, int>
    {
        public override int Id { get; set; }
        public string Title { get; set; }

        //public override void CustomMappings(IMappingExpression<Hr PersonsTypeType, PersonsTypeSelectDto> mappingExpression)
        //{
        //    mappingExpression.ForMember(
        //        dest => dest.CreatorName,
        //        config => config.MapFrom(src => $"{src.Title} ({src.Category.Name})"));
        //}
    }
    public class PersonsTypeListDto : BaseListDto<PersonsTypeListDto, CrePersonType, int>
    {
        public override int Id { get; set; }
        public string Title { get; set; }
        public override void CustomMappings(IMappingExpression<CrePersonType, PersonsTypeListDto> mappingExpression)
        {
            mappingExpression.AddActorMappings<CrePersonType, PersonsTypeListDto, int>();
        }
    }

    public class PersonsTypeCuDto : BaseDto<PersonsTypeCuDto, CrePersonType, int>
    {
        public override int Id { get; set; }
        public string Title { get; set; }
    }


    public class PersonsTypeSearchDto : BaseSearchDto, IHaveCustomExpression<CrePersonType, PersonsTypeSearchDto, int>
    {
        public string Title { get; set; }

        public Expression<Func<CrePersonType, bool>> GenerateExpression(PersonsTypeSearchDto dto)
        {
            List<Expression<Func<CrePersonType, bool>>> expressions = ExpressionsHelper.GenerateActorsExpression<CrePersonType, PersonsTypeSearchDto, int>(dto);

            if (!string.IsNullOrEmpty(dto.Title))
                expressions.Add(src => src.Title.Contains(dto.Title) || src.Title.Contains(dto.Title));

            return ExpressionsHelper.AndAll(expressions);
        }
    }
}
