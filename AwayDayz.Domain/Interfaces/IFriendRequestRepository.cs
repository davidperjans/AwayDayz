using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AwayDayz.Domain.Entities;

namespace AwayDayz.Domain.Interfaces
{
    public interface IFriendRequestRepository
    {
        Task<FriendRequest?> GetBetweenUsersAsync(string senderId, string receiverId, CancellationToken cancellationToken);
        Task AddAsync(FriendRequest request, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
