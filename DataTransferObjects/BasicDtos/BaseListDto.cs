using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Entities.DatabaseModels.CommonModels.BaseModels;

namespace DataTransferObjects.BasicDTOs
{
    //public abstract class BaseListDto
    //{
    //}

    public abstract class BaseListDto<TDto, TEntity, TKey> : BaseDto<TDto, TEntity, TKey>
        where TDto : BaseListDto<TDto, TEntity, TKey>, new()
        where TEntity : BaseEntityWithActors<TKey>, new()
    {
        [Display(Name = "فعال")]
        public bool IsActive { get; set; }

        [Display(Name = "شناسه ایجاد کننده")]
        public long CreatorId { get; set; }

        [Display(Name = "شناسه تغییر دهنده")]
        public long ModifierId { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        public string CreationDate { get; set; }

        [Display(Name = "تاریخ آخرین تغییر")]
        public string ModificationDate { get; set; }
        [Display(Name = "زمان ایجاد")]
        public string CreationTime { get; set; }

        [Display(Name = "زمان آخرین تغییر")]
        public string ModificationTime { get; set; }

        [Display(Name = "ایجادکننده")]
        public string Creator { get; set; }

        [Display(Name = "تغییردهنده")]
        public string Modifier { get; set; }

        //[Display(Name = "تعداد")]
        //public string Count { get; set; }

        //[Display(Name = "تغییردهنده")]
        //public string Vahid { get; set; }

        public override void CreateMappings(Profile profile)
        {
            var mappingExpression = profile.CreateMap<TDto, TEntity>();//.ReverseMap();

            var dtoType = typeof(TDto);
            var entityType = typeof(TEntity);
            //Ignore any property of source (like Post.Author) that dose not contains in destination 
            foreach (var property in entityType.GetProperties())
            {
                if (dtoType.GetProperty(property.Name) == null)
                    mappingExpression.ForMember(property.Name, opt => opt.Ignore());
            }

            CustomMappings(mappingExpression);
            CustomMappings(mappingExpression.ReverseMap());
        }
    }

    public abstract class BaseNoActorListDto<TDto, TEntity, TKey> : BaseDto<TDto, TEntity, TKey> 
        where TEntity : class, IEntity<TKey>, new() 
        where TDto : class, new()
    {
        [Display(Name = "فعال")]
        public bool IsActive { get; set; }

        public override void CreateMappings(Profile profile)
        {
            var mappingExpression = profile.CreateMap<TDto, TEntity>();//.ReverseMap();

            var dtoType = typeof(TDto);
            var entityType = typeof(TEntity);
            //Ignore any property of source (like Post.Author) that dose not contains in destination 
            foreach (var property in entityType.GetProperties())
            {
                if (dtoType.GetProperty(property.Name) == null)
                    mappingExpression.ForMember(property.Name, opt => opt.Ignore());
            }

            CustomMappings(mappingExpression);
            CustomMappings(mappingExpression.ReverseMap());
        }
    }
}
