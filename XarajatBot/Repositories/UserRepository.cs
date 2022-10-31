using Microsoft.EntityFrameworkCore;
using Xarajat.Data.Context;
using Xarajat.Data.Entities;

namespace XarajatBot.Repositories;

public class UserRepository
{
	private readonly XarajatDbContext _context;

	public UserRepository(XarajatDbContext context)
	{
		_context = context;
	}

	public async Task<UserEntity?> GetUserByChatId(long chatId)
	{
		return await _context.UserEntities.FirstOrDefaultAsync(x => x.ChatId == chatId);
	}

	public async Task AddUserAsync(UserEntity userEntity)
	{
		await _context.UserEntities.AddAsync(userEntity);
		await _context.SaveChangesAsync();
	}

	public async Task UpdateUser(UserEntity userEntity)
	{
		_context.Update(userEntity);
		await _context.SaveChangesAsync();
	}
}
