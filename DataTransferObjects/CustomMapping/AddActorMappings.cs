using System;
using AutoMapper;
using Common.Utilities;
using DataTransferObjects.BasicDTOs;
using Entities.DatabaseModels.CommonModels.BaseModels;

namespace DataTransferObjects.CustomMapping
{
    public static class ActorMappings
    {
        public static void AddActorMappings<TEntity, TDto, TKey>(this IMappingExpression<TEntity, TDto> mappingExpression) 
            where TEntity : BaseEntityWithActors<TKey>, new()
            where TDto : BaseListDto<TDto, TEntity, TKey>, new()
        {
            mappingExpression
                .ForMember(dest => dest.CreationDate,
                    opt =>
                        opt.MapFrom(src => PersianDateExtensions.ToPersianDate((DateTime) src.CreationDate)));

            mappingExpression
              .ForMember(dest => dest.CreationTime,
                  opt =>
                      opt.MapFrom(src => PersianDateExtensions.ToPersianTime((DateTime)src.CreationDate)));

            mappingExpression
                .ForMember(dest => dest.ModificationDate,
                    opt =>
                        opt.MapFrom(src => PersianDateExtensions.ToPersianDate((DateTime) src.ModificationDate)));

            mappingExpression
              .ForMember(dest => dest.ModificationTime,
                  opt =>
                      opt.MapFrom(src => PersianDateExtensions.ToPersianTime((DateTime)src.ModificationDate)));

        }
    }
}
