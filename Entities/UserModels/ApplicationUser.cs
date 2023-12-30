using Entities.BaseModels;
using Entities.PersonModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.UserModels
{
    public class ApplicationUser : IdentityUser<long>, IEntity<long>
    {
        public ApplicationUser()
        {
            IsActive = true;
            EmailConfirmed = true;
        }
        public bool IsActive { get; set; }
        public int? ConfirmationCode { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public long? FPersonsId { get; set; }
        [ForeignKey("FPersonsId")]

        public CrePerson Person { get; set; }
        public ICollection<CrePerson> CreatorPersons { get; set; }
        public ICollection<CrePerson> ModifierPersons { get; set; }

    }

    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasOne(p => p.Person).WithOne(c => c.User).HasForeignKey<ApplicationUser>(p => p.FPersonsId);

            var admin = new ApplicationUser
            {
                Id = 1,
                UserName = "superadmin",
                NormalizedUserName = "SUPERADMIN",
                Email = "superadmin@contoso.com",
                NormalizedEmail = "SUPERADMIN@CONTOSO.COM",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.Parse("55F82B99-AF47-426B-B09E-AF2D57E39F77").ToString("D"), //new Guid().ToString("D"),
                CreationDate = new DateTime(2022, 10, 10),
                ModificationDate = new DateTime(2022, 10, 10),
                ConcurrencyStamp = Guid.Parse("0AE777A1-1159-4A21-B4BB-FD2998062E6A").ToString("D")
            };

            //admin.PasswordHash = PassGenerate(admin, "superadmin");
            admin.PasswordHash = "AQAAAAEAACcQAAAAELOWEI9Oc6M53pTbwJUTVPd6f0+gZCRwDi7zS3kvrnzsqAam11gJpcoiTdOhOW+ORQ==";
            builder.HasData(admin);
        }

        private string PassGenerate(ApplicationUser user, string plainPassword)
        {
            var passHash = new PasswordHasher<ApplicationUser>();
            return passHash.HashPassword(user, plainPassword);
        }
    }
}