using AccessMNS.Controllers;
using AccessMNS.Models;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Driver;

namespace AccessMNS.Classes
{
    public class ChatHub : Hub
    {
        private readonly IMessageRepository _messageRepository;

        public ChatHub(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task SendMessage(int idCanaux, int idUser, string messageContent)
        {
            var message = new Message
            {
                ContenuMessage = messageContent,
                IdUser = idUser,
                IdCanaux = idCanaux,
                DateHeureEnvoi = DateTime.UtcNow
            };

            await _messageRepository.AddMessageAsync(message);

            // Diffuser le message en temps réel aux clients connectés
            await Clients.Group(idCanaux.ToString()).SendAsync("ReceiveMessage", message);
        }

        public async Task JoinChannel(int idCanaux)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, idCanaux.ToString());
        }

        public async Task LeaveChannel(int idCanaux)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, idCanaux.ToString());
        }
    }
}