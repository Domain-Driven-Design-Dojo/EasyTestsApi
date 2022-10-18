using System;
using System.Collections.Generic;
using Common.Utilities;
using Entities.DatabaseModels.CommonModels.BaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Common.Utilities.GlobalEnums;

namespace Entities.DatabaseModels.HrModels
{
    public class CrePeopleType : BaseEntityWithActorsNoIdentity
    {
        public string Title { get; set; }
        public string LatinTitle { get; set; }
        public ICollection<CrePerson> People { get; set; }
    }
    public class CrePeopleTypeConfiguration : IEntityTypeConfiguration<CrePeopleType>
    {
        public void Configure(EntityTypeBuilder<CrePeopleType> builder)
        {
            builder.Property(p => p.Title).IsRequired().HasMaxLength(200);

            foreach (var item in (PeopleType[])Enum.GetValues(typeof(PeopleType)))
            {
                var LatinTitle = Enum.GetName(typeof(PeopleType), item);
                var title = item.ToDisplay();

                builder.HasData(new CrePeopleType
                {
                    Title = title,
                    LatinTitle = LatinTitle,
                    Id = (int)item,
                    IsActive = true,
                    CreatorId = 1,
                    ModifierId = 1,
                    CreationDate = new DateTime(2021, 1, 1),
                    ModificationDate = new DateTime(2021, 1, 1)
                });
            }
        }
    }
}
