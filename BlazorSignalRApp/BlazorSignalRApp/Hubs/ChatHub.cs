using System.Diagnostics;
using BlazorSignalRApp.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace BlazorSignalRApp.Hubs;


[ApiController]
[Route("api/[controller]")]
public class MessagesController : ControllerBase
{
    private readonly AppDBContext _dbContext;

    public MessagesController(AppDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public IEnumerable<Message> GetMessages()
    {
        // Does it have to be a list?
        return _dbContext.Messages.Include(m => m.SenderId).ToList();
    }
}

public class ChatHub : Hub
{

    private readonly AppDBContext _dbContext;
    private readonly ILogger _logger;

		public ChatHub(AppDBContext dbContext, ILogger<ChatHub>logger)
		{
			_dbContext = dbContext;
            _logger = logger; 
		}
		
    public async Task SendMessage(string user, string messageContent)
	{

			var message = new Message(content: messageContent, sentTime: DateTime.Now, senderId: user);

			_dbContext.Messages.Add(message);
            await Clients.All.SendAsync("ReceiveMessage", user, message);
            
			await _dbContext.SaveChangesAsync();
			
    }

    public async Task<List<string>> InitialiseMessageHistory(string connectionId)
    {

        var serialisedListOfMessages = await _dbContext.Messages
            .Select(m => m.ToString())
            .ToListAsync();
       
        await Clients.All.SendAsync("LoadMessages", connectionId, serialisedListOfMessages);

        return serialisedListOfMessages;
    }
}