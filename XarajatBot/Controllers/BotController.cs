using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xarajat.Data.Entities;
using XarajatBot.Repositories;
using XarajatBot.Services;
using XarajatData.Entities;

namespace XarajatBot.Controllers;


[ApiController]
[Route("bot")]
public class BotController : ControllerBase
{
	private readonly TelegramBotService _botService;
	private readonly UserRepository _userRepository;
	private readonly RoomRepository _roomRepository;

	public BotController(
		TelegramBotService botService,
		UserRepository userRepository,
		RoomRepository roomRepository)
	{
		_botService = botService;
		_userRepository = userRepository;
		_roomRepository = roomRepository;
	}

	[HttpGet]
	public IActionResult GetMe() => Ok("working...");

	[HttpPost]
	public async Task PostUpdate(Update update)
	{
		if (update.Type is not UpdateType.Message)
			return;

		var (chatId, message, userName) = GetValues(update);

		var user = await FilterUser(chatId, userName);

		if (user.Step == 0)
		{
			if (message == "Create room")
			{
				user.Step = 1;
				await _userRepository.UpdateUser(user);
				 _botService.SendMessage(user.ChatId, "Enter room name ?");
			}
			else if (message == "Jiun room")
			{
				user.Step = 2;
				await _userRepository.UpdateUser(user);
				_botService.SendMessage(user.ChatId, "Enter room key ?");
			}
			else
			{
				var menu = new List<string> {"Create room", "Join room"};

				_botService.SendMessage(user.ChatId, "Menu", reply: _botService.GetKeyboard(menu));
			}
		}
		else if (user.Step == 1)
		{
			var room = new Room
			{
				Name = message,
				Key = Guid.NewGuid().ToString("N")[..10],
				Status = RoomStatus.Created
			};

			await _roomRepository.AddRoomAsync(room);

			user.RoomId = room.Id;
			user.IsAdmin = true;
			user.Step = 3;

			await _userRepository.UpdateUser(user);

			var menu = new List<string> {"Add outlay", "Calculate"};
			_botService.SendMessage(user.ChatId, "Menu", reply: _botService.GetKeyboard(menu));
		}
		else if (user.Step == 3)
		{
			if (message == "Add outlay")
			{
				user.Step = 1;
				await _userRepository.UpdateUser(user);
				_botService.SendMessage(user.ChatId, "Enter outlay details ?");
			}
			else if (message == "Calculate")
			{
				user.Step = 2;
				await _userRepository.UpdateUser(user);
				_botService.SendMessage(user.ChatId, "Room Statistics");
			}
			else
			{
				var menu = new List<string> {"Add outlay", "Calculate"};
				_botService.SendMessage(user.ChatId, "Menu", reply: _botService.GetKeyboard(menu));
			}
		}

	}

	private Tuple<long, string, string> GetValues(Update update)
	{
		var chatId = update.Message!.From!.Id;
		var message = update.Message.Text!;
		var name = update.Message.From.Username ?? update.Message.From.FirstName;

		return new(chatId, message, name);
	}

	private async Task<UserEntity> FilterUser(long chatId, string userName)
	{
		var user = await _userRepository.GetUserByChatId(chatId);

		if (user is null)
		{
			user = new UserEntity
			{
				ChatId = chatId,
				Name = userName
			};

			await _userRepository.AddUserAsync(user);
		}
		return user;
	}
}
