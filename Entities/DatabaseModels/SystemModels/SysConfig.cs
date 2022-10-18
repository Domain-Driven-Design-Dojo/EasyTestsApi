//using System;
//using Entities.DatabaseModels.CommonModels.BaseModels;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//#nullable disable

//namespace Entities.DatabaseModels.SystemModels
//{
//    public class SysConfig : BaseEntityWithActors<int>
//    {
//        public bool ErrorLoggingEnabled { get; set; }
//        public string JwtSecret { get; set; }
//        public string ImagesPath { get; set; }
//        public string DocsPath { get; set; }
//    }

//    public class SysConfigConfiguration : IEntityTypeConfiguration<SysConfig>
//    {
//        public void Configure(EntityTypeBuilder<SysConfig> builder)
//        {
//            builder.Property(c => c.ErrorLoggingEnabled).IsRequired();
//            builder.Property(c => c.JwtSecret).IsRequired();
//            builder.Property(c => c.ImagesPath).HasMaxLength(100);
//            builder.Property(c => c.DocsPath).HasMaxLength(100);
//            builder.HasData(new SysConfig() { Id=1, CreationDate = new DateTime(2022, 10, 10), CreatorId = 1, DocsPath = "Test", ErrorLoggingEnabled = false, ImagesPath = "Test", IsActive = true, JwtSecret = "Test", ModificationDate=new DateTime(2022, 10, 10), ModifierId=1});
//        }
//    }
//}
