using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataTransferObjects.BasicDTOs;
using DataTransferObjects.CustomExpressions;
using DataTransferObjects.CustomMapping;
using Entities.DatabaseModels.HrModels;

namespace DataTransferObjects.DataTransferObjects.HrDTOs
{
    public class PeopleTypeDto : BaseDto<PeopleTypeDto, CrePeopleType, int>
    {
        [Required]
        public string Title { get; set; }
    }

    /// <inheritdoc />
    public class PeopleTypeSelectDto : BaseDto<PeopleTypeSelectDto, CrePeopleType, int>
    {
        public override int Id { get; set; }
        public string Title { get; set; }

        //public override void CustomMappings(IMappingExpression<Hr PeopleTypeType, PeopleTypeSelectDto> mappingExpression)
        //{
        //    mappingExpression.ForMember(
        //        dest => dest.CreatorName,
        //        config => config.MapFrom(src => $"{src.Title} ({src.Category.Name})"));
        //}
    }
    public class PeopleTypeListDto : BaseListDto<PeopleTypeListDto, CrePeopleType, int>
    {
        public override int Id { get; set; }
        public string Title { get; set; }
        public override void CustomMappings(IMappingExpression<CrePeopleType, PeopleTypeListDto> mappingExpression)
        {
            mappingExpression.AddActorMappings<CrePeopleType, PeopleTypeListDto, int>();
        }
    }

    public class PeopleTypeCuDto : BaseDto<PeopleTypeCuDto, CrePeopleType, int>
    {
        public override int Id { get; set; }
        public string Title { get; set; }
    }


    public class PeopleTypeSearchDto : BaseSearchDto, IHaveCustomExpression<CrePeopleType, PeopleTypeSearchDto, int>
    {
        public string Title { get; set; }

        public Expression<Func<CrePeopleType, bool>> GenerateExpression(PeopleTypeSearchDto dto)
        {
            List<Expression<Func<CrePeopleType, bool>>> expressions = ExpressionsHelper.GenerateActorsExpression<CrePeopleType, PeopleTypeSearchDto, int>(dto);

            if (!String.IsNullOrEmpty(dto.Title))
                expressions.Add(src => (src.Title.Contains(dto.Title) || src.Title.Contains(dto.Title)));

            return ExpressionsHelper.AndAll(expressions);
        }
    }
}
