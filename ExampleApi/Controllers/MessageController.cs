using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/*-------------------------------------*/
using Microsoft.AspNetCore.Cors;
using ExampleApi.Model;
/*-------------------------------------*/

namespace ExampleApi.Controllers {
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase {

        private List<MessageDto> messages_ = new List<MessageDto> {
            new MessageDto {
                Id = 1,
                Title = "Thanks for register",
                From = "sys_admin",
                Time = DateTime.UtcNow,
                Body = "Thanks for register our Product. "
            },
            new MessageDto {
                Id = 2,
                Title = "New friend message",
                From = "sys_admin",
                Time = DateTime.UtcNow,
                Body = "someone send you a friend invite.check is out !",
            },
            new MessageDto {
                Id = 3,
                Title = "Friend Message",
                From = "Blade Master",
                Time = DateTime.UtcNow,
                Body = "Come,Let us do battle ! Let your blade talk"
            }
        };

        [Route("api/[controller]/username/{username}")]
        [HttpGet]
        public ICollection<MessageDto> MessageList([FromRoute] string username) {
            Console.WriteLine($"query user:{username}'s message list");
            return messages_;
        }

        [Route("api/[controller]/mid/{mid}")]
        [HttpGet]
        public MessageDto MessageDetails([FromRoute] int mid) {
            return
            messages_.Where(e => e.Id == mid)
                         .FirstOrDefault();
        }
    }
}
