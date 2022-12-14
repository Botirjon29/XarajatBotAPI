using Xarajat.Data.Context;
using Xarajat.Data.Entities;

namespace XarajatBot.Repositories;

public class RoomRepository
{
	private readonly XarajatDbContext _context;

	public RoomRepository(XarajatDbContext context)
	{
		_context = context;
	}

	public async Task<Room?> GetRoomById(int id)
	{
		return await _context.Rooms.FindAsync(id);
	}

	public async Task AddRoomAsync(Room room)
	{
		await _context.Rooms.AddAsync(room);
		await _context.SaveChangesAsync();
	}

	public async Task UpdateRoomAsync(Room room)
	{
		_context.Update(room);
		await _context.SaveChangesAsync();

	}
}
