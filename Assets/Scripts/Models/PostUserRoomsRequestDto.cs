using System;

namespace Models
{
    public class PostUserRoomsRequestDto
    {
        public Guid roomid;
        public string username;
        public bool isOwner;
    }
}