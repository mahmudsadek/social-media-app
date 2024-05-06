using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using social_media_app.DTOs;
using social_media_app.Models;
using social_media_app.Repository;

namespace social_media_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatRepository chatRepository;
        private readonly IMessageRepository messageRepository;

        public ChatController(IChatRepository _chatRepository, IMessageRepository _messageRepository)
        {
            chatRepository = _chatRepository;
            messageRepository = _messageRepository;
        }


        [HttpGet]
        public IActionResult GetChat(int id)
        {
            Chat chat = chatRepository.GetChatWithMessagesAndUsers(id, "Messages");
            if (chat == null)
            {
                return NotFound();
            }

            ChatDTO chatDto = new ChatDTO();
            chatDto.Id = chat.Id;
            chatDto.SenderId = chat.SenderId;
            chatDto.ReceiverId = chat.ReceiverId;

            return Ok(chatDto);
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            return Ok(chatRepository.GetAll());
        }

        [HttpPost]
        public IActionResult NewChat(ChatDTO chat)
        {
            if (ModelState.IsValid == true)
            {
                Chat newChat = new();
                newChat.Id = chat.Id;
                newChat.SenderId = chat.SenderId;
                newChat.ReceiverId = chat.ReceiverId; // One to one 

                chatRepository.Insert(newChat);
                chatRepository.Save();
                return CreatedAtAction("GetChat", new { id = newChat.Id }, newChat);

            }
            return BadRequest(ModelState);

        }


        [HttpDelete("{id}")]
        public IActionResult DeleteChat(int id)
        {
            Chat chat = chatRepository.GetChatWithMessagesAndUsers(id, "Messages");
            if (chat == null)
            {
                return NotFound();
            }

            if(chat.LastMessageId == null)
            {
                chatRepository.Delete(chat);
                chatRepository.Save();
            } else
            {
                chat.LastMessageId = null;
                chatRepository.DeleteChatHaveMessages(chat);
            }

            return NoContent();
        }





    }
}
