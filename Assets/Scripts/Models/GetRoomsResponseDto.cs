using System;
using System.Collections.Generic;

namespace Models
{
    public class GetRoomsResponseDto
    {
        public Guid roomId;
        public string name;
        public int width;
        public int height;
        public string tileId;
        public bool isOwner;
        public List<GetPropsResponse> props;
    }
}