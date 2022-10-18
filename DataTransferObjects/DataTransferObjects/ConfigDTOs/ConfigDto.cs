//using AutoMapper;
//using DataTransferObjects.BasicDTOs;
//using DataTransferObjects.CustomExpressions;
//using DataTransferObjects.CustomMapping;
//using Entities.DatabaseModels.SystemModels;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading.Tasks;

//namespace DataTransferObjects.DataTransferObjects.ConfigDTOs
//{
//    //public class ConfigDto : BaseDto<ConfigDto, SysConfig, int>
//    //{
//    //    public bool ErrorLoggingEnabled { get; set; }
//    //    public string JwtSecret { get; set; }
//    //    public string ImagesPath { get; set; }
//    //    public string DocsPath { get; set; }

//    //}

//    //public class ConfigSelectDto : BaseDto<ConfigSelectDto, SysConfig, int>
//    //{
//    //    public bool ErrorLoggingEnabled { get; set; }
//    //    public string JwtSecret { get; set; }
//    //    public string ImagesPath { get; set; }
//    //    public string DocsPath { get; set; }
//    //}

//    public class ConfigCuDto : BaseDto<ConfigCuDto, SysConfig, int>
//    {
//        public bool ErrorLoggingEnabled { get; set; }
//        public string JwtSecret { get; set; }
//        public string ImagesPath { get; set; }
//        public string DocsPath { get; set; }
//        public string ArticlesPath { get; set; }
//        public bool IsActive { get; set; }
//    }

//    public class ConfigListDto : BaseListDto<ConfigListDto, SysConfig, int>
//    {
//        public bool ErrorLoggingEnabled { get; set; }
//        public string JwtSecret { get; set; }
//        public string ImagesPath { get; set; }
//        public string DocsPath { get; set; }

//        public override void CustomMappings(IMappingExpression<SysConfig, ConfigListDto> mappingExpression)
//        {
//            mappingExpression.AddActorMappings<SysConfig, ConfigListDto, int>();
//        }
//    }

//    public class ConfigSearchDto : BaseSearchDto, IHaveCustomExpression<SysConfig, ConfigSearchDto, int>
//    {

//        public Expression<Func<SysConfig, bool>> GenerateExpression(ConfigSearchDto dto)
//        {
//            List<Expression<Func<SysConfig, bool>>> expressions = ExpressionsHelper.GenerateActorsExpression<SysConfig, ConfigSearchDto, int>(dto);
//            expressions.Add(src => src.IsActive.Equals(IsActive));
//            return ExpressionsHelper.AndAll(expressions);

//        }
//    }
//}
