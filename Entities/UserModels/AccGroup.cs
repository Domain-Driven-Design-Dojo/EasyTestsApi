using System;
using System.Collections.Generic;
using Entities.BaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#nullable disable

namespace Entities.UserModels
{
    public class AccGroup : BaseEntityWithActors<int>
    {
        public string Title { get; set; }
        public string LatinTitle { get; set; }
        public string Description { get; set; }

        public ICollection<AccGroupUser> GroupUsers { get; set; }
        public ICollection<AccGroupRole> GroupRoles { get; set; }
    }

    public class AccGroupConfiguration : IEntityTypeConfiguration<AccGroup>
    {
        public void Configure(EntityTypeBuilder<AccGroup> builder)
        {
            builder.Property(p => p.Title).IsRequired();
            builder.Property(p => p.LatinTitle).IsRequired();
            builder.HasData(new AccGroup()
            {
                Id = 1,
                CreationDate = new DateTime(2022, 10, 10),
                IsActive = true,
                LatinTitle = "Super Admins",
                ModificationDate = new DateTime(2022, 10, 10),
                Title = "سوپرادمین ها",
                CreatorId = 1,
                ModifierId = 1
            });
        }
    }
}
