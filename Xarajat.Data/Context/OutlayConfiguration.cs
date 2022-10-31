using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xarajat.Data.Entities;

namespace Xarajat.Data.Context;

public class OutlayConfiguration : IEntityTypeConfiguration<Outlay>
{
    public void Configure(EntityTypeBuilder<Outlay> builder)
    {
        builder.ToTable("outlays")
            .HasKey(x => x.Id);

        builder.Property(o => o.Description)
            .HasColumnType("MaxLength(255)")
            .IsRequired(false);

        builder.Property(o => o.RoomId)
            .HasColumnName("nameof(room_id)")
            .IsRequired();

        builder.Property(o => o.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.Ignore(o => o.ToReadable);

        builder.HasOne(o => o.User)
            .WithMany(u => u.Outlays)
            .HasForeignKey(o => o.UserId);

        builder.HasOne(o => o.Room)
            .WithMany(u => u.Outlays)
            .HasForeignKey(o => o.RoomId);
    }

}
