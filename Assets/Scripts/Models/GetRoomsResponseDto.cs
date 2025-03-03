﻿using System;
using System.Collections.Generic;

namespace Models
{
    public class GetRoomsResponseDto
    {
        public Guid roomId;
        public string name;
        public float width;
        public float height;
        public string tileId;
        public List<UserEntry> users;
        public List<GetPropsResponse> props;
    }
}