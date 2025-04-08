using System.Security.Claims;
using AwayDayz.Application.Commands.FriendRequests.Commands;
using AwayDayz.Application.DTOs.FriendRequestDtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AwayDayz.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FriendRequestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FriendRequestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("send-friendrequest")]
        public async Task<IActionResult> SendFriendRequestAsync([FromBody] SendFriendRequestDto sendFriendRequestDto)
        {
            if (sendFriendRequestDto == null)
            {
                return BadRequest("Invalid friend request.");
            }

            // Get the sender
            var senderId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var command = new SendFriendRequestCommand(senderId!, sendFriendRequestDto.receiverUsername);

            var result = await _mediator.Send(command);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }
    }
}
