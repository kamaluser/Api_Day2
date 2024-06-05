using Api_Task1.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api_Task1.Data.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(x=>x.FullName).HasMaxLength(35).IsRequired(true);
            builder.Property(x=>x.Email).HasMaxLength(100).IsRequired(true);
            builder.HasOne(x => x.Group).WithMany(g => g.Students).HasForeignKey(x=>x.GroupId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
