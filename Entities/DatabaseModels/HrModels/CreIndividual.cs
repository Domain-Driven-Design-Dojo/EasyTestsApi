using Entities.DatabaseModels.CommonModels.BaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable disable

namespace Entities.DatabaseModels.HrModels
{
    public class CreIndividual : BaseEntity<long>
    {
        public long FPeopleId { get; set; }
        public string FirstName { get; set; }
        [AllowNull]
        public string LastName { get; set; }


        public CrePerson Person { get; set; }
    }

    public class CreIndividualConfiguration : IEntityTypeConfiguration<CreIndividual>
    {
        public void Configure(EntityTypeBuilder<CreIndividual> builder)
        {
            //   builder.HasIndex(x => x.NationalId).IsUnique();

            builder.Property(p => p.FPeopleId).IsRequired();
            builder.Property(p => p.FirstName).HasMaxLength(200);
            builder.Property(p => p.LastName).HasMaxLength(200);

            builder.HasOne(p => p.Person).WithOne(c => c.IndividualPerson).HasForeignKey<CreIndividual>(c => c.FPeopleId);

            var adminIndividual = new CreIndividual()
            {
                Id=1,
                FPeopleId = 1,
                FirstName = "Super Admin"
            };
            builder.HasData(adminIndividual);

        }
    }
}
