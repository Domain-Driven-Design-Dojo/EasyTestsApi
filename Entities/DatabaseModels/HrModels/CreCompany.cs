using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Entities.DatabaseModels.CommonModels.BaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#nullable disable

namespace Entities.DatabaseModels.HrModels
{
    public class CreCompany : BaseEntity<long>
    {
        public long FPeopleId { get; set; }
        public string Title { get; set; }
        [AllowNull]
        public string NationalId { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string RegistrationNo { get; set; }
        public string EconomicNo { get; set; }
        [MaxLength(200)]
        public string TelNumbers { get; set; }

        public CrePerson Person { get; set; }
    }

    public class CreCompanyConfiguration : IEntityTypeConfiguration<CreCompany>
    {
        public void Configure(EntityTypeBuilder<CreCompany> builder)
        {
            builder.HasIndex(x => x.NationalId).IsUnique();
            builder.Property(p => p.Title).IsRequired().HasMaxLength(1000);
            builder.Property(p => p.NationalId).IsRequired().HasMaxLength(11);
            //builder.HasOne(p => p.Person).WithMany(c => c.Companies).HasForeignKey(c => c.FPeopleId);
            builder.HasOne(p => p.Person).WithOne(c => c.Company).HasForeignKey<CreCompany>(c => c.FPeopleId);
        }
    }
}
