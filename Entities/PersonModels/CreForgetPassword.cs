using Entities.BaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.PersonModels
{
    public class HrForgetPassword : BaseEntity<long>
    {
        public long FPersonsId { get; set; }
        public DateTime IssueDate { get; set; }
        public int Code { get; set; }
        public CrePerson Person { get; set; }
        public bool IsActive { get; set; }
    }
    public class HrForgetPasswordConfiguration : IEntityTypeConfiguration<HrForgetPassword>
    {
        public void Configure(EntityTypeBuilder<HrForgetPassword> builder)
        {
            builder.Property(p => p.FPersonsId).IsRequired();
            builder.Property(p => p.Code).IsRequired();
            builder.Property(p => p.IssueDate).IsRequired();
            //builder.HasOne(p => p.Person).WithMany(c => c.ForgetPasswords).HasForeignKey(c => c.FPersonsId);

        }
    }
}
