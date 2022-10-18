using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DatabaseModels.CommonModels.BaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.DatabaseModels.HrModels
{
    public class HrForgetPassword : BaseEntity<long>
    {
        public long FPeopleId { get; set; }
        public DateTime IssueDate { get; set; }
        public int Code { get; set; }
        public CrePerson Person { get; set; }
        public bool IsActive { get; set; }
    }
    public class HrForgetPasswordConfiguration : IEntityTypeConfiguration<HrForgetPassword>
    {
        public void Configure(EntityTypeBuilder<HrForgetPassword> builder)
        {
            builder.Property(p => p.FPeopleId).IsRequired();
            builder.Property(p => p.Code).IsRequired();
            builder.Property(p => p.IssueDate).IsRequired();
            //builder.HasOne(p => p.Person).WithMany(c => c.ForgetPasswords).HasForeignKey(c => c.FPeopleId);

        }
    }
}
