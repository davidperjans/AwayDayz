using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AwayDayz.Application.Common;
using MediatR;

namespace AwayDayz.Application.Commands.FriendRequests.Commands
{
    public class SendFriendRequestCommand : IRequest<OperationResult<string>>
    {
        public string SenderId { get; set; }
        public string ReceiverUsername { get; set; }
        public SendFriendRequestCommand(string senderId, string receiverUsername)
        {
            SenderId = senderId;
            ReceiverUsername = receiverUsername;
        }
    }
}
