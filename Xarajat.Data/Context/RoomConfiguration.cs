using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.ComponentModel.DataAnnotations;
using Xarajat.Data.Entities;
using XarajatData.Entities;

namespace Xarajat.Data.Context;

public class RoomConfiguration 
{
    public static void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.ToTable("rooms")
            .Property(r => r.Name)
            .IsRequired()
            .HasColumnType("MaxLength(50)");

        builder.Property(r => r.Key)
            .HasColumnType("MaxLength(20)")
            .IsRequired();

        builder.Property(r => r.Status)
            .HasDefaultValue(RoomStatus.Created)
            .IsRequired();
    }
}
