using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AwayDayz.Application.Commands.FriendRequests.Commands;
using AwayDayz.Application.Common;
using AwayDayz.Domain.Entities;
using AwayDayz.Domain.Enums;
using AwayDayz.Domain.Interfaces;
using AwayDayz.Infrastructure.Database;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AwayDayz.Application.Commands.FriendRequests.CommandHandlers
{
    public class SendFriendRequestCommandHandler : IRequestHandler<SendFriendRequestCommand, OperationResult<string>>
    {
        private readonly IFriendRequestRepository _friendRequestRepository;
        private readonly UserManager<User> _userManager;

        public SendFriendRequestCommandHandler(IFriendRequestRepository friendRequestRepository, UserManager<User> userManager)
        {
            _friendRequestRepository = friendRequestRepository;
            _userManager = userManager;
        }


        public async Task<OperationResult<string>> Handle(SendFriendRequestCommand request, CancellationToken cancellationToken)
        {
            // Get the receiver
            var receiver = await _userManager.FindByNameAsync(request.ReceiverUsername);

            if (receiver == null)
            {
                return OperationResult<string>.Failure("Receiver not found.");
            }

            // Check if the sender and receiver are the same
            if (request.SenderId == receiver.Id)
            {
                return OperationResult<string>.Failure("You cannot send a friend request to yourself.");
            }

            // Check if a friend request already exists
            var existingRequest = await _friendRequestRepository.GetBetweenUsersAsync(request.SenderId, receiver.Id, cancellationToken);

            if (existingRequest != null)
            {
                return OperationResult<string>.Failure("A friend request already exists between these users.");
            }

            // Create a new friend request
            var friendRequest = new FriendRequest
            {
                SenderId = request.SenderId,
                ReceiverId = receiver.Id,
                Status = RequestStatus.Pending,
                SentAt = DateTime.UtcNow
            };

            // Add the friend request to the database
            await _friendRequestRepository.AddAsync(friendRequest, cancellationToken);
            await _friendRequestRepository.SaveChangesAsync(cancellationToken);

            return OperationResult<string>.Success("Friend request sent successfully.");
        }
    }
}
