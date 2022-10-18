using Entities.DatabaseModels.CommonModels.BaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Entities.DatabaseModels.UserModels
{
    public class AccGroupUser : BaseEntityWithActors<long>
    {
        public long UserId { get; set; }
        public int FGroupsId { get; set; }
        //public bool IsActive { get; set; }

        [Required]
        public ApplicationUser User { get; set; }
        public AccGroup Group { get; set; }
    }

    public class AccGroupUserConfiguration : IEntityTypeConfiguration<AccGroupUser>
    {
        public void Configure(EntityTypeBuilder<AccGroupUser> builder)
        {
            builder.Property(p => p.UserId).IsRequired();
            builder.Property(p => p.FGroupsId).IsRequired();
            //builder.Property(p => p.IsActive).IsRequired();
            builder.HasIndex(x => new { x.FGroupsId, x.UserId }).IsUnique();
            //builder.HasOne(p => p.User).WithMany(c => c.GroupUsers).HasForeignKey(c => c.FUsersId);
            builder.HasOne(p => p.Group).WithMany(c => c.GroupUsers).HasForeignKey(c => c.FGroupsId);

            builder.HasData(new AccGroupUser()
            {
                CreationDate = new DateTime(2022,10,10),
                CreatorId = 1,
                FGroupsId = 1,
                Id = 1,
                ModificationDate = new DateTime(2022,10,10),
                IsActive = true,
                UserId=1,
                ModifierId =1
            });
        }
    }
}
