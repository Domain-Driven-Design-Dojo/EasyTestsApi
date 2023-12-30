using Entities.BaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;


namespace Entities.UserModels
{
    public class AccGroupRole : BaseEntityWithActors<long>
    {
        public long FRolesId { get; set; }
        public int FGroupsId { get; set; }
        public AccRole Role { get; set; }
        public AccGroup Group { get; set; }
    }

    public class AccGroupRoleConfiguration : IEntityTypeConfiguration<AccGroupRole>
    {
        public void Configure(EntityTypeBuilder<AccGroupRole> builder)
        {
            builder.Property(p => p.FRolesId).IsRequired();
            builder.Property(p => p.FGroupsId).IsRequired();
            builder.HasIndex(x => new { x.FRolesId, x.FGroupsId }).IsUnique();
            builder.HasOne(p => p.Role).WithMany(c => c.GroupRoles).HasForeignKey(c => c.FRolesId);
            builder.HasOne(p => p.Group).WithMany(c => c.GroupRoles).HasForeignKey(c => c.FGroupsId);

            builder.HasData(new AccGroupRole()
            {
                CreationDate = new DateTime(2022, 10, 10),
                CreatorId = 1,
                FGroupsId = 1,
                FRolesId = 1,
                Id = 1,
                IsActive = true,
                ModificationDate = new DateTime(2022, 10, 10),
                ModifierId = 1
            });
        }
    }
}
