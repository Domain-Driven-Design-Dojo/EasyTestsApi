using Common.Utilities;
using Entities.BaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using static Common.Utilities.GlobalEnums;

namespace Entities.PersonModels
{
    public class CrePersonType : BaseEntityWithActorsNoIdentity
    {
        public string Title { get; set; }
        public string LatinTitle { get; set; }
        public ICollection<CrePerson> Persons { get; set; }
    }
    public class CrePersonsTypeConfiguration : IEntityTypeConfiguration<CrePersonType>
    {
        public void Configure(EntityTypeBuilder<CrePersonType> builder)
        {
            builder.Property(p => p.Title).IsRequired().HasMaxLength(200);

            foreach (var item in (PersonsType[])Enum.GetValues(typeof(PersonsType)))
            {
                var LatinTitle = Enum.GetName(typeof(PersonsType), item);
                var title = item.ToDisplay();

                builder.HasData(new CrePersonType
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
