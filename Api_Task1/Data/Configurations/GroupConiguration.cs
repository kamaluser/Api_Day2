using Api_Task1.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api_Task1.Data.Configurations
{
    public class GroupConiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.Property(x => x.No).HasMaxLength(5).IsRequired(true);
            builder.Property(x=>x.Limit).IsRequired(true);
        }
    }
}
