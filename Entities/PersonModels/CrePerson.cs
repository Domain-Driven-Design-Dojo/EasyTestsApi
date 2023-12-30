using Entities.BaseModels;
using Entities.UserModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using static Common.Utilities.GlobalEnums;

#nullable disable

namespace Entities.PersonModels
{
    public class CrePerson : BaseEntityWithActors<long>
    {
        //public CrePerson()
        //{
        //    Users = new HashSet<ApplicationUser>();
        //}
        public int FPersonsTypesId { get; set; }
        public CrePersonType PersonType { get; set; }
        public CreIndividual IndividualPerson { get; set; }
        public CreCompany Company { get; set; }
        public ApplicationUser User { get; set; }
        //public ICollection<HrForgetPassword> ForgetPasswords { get; set; }
    }

    public class CrePersonConfiguration : IEntityTypeConfiguration<CrePerson>
    {
        public void Configure(EntityTypeBuilder<CrePerson> builder)
        {
            builder.HasOne(p => p.PersonType).WithMany(c => c.Persons).HasForeignKey(p => p.FPersonsTypesId);
            builder.HasOne(p => p.Creator).WithMany(c => c.CreatorPersons).HasForeignKey(p => p.CreatorId);
            builder.HasOne(p => p.Modifier).WithMany(c => c.ModifierPersons).HasForeignKey(p => p.ModifierId);
            var adminPerson = new CrePerson()
            {
                Id = 1,
                CreationDate = new DateTime(2022, 10, 10),
                FPersonsTypesId = (int)PersonsType.Individual,
                CreatorId = 1,
                IsActive = true,
                ModifierId = 1,
                ModificationDate = new DateTime(2022, 10, 10)
            };
            builder.HasData(adminPerson);
        }
    }
}
