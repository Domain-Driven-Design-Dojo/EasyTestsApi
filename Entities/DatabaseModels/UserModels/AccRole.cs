using Entities.DatabaseModels.CommonModels.BaseModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Entities.DatabaseModels.UserModels
{
    public class AccRole : IdentityRole<long>, IEntity<long>
    {

        [Required]
        [StringLength(100)]
        public string Description { get; set; }
        public string PersianName { get; set; }
        public ICollection<AccGroupRole> GroupRoles { get; set; }

    }

    public class RoleConfiguration : IEntityTypeConfiguration<AccRole>
    {
        public void Configure(EntityTypeBuilder<AccRole> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.HasData(new AccRole()
            {
                ConcurrencyStamp = new Guid().ToString("D"),
                Description = "Super admin's group",
                Id =1,
                Name = "SuperAdmin",
                NormalizedName = "SUPERADMIN",
                PersianName = "سوپرادمین"
            });
        }
    }
}
