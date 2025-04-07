using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AwayDayz.Domain.Enums;

namespace AwayDayz.Domain.Entities
{
    public class FriendRequest
    {
        public int Id { get; set; }

        public string SenderId { get; set; }
        public User Sender { get; set; }

        public string ReceiverId { get; set; }
        public User Receiver { get; set; }

        public DateTime SentAt { get; set; }
        public RequestStatus Status { get; set; }
        public DateTime? AcceptedOrRejectedAt { get; set; }
    }
}
