using AccessMNS.Controllers;
using AccessMNS.Models;
using AccessMNS.MongoDb;
using Microsoft.AspNetCore.Mvc;

namespace AccessMNS.API
{
    [ApiController]
    [Route("api/messages")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageRepository _messageRepository;

        public MessageController(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        // À approfondir ...
        [HttpGet("{idCanaux}")]
        public async Task<IActionResult> GetMessages(int idCanaux)
        {
            var messages = await _messageRepository.GetMessagesByCanalAsync(idCanaux);
            if (messages == null || !messages.Any()) return Ok(new List<Message>());

            return Ok(messages);
        }


        // À approfondir ...
        [HttpPost]
        public async Task<IActionResult> PostMessage([FromBody] Message message)
        {
            var newMessage = await _messageRepository.AddMessageAsync(message);
            return CreatedAtAction(nameof(GetMessages), new { idCanaux = newMessage.IdCanaux }, newMessage);
        }
    }
}