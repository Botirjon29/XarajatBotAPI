using Microsoft.EntityFrameworkCore;
using Xarajat.Data.Entities;

namespace Xarajat.Data.Context;

public class XarajatDbContext : DbContext
{
	public DbSet<Outlay>? Outlays { get; set; }
	public DbSet<Room>? Rooms { get; set; }
	public DbSet<UserEntity>? UserEntities { get; set; }
	public XarajatDbContext(DbContextOptions options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		RoomConfiguration.Configure(modelBuilder.Entity<Room>());

		modelBuilder.ApplyConfigurationsFromAssembly(typeof(XarajatDbContext).Assembly);
	}
}
