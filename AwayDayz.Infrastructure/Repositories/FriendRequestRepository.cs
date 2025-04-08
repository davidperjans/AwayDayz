using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AwayDayz.Domain.Entities;
using AwayDayz.Domain.Interfaces;
using AwayDayz.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace AwayDayz.Infrastructure.Repositories
{
    public class FriendRequestRepository : IFriendRequestRepository
    {
        private readonly AppDbContext _context;

        public FriendRequestRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(FriendRequest request, CancellationToken cancellationToken)
        {
            await _context.FriendRequests.AddAsync(request, cancellationToken);
        }

        public async Task<FriendRequest?> GetBetweenUsersAsync(string senderId, string receiverId, CancellationToken cancellationToken)
        {
            return await _context.FriendRequests.FirstOrDefaultAsync(fr =>
                (fr.SenderId == senderId && fr.ReceiverId == receiverId) ||
                (fr.SenderId == receiverId && fr.ReceiverId == senderId),
                cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
