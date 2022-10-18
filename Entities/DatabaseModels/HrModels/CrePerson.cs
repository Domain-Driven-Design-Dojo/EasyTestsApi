using Entities.DatabaseModels.CommonModels.BaseModels;
using Entities.DatabaseModels.UserModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using static Common.Utilities.GlobalEnums;

#nullable disable

namespace Entities.DatabaseModels.HrModels
{
    public class CrePerson : BaseEntityWithActors<long>
    {
        //public CrePerson()
        //{
        //    Users = new HashSet<ApplicationUser>();
        //}
        public int FPeopleTypesId { get; set; }
        public CrePeopleType PersonType { get; set; }
        public CreIndividual IndividualPerson { get; set; }
        public CreCompany Company { get; set; }
        public ApplicationUser User { get; set; }
        //public ICollection<HrForgetPassword> ForgetPasswords { get; set; }
    }

    public class CrePersonConfiguration : IEntityTypeConfiguration<CrePerson>
    {
        public void Configure(EntityTypeBuilder<CrePerson> builder)
        {
            builder.HasOne(p => p.PersonType).WithMany(c => c.People).HasForeignKey(p => p.FPeopleTypesId);
            builder.HasOne(p => p.Creator).WithMany(c => c.CreatorPeople).HasForeignKey(p => p.CreatorId);
            builder.HasOne(p => p.Modifier).WithMany(c => c.ModifierPeople).HasForeignKey(p => p.ModifierId);
            var adminPerson = new CrePerson() 
            {
                Id =1,
                CreationDate = new DateTime(2022, 10, 10),
                FPeopleTypesId = (int)PeopleType.Individual,
                CreatorId = 1,
                IsActive = true,
                ModifierId=1,
                ModificationDate = new DateTime(2022, 10, 10)
            };
            builder.HasData(adminPerson);
        }
    }
}
